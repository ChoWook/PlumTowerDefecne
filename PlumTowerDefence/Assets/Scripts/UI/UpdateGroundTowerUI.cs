using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateGroundTowerUI : MonoBehaviour
{
    private Tower[] selectedTower;
    private Tower _tower;

    public void SetTower(Tower tower)
    {
        _tower = tower;
    }

    private void GetTowers()
    {
        Tile _tile = _tower.belowTile;
        foreach (var tile in _tile.ParentGround.Tiles)
        {
            //if(tile.위의 오브젝트.name == _tower.name)
            //{
                //selectedTower += tile.위의 오브젝트;
            //}
        }
    }
}
