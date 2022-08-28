using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Ground : MonoBehaviour
{
    [SerializeField] GameObject GridLine;

    [SerializeField] int GroundSize = 7;

    public int Pattern = 1;

    public EGroundType GroundType = EGroundType.LR;

    public Tile[] Tiles;

    public List<Tile> EmptyLandTiles;

    public int EmptyLandTileCount = 0;

    public int ResourceTileCount = 0;

    bool _IsActive;

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
            Tiles[i].PosX = i / GroundSize;
            Tiles[i].PosY = i % GroundSize;
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
            Tiles[i].TileType = Tables.GroundPattern.Get(id)._Tiles[i];

            if(Tiles[i].TileType == ETileType.Land)
            {
                EmptyLandTileCount++;
            } 
        }  
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
            if (Tiles[i].TileType == ETileType.Land && Tiles[i].ObjectOnTile == null)
            {
                EmptyLandTiles.Add(Tiles[i]);
            }
        }

        return EmptyLandTiles;
    }
}
