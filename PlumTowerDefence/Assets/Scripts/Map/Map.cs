using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using RTS_Cam;
using TMPro;

#region Position
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

public enum EDirection
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

#endregion

public class Map : MonoBehaviour
{
    #region Static Field
    public static Map Instance;
    #endregion

    #region Serialize Field
    [SerializeField] GameObject GroundPrefab;

    [SerializeField] MapGimmicSpawner GimmicSpawner;

    [SerializeField] GameObject EnemySpawnerPrefab;

    [SerializeField] GameObject Waypoint;

    [SerializeField] GameObject TreeEnvironment;

    [SerializeField] int TreesLayer = 2;

    [SerializeField] float MaxCameraHeight = 20;
    #endregion

    #region Public Field

    public int OpenGroundCnt = 0;                               // 몇 개의 그라운드가 열렸는지

    public int AttackRouteCnt = 1;                              // 몇개의 갈래로 공격로가 형성되었는지

    public int CurAttackRouteCnt = 1;

    public int HoleEmptyLandCnt = 0;                            // 전체 맵 중 비어있는 평지의 수

    [HideInInspector]
    public List<Ground> Grounds = new();                        // 생성되는 순서대로 저장되는 리스트

    [HideInInspector]
    public Dictionary<Pos, Ground> GroundsWithPos = new Dictionary<Pos, Ground>(new PosComparer());      // 그라운드의 위치를 키값으로 하는 딕셔너리

    [HideInInspector]
    public Dictionary<Pos, GameObject> TreesWithPos = new Dictionary<Pos, GameObject>(new PosComparer());

    public Dictionary<EDirection, Pos> _Direction;               // 방향에 대한 값을 저장해둔 딕셔너리

    #endregion

    #region Private Field

    RTS_Camera MainCamera;                                      // 카메라 이동을 위한 카메라

    List<Tile> EmptyLandTiles = new();                          // 기믹을 설치하기 위해 빈 땅을 미리 캐싱

    List<Ground> CurEnemySpawnerGrounds = new();                // 이번 스테이지에서 몹이 나와야할 그라운드

    const int GroundSize = 10;

    float YFOV = 20;

    float XFOV = 32.9f;

    #endregion

    [Header("Debug")]
    [SerializeField] TMP_InputField MapInput;

    #region Unity Function

    private void Awake()
    {
        Tables.Load();

        MainCamera = Camera.main.GetComponent<RTS_Camera>();

        Instance = this;

        _Direction = new();
        _Direction.Add(EDirection.R, new Pos { PosX = 1, PosY = 0 });        // 동서남북
        _Direction.Add(EDirection.L, new Pos { PosX = -1, PosY = 0 });
        _Direction.Add(EDirection.D, new Pos { PosX = 0, PosY = -1 });
        _Direction.Add(EDirection.U, new Pos { PosX = 0, PosY = 1 });

        _Direction.Add(EDirection.UR, new Pos { PosX = 1, PosY = 1 });
        _Direction.Add(EDirection.UL, new Pos { PosX = -1, PosY = 1 });
        _Direction.Add(EDirection.DR, new Pos { PosX = 1, PosY = -1 });
        _Direction.Add(EDirection.DL, new Pos { PosX = -1, PosY = -1 });



        // TODO 게임메니저로 넘겨야 함
        Cursor.lockState = CursorLockMode.Confined;

        XFOV = 1 / Mathf.Tan(XFOV * Mathf.Deg2Rad);

        YFOV = 1 / Mathf.Tan(YFOV * Mathf.Deg2Rad);

    }

    #endregion

    #region EventHandler

#if UNITY_EDITOR
    public void OnSetMapPatternBtnClick()
    {
        if (MapInput.text != null)
        {
            int tmp;

            int.TryParse(MapInput.text, out tmp);

            SetMapPattern(tmp);
        }
    }
#endif

    #endregion

    #region Init

    public void ChooseRandomMapPattern()
    {
        int RandomID = Random.Range(1, Tables.MapPattern.GetSzie() + 1);

        SetMapPattern(RandomID);

        HideAllGridLine();
    }

    void SetMapPattern(int id = 0)
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

        // 게임이 시작됐으니 게임매니저 정보 초기화
        InitGameManager();
    }

    void InitGameManager()
    {
        // 유닛 타일 사이즈 계산
        GameManager.instance.UnitTileSize = GetTileInMap(new Pos { PosX = 1, PosY = 0 }).transform.position.x
            - GetTileInMap(new Pos { PosX = 0, PosY = 0 }).transform.position.x;

        GameManager.instance.InitGame();
    }

    void InitCameraLimit(int x, int y)
    {
        MainCamera.MinX = -x * GroundSize;
        MainCamera.MaxX = x * GroundSize;

        MainCamera.MinY = -y * GroundSize;
        MainCamera.MaxY = y * GroundSize;
    }

    // 특정한 좌표에 Ground 생성
    void AddGround(int x, int y, EGroundType type)
    {
        Ground NewGround = Instantiate(GroundPrefab, transform).GetComponent<Ground>();

        Pos NewPos = new Pos { PosX = x, PosY = y };

        NewGround.SetPosition(NewPos);

        NewGround.SetGroundPattern(type);

        Grounds.Add(NewGround);

        GroundsWithPos.Add(NewPos, NewGround);
    }

    void InitGrounds()
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

            CreateTreesAroundGround(Grounds[i]._Pos, TreesLayer);
        }

        // 맨 첫 그라운드에 하우스 생성
        Grounds[0].CreateCastle();
    }

    void CreateEnemySpawner(Tile cur, Transform next, int Route)
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

    #endregion

    #region Set Waypoints
    public void SetWaypoints()
    {
        Tile HouseTile = GetHouseTile();

        Waypoints.points[0].Add(HouseTile.transform);

        EDirection HouseDir;

        switch (HouseTile.ParentGround.GroundType)
        {
            case EGroundType.TD:
                HouseDir = EDirection.D;
                break;
            case EGroundType.TL:
                HouseDir = EDirection.L;
                break;
            case EGroundType.TR:
                HouseDir = EDirection.R;
                break;
            case EGroundType.TU:
                HouseDir = EDirection.U;
                break;
            default:
                HouseDir = EDirection.R;
                break;
        }

        FindNextAttackRouteTile(HouseTile._MapPos, HouseDir, 0);
    }

    // dir은 이 타일이 어느 방향으로부터 확장되어 왔는가
    void FindNextAttackRouteTile(Pos pos, EDirection InDir, int Route)
    {
        Tile CurTile = GetTileInMap(pos);

        CurTile.IsSelectedAttackRoute = true;

        // 모든 공격로에 웨이포인트 소환하기
        /*
        var obj = ObjectPools.Instance.GetPooledObject("Waypoint");
        obj.transform.parent = CurTile.transform;
        obj.transform.localPosition = Vector3.zero;
        */

        // 해당 타일이 코너도 아니고 분기점도 아닌지 검사
        List<EDirection> NextDirs = new();

        for (EDirection OutDir = EDirection.R; OutDir <= EDirection.U; OutDir++)
        {
            Pos NextPos = pos.SumPos(_Direction[OutDir]);

            Tile NextTile = GetTileInMap(NextPos);

            if (NextTile == null)
            {
                continue;
            }

            if (NextTile.CheckTileType(ETileType.AttackRoute) && NextTile.IsSelectedAttackRoute == false)
            {
                NextDirs.Add(OutDir);
            }
        }

        // 길이 없음
        if (NextDirs.Count == 0)
        {
            return;
        }

        // 길이 1개
        if (NextDirs.Count == 1)
        {
            Pos NextPos = pos.SumPos(_Direction[NextDirs[0]]);

            Tile NextTile = GetTileInMap(NextPos);

            // 직선이면
            if (InDir == NextDirs[0])
            {
                // 다음 타일이 다른 그라운드면 에너미스포너 생성
                if (CurTile.ParentGround != NextTile.ParentGround)
                {
                    CreateEnemySpawner(CurTile, NextTile.transform, Route);
                }
            }
            // 코너이면
            else
            {
                SpawnWaypoint(CurTile.transform, Route);
            }

            FindNextAttackRouteTile(NextPos, NextDirs[0], Route);
        }

        // 분기점이면
        if (NextDirs.Count == 2)
        {
            SpawnWaypoint(CurTile.transform, Route);

            // 브랜치 타일에서 사용할 변수
            CurTile.WaypointRoute = Route;
            CurTile.WaypointIndex = Waypoints.points[Route].Count - 1;

            FindNextAttackRouteTile(pos.SumPos(_Direction[NextDirs[0]]), NextDirs[0], Route);

            FindNextAttackRouteTile(pos.SumPos(_Direction[NextDirs[1]]), NextDirs[1], MakeBranch(CurTile));
        }
    }

    int MakeBranch(Tile CurTile)
    {
        if (CurTile.WaypointRoute == -1 || CurTile.WaypointIndex == -1)
        {
            Debug.LogWarning("Make Branch Warning");
        }

        // 이전까지 루트 내용 복사
        Waypoints.points.Add(CopyList(CurTile));

        AttackRouteCnt = Waypoints.points.Count - 1;

        Waypoints.points[AttackRouteCnt].Add(CurTile.transform);

        return AttackRouteCnt;
    }

    public void SpawnWaypoint(Transform CurTransform, int Route)
    {
        Waypoints.points[Route].Add(CurTransform);

        var obj1 = ObjectPools.Instance.GetPooledObject("Waypoint");

        obj1.transform.SetParent(CurTransform);

        obj1.transform.localPosition = Vector3.zero;
    }
    #endregion

    #region Camera Move
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

        float tmpHeight = ((hx > hy) ? hx : hy) / 4;

        tmpHeight = (tmpHeight > MaxCameraHeight) ? MaxCameraHeight : tmpHeight;

        MainCamera.maxHeight = tmpHeight;
    }

    #endregion

    #region Ground Expand
    // 그라운드 확장
    public void ShowNextGrounds()
    {
        int AddAttackRouteCnt = 0;

        CurEnemySpawnerGrounds.Clear();

        for(int i = 0; i < CurAttackRouteCnt; i++)
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

            CurEnemySpawnerGrounds.Add(_Ground);

            SpawnAllGimmick(false);

            // 그라운드가 생성될 위치에 트리가 있으면 지우기
            GameObject Trees;

            TreesWithPos.TryGetValue(_Ground._Pos, out Trees);

            if(Trees != null)
            {
                TreesWithPos.Remove(_Ground._Pos);

                ObjectPools.Instance.ReleaseObjectToPool(Trees);
            }

            // 확장된 그라운드 주변 나무 생성
            CreateTreesAroundGround(_Ground._Pos, TreesLayer);

            // 확장된 그라운드 표시
            _Ground.ShowExpandedLine();

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

    void SpawnAllGimmick(bool IsExistedMap)
    {
        for (EMapGimmickType type = EMapGimmickType.LaneBuff; type <= EMapGimmickType.Treasure; type++)
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

    void CreateTreesAroundGround(Pos GroundPos, int Layer = 1)
    {
        if (Layer <= 0)
        {
            return;
        }

        for (EDirection i = EDirection.R; i <= EDirection.DR; i++)
        {
            Ground ground;

            Pos TreesPos = GroundPos.SumPos(_Direction[i]);

            GroundsWithPos.TryGetValue(TreesPos, out ground);

            // 그라운드가 비활성화일 때만 생성
            if (ground == null || ground.gameObject.activeSelf == false)
            {
                GameObject Trees;

                TreesWithPos.TryGetValue(TreesPos, out Trees);

                // 트리가 이미 생성되어 있었어도 그 너머에 트리를 소환할 필요가 있음
                CreateTreesAroundGround(TreesPos, Layer - 1);

                // 이미 트리가 생성되어 있으면 continue
                if (Trees != null)
                {
                    continue;
                }

                Trees = ObjectPools.Instance.GetPooledObject("Trees");

                SetTreesPosition(Trees.transform, TreesPos);

                TreesWithPos.TryAdd(TreesPos, Trees);

                if (!GameManager.instance.EnvironmentChecked)
                {
                    Trees.SetActive(false);
                }
            }
        }
    }
    #endregion

    #region Get

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

    public List<Tile> GetEmptyLandTilesInMap()
    {
        EmptyLandTiles.Clear();

        for (int i = 0; i < OpenGroundCnt; i++)
        {
            EmptyLandTiles.AddRange(Grounds[i].GetEmptyLandTilesInGround());
        }

        return EmptyLandTiles;
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

        Ground ground;
        GroundsWithPos.TryGetValue(pos, out ground);

        return ground?.GetTileInMapPosition(Sender);
    }

    Tile GetHouseTile()
    {
        for(int i = 0; i < Grounds[0].Tiles.Length; i++)
        {
            if (Grounds[0].Tiles[i].CheckTileType(ETileType.House))
            {
                return Grounds[0].Tiles[i];
            }
            
        }
        return null;
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


    #endregion

    #region Set
    public void HideAllGridLine()
    {
        for (int i = 0; i < Grounds.Count; i++)
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

    public void SetTreesPosition(Transform Trees, Pos Sender)
    {
        Trees.SetParent(transform);

        Trees.transform.localPosition = new Vector3(Sender.PosX * GroundSize, 0, Sender.PosY * GroundSize);

        Trees.SetParent(TreeEnvironment.transform);
    }

    public void ShowTreeEnvironmnet()
    {
        TreeEnvironment.SetActive(true);
    }

    public void HideTreeEnvironmnet()
    {
        TreeEnvironment.SetActive(false);
    }

    #endregion
}
