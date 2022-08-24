using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Ground : MonoBehaviour
{
    [SerializeField] GameObject GridLine;

    [SerializeField] int GroundSize = 7;

    public int Pattern = 1;

    public Tile[] Tiles;

#if UNITY_EDITOR

    private void Update()
    {
        if (!Application.isPlaying)
        {
            Debug.Log("Tables.Load");
            Tables.Load();
            SetGroundPattern(Pattern);
            SetTilesPos();
        }
    }

    void SetTilesPos()
    {
        for (int i = 0; i < GroundSize * GroundSize; i++)
        {
            Tiles[i].PosX = i / GroundSize;
            Tiles[i].PosY = i % GroundSize;
        }
    }
#endif

    public void SetGroundPattern(int id)
    {
        for (int i = 0; i < GroundSize * GroundSize; i++)
        {
            Tiles[i].TileType = Tables.GroundPattern.Get(id)._Tiles[i];
        }  
    }
}
