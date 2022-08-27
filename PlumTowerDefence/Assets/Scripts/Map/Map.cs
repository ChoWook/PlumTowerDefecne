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

    public int OpenGroundCnt = 0;                    // 몇 개의 그라운드가 열렸는지

    public int AttackRouteCnt = 1;                   // 몇개의 갈래로 공격로가 형성되었는지

    public int HoleEmptyLandCnt = 0;                 // 전체 맵 중 비어있는 평지의 수

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

    // 특정한 좌표에 Ground 생성
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

    // 그라운드 확장
    public void ShowNextGrounds()
    {
        for(int i = 0; i < AttackRouteCnt; i++)
        {
            // 스테이지가 끝났는데도 이 함수가 실행되면 안됨
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

    // 처음 시작 그라운드를 제외한 나머지 그라운드 비활성화
    public void HideGrounds()
    {
        //TODO 몇개의  그라운드를 처음 시작 그라운드로 할지 정해야 함

        int StartGround = 3;

        OpenGroundCnt = StartGround;

        for (int i = StartGround; i < Grounds.Count; i++)
        {
            Grounds[i].IsActive = false;
        }
    }
}
