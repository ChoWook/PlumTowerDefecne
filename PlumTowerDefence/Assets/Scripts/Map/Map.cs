using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTS_Cam;
using TMPro;

[System.Serializable]
public struct Pos{
    public int PosX;
    public int PosY;

    public Pos SumPos(Pos p)
    {
        Pos NewPos = new Pos();
        NewPos.PosX = PosX + p.PosX;
        NewPos.PosY = PosY + p.PosY;
        return NewPos;
    }
}

public enum Direction
{
    R,
    L,
    D,
    U,
    UL,
    UR,
    DL,
    DR,
}


// ��ųʸ� ������ ������ �ϱ� ���� Ŭ����
public class PosComparer : IEqualityComparer<Pos>
{
    public bool Equals(Pos x, Pos y)
    {
        return x.PosX == y.PosX && x.PosY == y.PosY;
    }

    public int GetHashCode(Pos pos)
    {
        return pos.PosX.GetHashCode() ^ pos.PosY.GetHashCode() << 2;
    }

}

public class Map : MonoBehaviour
{
    [SerializeField] GameObject GroundPrefab;

    [SerializeField] MapGimmicSpawner GimmicSpawner;

    [SerializeField] GameObject EnemySpawnerPrefab;

    [SerializeField] GameObject Waypoint;

    public static Map Instance;

    RTS_Camera MainCamera;

    [HideInInspector]
    public List<Ground> Grounds = new();                        // �����Ǵ� ������� ����Ǵ� ����Ʈ

    public Dictionary<Pos, Ground> GroundsWithPos = new Dictionary<Pos, Ground>(new PosComparer());      // �׶����� ��ġ�� Ű������ �ϴ� ��ųʸ�

    public int OpenGroundCnt = 0;                               // �� ���� �׶��尡 ���ȴ���

    public int AttackRouteCnt = 1;                              // ��� ������ ���ݷΰ� �����Ǿ�����

    public int CurAttackRouteCnt = 1;

    public int HoleEmptyLandCnt = 0;                            // ��ü �� �� ����ִ� ������ ��

    List<Tile> EmptyLandTiles = new();

    List<Ground> CurEnemySpawnerGrounds = new();                // �̹� ������������ ���� ���;��� �׶���

    Pos HousePos = new Pos { PosX = 0, PosY = 0 };

    public Dictionary<Direction, Pos> _Direction;

    int GroundSize = 10;

    float YFOV = 20;

    float XFOV = 32.9f;

    [Header("Debug")]
    [SerializeField] TMP_InputField MapInput;

    public void OnSetMapPatternBtnClick()
    {
        if(MapInput.text != null)
        {
            int tmp = 0;

            int.TryParse(MapInput.text, out tmp);

            SetMapPattern(tmp);
        }
    }


    private void Awake()
    {
        Tables.Load();

        MainCamera = Camera.main.GetComponent<RTS_Camera>();

        Instance = this;

        _Direction = new();
        _Direction.Add(Direction.R, new Pos { PosX = 1, PosY = 0 });        // ��������
        _Direction.Add(Direction.L, new Pos { PosX = -1, PosY = 0 });
        _Direction.Add(Direction.D, new Pos { PosX = 0, PosY = -1 });
        _Direction.Add(Direction.U, new Pos { PosX = 0, PosY = 1 });

        _Direction.Add(Direction.UR, new Pos { PosX = 1, PosY = 1 });
        _Direction.Add(Direction.UL, new Pos { PosX = -1, PosY = 1 });
        _Direction.Add(Direction.DR, new Pos { PosX = 1, PosY = -1 });
        _Direction.Add(Direction.DL, new Pos { PosX = -1, PosY = -1 });



        // TODO ���Ӹ޴����� �Ѱܾ� ��
        Cursor.lockState = CursorLockMode.Confined;

        XFOV = 1 / Mathf.Tan(XFOV * Mathf.Deg2Rad);

        YFOV = 1 / Mathf.Tan(YFOV * Mathf.Deg2Rad);

    }

    public void ChooseRandomMapPattern()
    {
        int RandomID = Random.Range(1, Tables.MapPattern.GetSzie() + 1);

        SetMapPattern(RandomID);

        HideAllGridLine();
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

        SetWaypoints();

        // ó���� ���� �ִ� �׶��带 �����ϰ� ���� ��Ȱ��ȭ
        InitGrounds();

        // ������ ���۵����� ���ӸŴ��� ���� �ʱ�ȭ
        InitGameManager();
    }

    void InitGameManager()
    {
        // ���� Ÿ�� ������ ���
        GameManager.instance.unitTileSize = GetTileInMap(new Pos { PosX = 1, PosY = 0 }).transform.position.x
            - GetTileInMap(new Pos { PosX = 0, PosY = 0 }).transform.position.x;

        GameManager.instance.InitCoupon();
    }


    // Ư���� ��ǥ�� Ground ����
    public void AddGround(int x, int y, EGroundType type)
    {
        Ground NewGround = Instantiate(GroundPrefab, transform).GetComponent<Ground>();

        Pos NewPos = new Pos { PosX = x, PosY = y };

        NewGround.SetPosition(NewPos);

        NewGround.SetGroundPattern(type);

        Grounds.Add(NewGround);

        GroundsWithPos.Add(NewPos, NewGround);
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
        else if (tmpX > MainCamera.MaxX)
        {
            MainCamera.MaxX = tmpX;
        }

        bool IsChangeY = false;

        if (tmpY < MainCamera.MinY)
        {
            MainCamera.MinY = tmpY;

            IsChangeY = true;
        }
        else if (tmpY > MainCamera.MaxY)
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

        var hx = x * XFOV;  //tan ����� �̸� �� ��

        var hy = y * YFOV;

        MainCamera.maxHeight = ((hx > hy) ? hx : hy) / 2;

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

        CurEnemySpawnerGrounds.Clear();

        for(int i = 0; i < CurAttackRouteCnt; i++)
        {
            // ���������� �����µ��� �� �Լ��� ����Ǹ� �ȵ�
            if(Grounds.Count == OpenGroundCnt)
            {
                Debug.LogWarning("Worng Function Call");
                return;
            }

            Ground _Ground = Grounds[OpenGroundCnt];

            _Ground.IsActive = true;

            UpdateCameraLimit(_Ground._Pos.PosX, _Ground._Pos.PosY);

            AddAttackRouteCnt += CheckBrach(_Ground.GroundType);

            HoleEmptyLandCnt += _Ground.EmptyLandTileCount;

            CurEnemySpawnerGrounds.Add(_Ground);

            SpawnAllGimmick(false);

            OpenGroundCnt++;
        }

        CurAttackRouteCnt += AddAttackRouteCnt;

        SpawnAllGimmick(true);
    }

    public void StartEnemySpawn()
    {
        for(int i = 0; i < CurEnemySpawnerGrounds.Count; i++)
        {
            CurEnemySpawnerGrounds[i].StartEnemySpawners(i);
        }
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
            UpdateCameraLimit(Grounds[i]._Pos.PosX, Grounds[i]._Pos.PosY);
        }

        // �� ù �׶��忡 �Ͽ콺 ����
        Grounds[0].CreateCastle();
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
        for(EMapGimmickType type = EMapGimmickType.LaneBuff; type <= EMapGimmickType.Treasure; type++)
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

    public Tile GetTileInMap(Pos Sender)
    {
        Pos pos = new Pos();

        int tmp = Sender.PosX + 3;
        pos.PosX = tmp / 7;
        if (tmp < 0 && tmp % 7 != 0)
        {
            pos.PosX -= 1;
        }

        tmp = Sender.PosY + 3;
        pos.PosY = tmp / 7;
        if (tmp < 0 && tmp % 7 != 0)
        {
            pos.PosY -= 1;
        }

        Ground ground = null;
        GroundsWithPos.TryGetValue(pos, out ground);

        return ground?.GetTileInMapPosition(Sender);
    }

    public void SetWaypoints()
    {
        HousePos.PosX = 0;
        HousePos.PosY = 0;

        Tile HouseTile = GetTileInMap(HousePos);

        Waypoints.points[0].Add(HouseTile.transform);

        Direction HouseDir;

        switch (HouseTile.ParentGround.GroundType)
        {
            case EGroundType.TD:
                HouseDir = Direction.D;
                break;
            case EGroundType.TL:
                HouseDir = Direction.L;
                break;
            case EGroundType.TR:
                HouseDir = Direction.R;
                break;
            case EGroundType.TU:
                HouseDir = Direction.U;
                break;
            default : HouseDir = Direction.R;
                break;
        }

        FindNextAttackRouteTile(HousePos, HouseDir, 0);
    }

    // dir�� �� Ÿ���� ��� �������κ��� Ȯ��Ǿ� �Դ°�
    void FindNextAttackRouteTile(Pos pos, Direction InDir, int Route)
    {
        Tile CurTile = GetTileInMap(pos);

        CurTile.IsSelectedAttackRoute = true;

        // ��� ���ݷο� ��������Ʈ ��ȯ�ϱ�
        /*
        var obj = ObjectPools.Instance.GetPooledObject("Waypoint");
        obj.transform.parent = CurTile.transform;
        obj.transform.localPosition = Vector3.zero;
        */

        // �ش� Ÿ���� �ڳʵ� �ƴϰ� �б����� �ƴ��� �˻�
        List<Direction> NextDirs = new();
        
        for(Direction OutDir = Direction.R; OutDir <= Direction.U; OutDir++)
        {
            Pos NextPos = pos.SumPos(_Direction[OutDir]);

            Tile NextTile = GetTileInMap(NextPos);

            if (NextTile == null)
            {
                continue;
            }

            if(NextTile.CheckTileType(ETileType.AttackRoute) && NextTile.IsSelectedAttackRoute == false)
            {
                NextDirs.Add(OutDir);
            }
        }

        // ���� ����
        if (NextDirs.Count == 0)
        {
            return;
        }

        // ���� 1��
        if (NextDirs.Count == 1)
        {
            Pos NextPos = pos.SumPos(_Direction[NextDirs[0]]);

            Tile NextTile = GetTileInMap(NextPos);

            // �����̸�
            if (InDir == NextDirs[0])
            {
                // ���� Ÿ���� �ٸ� �׶���� ���ʹ̽����� ����
                if (CurTile.ParentGround != NextTile.ParentGround)
                {
                    CreateEnemySpawner(CurTile, NextTile.transform, Route);
                }
            }
            // �ڳ��̸�
            else
            {
                SpawnWaypoint(CurTile.transform, Route);
            }

            FindNextAttackRouteTile(NextPos, NextDirs[0], Route);
        }

        // �б����̸�
        if (NextDirs.Count == 2)
        {
            SpawnWaypoint(CurTile.transform, Route);

            // �귣ġ Ÿ�Ͽ��� ����� ����
            CurTile.WaypointRoute = Route;
            CurTile.WaypointIndex = Waypoints.points[Route].Count - 1;

            FindNextAttackRouteTile(pos.SumPos(_Direction[NextDirs[0]]), NextDirs[0], Route);

            FindNextAttackRouteTile(pos.SumPos(_Direction[NextDirs[1]]), NextDirs[1], MakeBranch(Route, CurTile));
        }

        /* ���װ� �ִ� �ڵ�
         * 
        bool IsBranchGround = false;

        bool IsBranchTile = false;

        if (CheckBrach(CurTile.ParentGround.GroundType) == 1)
        {
            IsBranchGround = true;
        }
        // �ڳ� Ȥ�� �б�����
        foreach (Direction OutDir in _Direction.Keys)
        {
            Pos NextPos = SumPos(pos, _Direction[OutDir]);

            Tile NextTile = GetTileInMap(NextPos);

            if (NextTile == null)
            {
                continue;
            }

            // ���� ���õ� ���ݷΰ� �ƴҶ��� ����
            if (NextTile.TileType == ETileType.AttackRoute && !NextTile.IsSelectedAttackRoute)
            {
                // �׶��尡 �ٲ�� ���ʹ� ������ ����
                if(CurTile.ParentGround != NextTile.ParentGround)
                {
                    CreateEnemySpawner(CurTile, NextTile.transform, Route);
                }


                // ���� Ÿ���� �����̸�
                if(OutDir == InDir)
                {
                    if (IsBranchTile)
                    {
                        Debug.Log($"�б� �߻� {pos.PosX} , {pos.PosY}");

                        FindNextAttackRouteTile(NextPos, OutDir, MakeBranch(Route, CurTile));
                    }
                    else
                    {
                        IsBranchTile = true;

                        FindNextAttackRouteTile(NextPos, OutDir, Route);
                    }
                    
                    continue;
                }

                // ���� Ÿ�� ��ġ�� ������ �� ��������Ʈ ����
                Waypoints.points[Route].Add(CurTile.transform);
                var obj1 = ObjectPools.Instance.GetPooledObject("Waypoint");
                obj1.transform.SetParent(CurTile.transform);
                obj1.transform.localPosition = Vector3.zero;

                CurTile.WaypointRoute = Route;
                CurTile.WaypointIndex = Waypoints.points[Route].Count - 1;

                // Ÿ���� �������� ��
                if (IsBranchTile)
                {
                    Debug.Log($"�б� �߻� {pos.PosX} , {pos.PosY}");

                    FindNextAttackRouteTile(NextPos, OutDir, MakeBranch(Route, CurTile));
                }
                else
                {
                    // �� �׶��尡 �귣ġ�� �ִ� �׶����̸�
                    if (IsBranchGround)
                    {
                        IsBranchTile = true;
                    }

                    FindNextAttackRouteTile(NextPos, OutDir, Route);
                }
            }
        }
        */
    }

    int MakeBranch(int Route, Tile CurTile)
    {
        if(CurTile.WaypointRoute == -1 || CurTile.WaypointIndex == -1)
        {
            Debug.LogWarning("Make Branch Warning");
        }

        // �������� ��Ʈ ���� ����
        Waypoints.points.Add(CopyList(CurTile));

        AttackRouteCnt = Waypoints.points.Count - 1;

        Waypoints.points[AttackRouteCnt].Add(CurTile.transform);

        return AttackRouteCnt;
    }

    List<Transform> CopyList(Tile EndTile)
    {
        List<Transform> NewList = new();

        for(int i = 0; i < Waypoints.points[EndTile.WaypointRoute].Count; i++)
        {
            NewList.Add(Waypoints.points[EndTile.WaypointRoute][i]);

            if(i == EndTile.WaypointIndex)
            {
                break;
            }
        }

        return NewList;
    }

    public void CreateEnemySpawner(Tile cur, Transform next, int Route)
    {
        var Spawner = ObjectPools.Instance.GetPooledObject("EnemySpawner");

        Spawner.transform.SetParent(cur.transform);

        Spawner.transform.position = next.position;

        var ES = Spawner.GetComponent<EnemySpawner>();

        ES.Route = Route;

        ES.SpawnPoint = ES.transform;

        ES.WaypointIndex = Waypoints.points[Route].Count - 1;

        cur.ParentGround.EnemySpawners.Add(ES);
    }

    public void SpawnWaypoint(Transform CurTransform, int Route)
    {
        Waypoints.points[Route].Add(CurTransform);

        var obj1 = ObjectPools.Instance.GetPooledObject("Waypoint");

        obj1.transform.SetParent(CurTransform);

        obj1.transform.localPosition = Vector3.zero;
    }
}
