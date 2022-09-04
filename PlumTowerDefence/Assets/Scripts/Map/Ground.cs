using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[ExecuteInEditMode]
public class Ground : MonoBehaviour
{
    [SerializeField] GameObject GridLine;

    [SerializeField] GameObject CastlePrefab;

    [SerializeField] int GroundSize = 7;

    [SerializeField] int GroundScale = 10;


    public Pos _Pos = new Pos();

    public int Pattern = 1;

    public EGroundType GroundType = EGroundType.LR;

    public Tile[] Tiles;

    public List<Tile> EmptyLandTiles;

    public int EmptyLandTileCount = 0;

    public int ResourceTileCount = 0;

    public List<EnemySpawner> EnemySpawners = new();

    public Tile BranchTile;                       // 브랜치가 일어나는 타일

    bool _IsActive;

    int HouseTileIndex = -1;

    public bool IsActive
    {
        get { return _IsActive; }
        set
        {
            _IsActive = value;

            gameObject.SetActive(_IsActive);

        }
    }

#if UNITY_EDITOR

    private void Update()
    {
        if (!Application.isPlaying)
        {
            Tables.Load();
            SetGroundPattern(Pattern);
            SetTilesPos();
        }
        
    }

    void SetTilesPos()
    {
        for (int i = 0; i < Tiles.Length; i++)
        {
            Tiles[i]._GroundPos.PosX = i / GroundSize;
            Tiles[i]._GroundPos.PosY = i % GroundSize;

        }
    }
#endif



    public void SetGroundPattern(EGroundType type)
    {
        GroundType = type;

        SetGroundPattern(SelectRandomPattern(type));
    }

    void SetGroundPattern(int id = 0)
    {
        Pattern = id;
        // id == 0 이면 ground 꺼주기
        if(id == 0)
        {
            gameObject.SetActive(false);
        }
        

        if (Tables.GroundPattern.Get(id) == null)
        {
            return;
        }


        for (int i = 0; i < GroundSize * GroundSize; i++)
        {
            Tiles[i]._GroundPos.PosX = i / GroundSize;

            Tiles[i]._GroundPos.PosY = i % GroundSize;

            Tiles[i]._MapPos.PosX = _Pos.PosX * 7 + (Tiles[i]._GroundPos.PosY - 3);

            Tiles[i]._MapPos.PosY = _Pos.PosY * 7 - (Tiles[i]._GroundPos.PosX - 3);

            Tiles[i].SetTileType(Tables.GroundPattern.Get(id)._Tiles[i]);

            Tiles[i].ParentGround = this;

            if(Tiles[i].CheckTileType(ETileType.Land))
            {
                EmptyLandTileCount++;
            }
            else if(Tiles[i].CheckTileType(ETileType.House))
            {
                HouseTileIndex = i;
            }
        }  
    }

    public void CreateCastle()
    {
        if(HouseTileIndex == -1) return;

        GameObject house = Instantiate(CastlePrefab, Tiles[HouseTileIndex].transform);

        Tiles[HouseTileIndex].SetObjectOnTile(house, 3);

        float YAngle = 0;

        switch (GroundType)
        {
            case EGroundType.TU:
                YAngle = 0;
                break;
            case EGroundType.TD:
                YAngle = 180;
                break;
            case EGroundType.TL:
                YAngle = 270;
                break;
            case EGroundType.TR:
                YAngle = 90;
                break;
        }
        house.transform.RotateAround(Tiles[HouseTileIndex].transform.position, Vector3.up, YAngle);
    }

    // type에 맞는 패턴 중에 랜덤하게 선택
    int SelectRandomPattern(EGroundType type)
    {
        var list = Tables.GroundPattern.Get(type);

        if(list.Count == 0)
        {
            return 0;
        }

        return list[Random.Range(0, list.Count)]._ID;
    }

    public void HideGridLine()
    {
        GridLine.SetActive(false);
    }

    public void ShowGridLine()
    {
        GridLine.SetActive(true);
    }

    public List<Tile> GetEmptyLandTilesInGround()
    {
        EmptyLandTiles.Clear();

        for (int i = 0; i < Tiles.Length; i++)
        {
            if (Tiles[i].CheckTileType(ETileType.Land) && Tiles[i].GetObjectOnTile() == null)
            {
                EmptyLandTiles.Add(Tiles[i]);
            }
        }

        return EmptyLandTiles;
    }

    public void SetPosition(Pos Sender)
    {
        _Pos = Sender;

        transform.localPosition = new Vector3(_Pos.PosX * GroundScale, 0, _Pos.PosY * GroundScale);
    }

    public Tile GetTileInMapPosition(Pos Sender)
    {
        Sender.PosX -= GroundSize * _Pos.PosX;
        Sender.PosY -= GroundSize * _Pos.PosY;

        Sender.PosX += 3;
        Sender.PosY -= 3;

        Sender.PosY = -Sender.PosY;

        return Tiles[Sender.PosX + GroundSize * Sender.PosY];
    }

    public void StartEnemySpawners(int idx)
    {
        if(idx == 0)
        {
            EnemySpawners[0].UpdateEnemySpawnCounts();
        }

        for(int i = 0; i < EnemySpawners.Count; i++)
        {
            EnemySpawners[i].SpawnWave();
        }
    }
}
