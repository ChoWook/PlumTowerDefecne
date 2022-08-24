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
        Tables.Load();
        SetGroundPattern(Pattern);
        SetTilesPos();
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
        SetGroundPattern(SelectRandomPattern(type));
    }

    void SetGroundPattern(int id = 0)
    {
        Pattern = id;
        // id == 0 �̸� Tile ���ֱ�
        if(id == 0)
        {
            for (int i = 0; i < GroundSize * GroundSize; i++)
            {
                Tiles[i].gameObject.SetActive(id != 0);
            }
        }
        

        if (Tables.GroundPattern.Get(id) == null)
        {
            return;
        }


        for (int i = 0; i < GroundSize * GroundSize; i++)
        {
            Tiles[i].TileType = Tables.GroundPattern.Get(id)._Tiles[i];
        }  
    }

    // type�� �´� ���� �߿� �����ϰ� ����
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
}
