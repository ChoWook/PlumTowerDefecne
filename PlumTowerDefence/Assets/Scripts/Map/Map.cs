using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] GameObject GroundPrefab;

    [SerializeField] MapGimmicSpawner GimmicSpawner;

    public static Map Instance;

    Camera MainCamera;

    [HideInInspector]
    public List<Ground> Grounds = new();

    public int OpenGroundCnt = 0;                    // �� ���� �׶��尡 ���ȴ���

    public int AttackRouteCnt = 1;                   // ��� ������ ���ݷΰ� �����Ǿ�����

    public int HoleEmptyLandCnt = 0;                 // ��ü �� �� ����ִ� ������ ��

    float GroundSize = 10;

    private void Awake()
    {
        MainCamera = Camera.main;

        Instance = this;
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

        for(int i = 0; i < NewMap._Grounds.Count; i++)
        {
            AddGround(NewMap._Grounds[i]._PosX, NewMap._Grounds[i]._PosY, NewMap._Grounds[i]._Type);
        }
    }

    // Ư���� ��ǥ�� Ground ����
    public void AddGround(int x, int y, EGroundType type)
    {
        Ground NewGround = Instantiate(GroundPrefab, transform).GetComponent<Ground>();

        NewGround.transform.localPosition = new Vector3(x * GroundSize, 0, y * GroundSize);

        NewGround.SetGroundPattern(type);

        Grounds.Add(NewGround);
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
        for(int i = 0; i < AttackRouteCnt; i++)
        {
            // ���������� �����µ��� �� �Լ��� ����Ǹ� �ȵ�
            if(Grounds.Count == OpenGroundCnt)
            {
                Debug.LogWarning("Worng Function Call");
                return;
            }

            Grounds[OpenGroundCnt].IsActive = true;

            HoleEmptyLandCnt += Grounds[OpenGroundCnt].EmptyLandTileCount;

            GimmicSpawner.SpawnGimmick(EMapGimmickType.Obstacle, OpenGroundCnt);
        }

        GimmicSpawner.SpawnGimmick(EMapGimmickType.Resource);

        GimmicSpawner.SpawnGimmick(EMapGimmickType.Treasure);
    }

    // ó�� ���� �׶��带 ������ ������ �׶��� ��Ȱ��ȭ
    public void HideGrounds()
    {
        //TODO ���  �׶��带 ó�� ���� �׶���� ���� ���ؾ� ��

        int StartGround = 3;

        OpenGroundCnt = StartGround;

        for (int i = StartGround; i < Grounds.Count; i++)
        {
            Grounds[i].IsActive = false;
        }
    }
}
