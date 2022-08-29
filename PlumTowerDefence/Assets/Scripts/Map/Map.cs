using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTS_Cam;

public class Map : MonoBehaviour
{
    [SerializeField] GameObject GroundPrefab;

    [SerializeField] MapGimmicSpawner GimmicSpawner;

    public static Map Instance;

    public int OpenGroundCnt = 0;                    // �� ���� �׶��尡 ���ȴ���

    public int AttackRouteCnt = 1;                   // ��� ������ ���ݷΰ� �����Ǿ�����

    public int HoleEmptyLandCnt = 0;                 // ��ü �� �� ����ִ� ������ ��

    public float GroundSize = 10;

    RTS_Camera MainCamera;

    float YFOV = 20;

    float XFOV = 32.9f;

    List<Tile> EmptyLandTiles = new();

    [HideInInspector]
    public List<Ground> Grounds = new();

    private void Awake()
    {
        MainCamera = Camera.main.GetComponent<RTS_Camera>();

        Instance = this;

        // TODO ���Ӹ޴����� �Ѱܾ� ��
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void ChooseRandomMapPattern()
    {
        int RandomID = Random.Range(1, Tables.MapPattern.GetSzie() + 1);

        SetMapPattern(RandomID);
    }

    public void SetMapPattern(int id = 0)
    {
        if(id == 0)
        {
            return;
        }

        var NewMap = Tables.MapPattern.Get(id);

        if (NewMap == null)
        {
            return;
        }

        // ī�޶� ��ġ �ʱ�ȭ
        InitCameraLimit(0, 0);

        for (int i = 0; i < NewMap._Grounds.Count; i++)
        {
            AddGround(NewMap._Grounds[i]._PosX, NewMap._Grounds[i]._PosY, NewMap._Grounds[i]._Type);
        }

        // ó���� ���� �ִ� �׶��带 �����ϰ� ���� ��Ȱ��ȭ
        InitGrounds();
    }

    // Ư���� ��ǥ�� Ground ����
    public void AddGround(int x, int y, EGroundType type)
    {
        Ground NewGround = Instantiate(GroundPrefab, transform).GetComponent<Ground>();

        NewGround.SetPosition(x, y);

        NewGround.SetGroundPattern(type);

        Grounds.Add(NewGround);
    }

    public void InitCameraLimit(int x, int y)
    {
        MainCamera.MinX = -x * GroundSize;
        MainCamera.MaxX = x * GroundSize;

        MainCamera.MinY = -y * GroundSize;
        MainCamera.MaxY = y * GroundSize;
    }

    public void UpdateCameraLimit(int x, int y)
    {
        float tmpX = x * GroundSize;
        float tmpY = y * GroundSize;

        if (tmpX < MainCamera.MinX)
        {
            MainCamera.MinX = tmpX;
        }
        else if(tmpX > MainCamera.MaxX)
        {
            MainCamera.MaxX = tmpX;
        }

        bool IsChangeY = false;

        if(tmpY < MainCamera.MinY)
        {
            MainCamera.MinY = tmpY;

            IsChangeY = true;
        }
        else if(tmpY > MainCamera.MaxY)
        {
            MainCamera.MaxY = tmpY;

            IsChangeY = true;
        }

        if (IsChangeY)
        {
            CalculateDelY();
        }

        CalculateMaxHeight();
    }

    public void CalculateDelY()
    {
        var set = 90 - MainCamera.transform.rotation.eulerAngles.x;


        var h = MainCamera.transform.localPosition.y;

        var delY = h * Mathf.Tan(Mathf.Deg2Rad * set);

        MainCamera.DelY = delY;
    }

    public void CalculateMaxHeight()
    {
        var x = (MainCamera.MaxX - MainCamera.MinX) / 2;

        var y = (MainCamera.MaxY - MainCamera.MinY) / 2;

        var hx = x * (1 / Mathf.Tan(XFOV * Mathf.Deg2Rad));

        var hy = y * (1 / Mathf.Tan(YFOV * Mathf.Deg2Rad));

        MainCamera.maxHeight = (hx > hy) ? hx : hy;

        
    }

    public void HideAllGridLine()
    {
        for(int i = 0; i < Grounds.Count; i++)
        {
            Grounds[i].HideGridLine();
        }
    }

    public void ShowAllGridLine()
    {
        for (int i = 0; i < Grounds.Count; i++)
        {
            Grounds[i].ShowGridLine();
        }
    }

    // �׶��� Ȯ��
    public void ShowNextGrounds()
    {
        int AddAttackRouteCnt = 0;

        for(int i = 0; i < AttackRouteCnt; i++)
        {
            // ���������� �����µ��� �� �Լ��� ����Ǹ� �ȵ�
            if(Grounds.Count == OpenGroundCnt)
            {
                Debug.LogWarning("Worng Function Call");
                return;
            }

            Ground _Ground = Grounds[OpenGroundCnt];

            _Ground.IsActive = true;

            UpdateCameraLimit(_Ground.PosX, _Ground.PosY);

            AddAttackRouteCnt += CheckBrach(_Ground.GroundType);
            
            HoleEmptyLandCnt += _Ground.EmptyLandTileCount;

            SpawnAllGimmick(false);

            OpenGroundCnt++;
        }

        AttackRouteCnt += AddAttackRouteCnt;

        SpawnAllGimmick(true);
    }

    public int CheckBrach(EGroundType type)
    {
        switch (type)
        {
            case EGroundType.URD:
            case EGroundType.UDL:
            case EGroundType.RDL:
            case EGroundType.URL:
                return 1;
            default:
                return 0;
        }
    }

    // ó�� ���� �׶��带 ������ ������ �׶��� ��Ȱ��ȭ
    public void InitGrounds()
    {
        int StartGround = Tables.GlobalSystem.Get("Start_Ground_Num")._Value;

        OpenGroundCnt = StartGround;

        for (int i = StartGround; i < Grounds.Count; i++)
        {
            Grounds[i].IsActive = false;
        }

        // ó�� ���� Ÿ�� ���� ��ġ�� �������� ī�޶� ���� ������Ʈ
        for (int i = 0; i < StartGround; i++)
        {
            UpdateCameraLimit(Grounds[i].PosX, Grounds[i].PosY);
        }
    }

    public List<Tile> GetEmptyLandTilesInMap()
    {
        EmptyLandTiles.Clear();

        for (int i = 0; i < OpenGroundCnt; i++)
        {
            EmptyLandTiles.AddRange(Grounds[i].GetEmptyLandTilesInGround());
        }

        return EmptyLandTiles;
    }

    void SpawnAllGimmick(bool IsExistedMap)
    {
        for(EMapGimmickType type = EMapGimmickType.Obstacle; type <= EMapGimmickType.Treasure; type++)
        {
            if (Tables.MapGimmick.Get(type)._ExistedMap == IsExistedMap)
            {
                if (IsExistedMap)
                {
                    GimmicSpawner.SpawnGimmick(type);
                }
                else
                {
                    GimmicSpawner.SpawnGimmick(type, OpenGroundCnt);
                }

            }
        }
    }
}
