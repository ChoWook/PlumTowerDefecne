using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField]
    int GroundSize = 7;

    Tile[,] Tiles;


    private void Start()
    {
        InitTiles();
    }

    void InitTiles()
    {
        Tiles = new Tile[GroundSize, GroundSize];

        for(int i = 0; i < Tiles.GetLength(0); i++)
        {
            for(int j = 0; j < Tiles.GetLength(1); j++)
            {
                Tiles[i, j] = new Tile();
            }
        }
    }

    public void SetGroundPattern(int id)
    {
        if(Tiles == null)
        {
            Debug.Log("Tiles == null");
            InitTiles();
        }

        for(int i = 0; i < GroundSize; i++)
        {
            for(int j = 0; j < GroundSize; j++)
            {
                var tmp = Tables.GroundPattern.Get(id)._Tiles[(i * GroundSize) + j];
                Tiles[i, j].TileType = tmp;
                Debug.Log(Tiles[i, j].TileType);
            }
        }
    }
}
