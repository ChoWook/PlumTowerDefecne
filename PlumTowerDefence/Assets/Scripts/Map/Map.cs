using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTS_Cam;

[System.Serializable]
public struct Pos{
    public int PosX;
    public int PosY;
}

enum Direction
{
    R,
    L,
    D,
    U,
}

// 딕셔너리 연산을 빠르게 하기 위한 클래스
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
    public List<Ground> Grounds = new();                        // 생성되는 순서대로 저장되는 리스트

    public Dictionary<Pos, Ground> GroundsWithPos = new Dictionary<Pos, Ground>(new PosComparer());      // 그라운드의 위치를 키값으로 하는 딕셔너리

    public int OpenGroundCnt = 0;                               // 몇 개의 그라운드가 열렸는지

    public int AttackRouteCnt = 1;                              // 몇개의 갈래로 공격로가 형성되었는지

    public int HoleEmptyLandCnt = 0;                            // 전체 맵 중 비어있는 평지의 수

    List<Tile> EmptyLandTiles = new();

    Pos HousePos = new Pos { PosX = 0, PosY = 0 };

    Dictionary<Direction, Pos> _Direction;

    int GroundSize = 10;

    float YFOV = 20;

    float XFOV = 32.9f;

    private void Awake()
    {
        MainCamera = Camera.main.GetComponent<RTS_Camera>();

        Instance = this;

        _Direction = new();
        _Direction.Add(Direction.R, new Pos { PosX = 1, PosY = 0 });        // 동서남북
        _Direction.Add(Direction.L, new Pos { PosX = -1, PosY = 0 });
        _Direction.Add(Direction.D, new Pos { PosX = 0, PosY = -1 });
        _Direction.Add(Direction.U, new Pos { PosX = 0, PosY = 1 });

        // TODO 게임메니저로 넘겨야 함
        Cursor.lockState = CursorLockMode.Confined;

        XFOV = 1 / Mathf.Tan(XFOV * Mathf.Deg2Rad);

        YFOV = 1 / Mathf.Tan(YFOV * Mathf.Deg2Rad);
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

        // 카메라 위치 초기화
        InitCameraLimit(0, 0);

        for (int i = 0; i < NewMap._Grounds.Count; i++)
        {
            AddGround(NewMap._Grounds[i]._PosX, NewMap._Grounds[i]._PosY, NewMap._Grounds[i]._Type);
        }

        SetWaypoints();

        // 처음에 열려 있는 그라운드를 제외하고 전부 비활성화
        InitGrounds();
    }

    // 특정한 좌표에 Ground 생성
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

        var hx = x * XFOV;  //tan 계산을 미리 한 값

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

    // 그라운드 확장
    public void ShowNextGrounds()
    {
        int AddAttackRouteCnt = 0;

        for(int i = 0; i < AttackRouteCnt; i++)
        {
            // 스테이지가 끝났는데도 이 함수가 실행되면 안됨
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

            _Ground.StartEnemySpawners();

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

    public void InitGrounds()
    {
        int StartGround = Tables.GlobalSystem.Get("Start_Ground_Num")._Value;

        OpenGroundCnt = StartGround;

        for (int i = StartGround; i < Grounds.Count; i++)
        {
            Grounds[i].IsActive = false;
        }

        // 처음 시작 타일 들의 위치를 바탕으로 카메라 제한 업데이트
        for (int i = 0; i < StartGround; i++)
        {
            UpdateCameraLimit(Grounds[i]._Pos.PosX, Grounds[i]._Pos.PosY);
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

        Direction dir;

        switch (HouseTile.ParentGround.GroundType)
        {
            case EGroundType.TD:
                dir = Direction.D;
                break;
            case EGroundType.TL:
                dir = Direction.L;
                break;
            case EGroundType.TR:
                dir = Direction.R;
                break;
            case EGroundType.TU:
                dir = Direction.U;
                break;
            default : dir = Direction.R;
                break;
        }

        FindNextAttackRouteTile(HousePos, dir, 0);
    }

    public Pos SumPos(Pos p1, Pos p2)
    {
        return new Pos { PosX = p1.PosX + p2.PosX, PosY = p1.PosY + p2.PosY };
    }

    // dir은 이 타일이 어느 방향으로부터 확장되어 왔는가
    void FindNextAttackRouteTile(Pos pos, Direction InDir, int Route)
    {
        Tile CurTile = GetTileInMap(pos);

        CurTile.IsSelectedAttackRoute = true;

        bool IsBranchGround = false;

        bool IsBranchTile = false;

        if (CheckBrach(CurTile.ParentGround.GroundType) == 1)
        {
            IsBranchGround = true;
        }

        // 모든 공격로에 웨이포인트 소환하기
        /*
        var obj = ObjectPools.Instance.GetPooledObject("Waypoint");
        obj.transform.parent = CurTile.transform;
        obj.transform.localPosition = Vector3.zero;
        */

        foreach (Direction OutDir in _Direction.Keys)
        {
            Pos NextPos = SumPos(pos, _Direction[OutDir]);

            Tile NextTile = GetTileInMap(NextPos);

            if (NextTile == null)
            {
                continue;
            }

            // 아직 선택된 공격로가 아닐때만 실행
            if (NextTile.TileType == ETileType.AttackRoute && !NextTile.IsSelectedAttackRoute)
            {
                // 그라운드가 바뀌면 에너미 스포너 생성
                if(CurTile.ParentGround != NextTile.ParentGround)
                {
                    CreateEnemySpawner(CurTile.transform, NextTile.transform, Route);
                }


                // 다음 타일이 직선이면
                if(OutDir == InDir)
                {
                    if (IsBranchTile)
                    {
                        Debug.Log($"분기 발생 {pos.PosX} , {pos.PosY}");

                        FindNextAttackRouteTile(NextPos, OutDir, MakeBranch(Route, CurTile.transform));
                    }
                    else
                    {
                        IsBranchTile = true;

                        FindNextAttackRouteTile(NextPos, OutDir, Route);
                    }
                    
                    continue;
                }

                // 다음 타일 위치가 꺽였을 때 웨이포인트 생성
                Waypoints.points[Route].Add(CurTile.transform);
                var obj1 = ObjectPools.Instance.GetPooledObject("Waypoint");
                obj1.transform.SetParent(CurTile.transform);
                obj1.transform.localPosition = Vector3.zero;
                Debug.Log($"Spawn Waypoint {pos.PosX} , {pos.PosY}");

                // 타일이 나눠졌을 때
                if (IsBranchTile)
                {
                    Debug.Log($"분기 발생 {pos.PosX} , {pos.PosY}");

                    FindNextAttackRouteTile(NextPos, OutDir, MakeBranch(Route, CurTile.transform));
                }
                else
                {
                    // 이 그라운드가 브랜치가 있는 그라운드이면
                    if (IsBranchGround)
                    {
                        IsBranchTile = true;
                    }

                    FindNextAttackRouteTile(NextPos, OutDir, Route);
                }
            }
        }
    }

    int MakeBranch(int Route, Transform TileTransform)
    {
        // 이전까지 루트 내용 복사
        Waypoints.points.Add(Waypoints.points[Route]);

        AttackRouteCnt = Waypoints.points.Count - 1;

        Waypoints.points[AttackRouteCnt].Add(TileTransform);

        var obj2 = ObjectPools.Instance.GetPooledObject("Waypoint");

        obj2.transform.SetParent(TileTransform);

        obj2.transform.localPosition = Vector3.zero;

        return AttackRouteCnt;
    }

    public void CreateEnemySpawner(Transform cur, Transform next, int Route)
    {
        var Spawner = ObjectPools.Instance.GetPooledObject("EnemySpawner");

        Spawner.transform.SetParent(cur);

        Spawner.transform.position = next.position;

        var ES = Spawner.GetComponent<EnemySpawner>();

        ES.Route = Route;

        ES.SpawnPoint = ES.transform;
    }
}
