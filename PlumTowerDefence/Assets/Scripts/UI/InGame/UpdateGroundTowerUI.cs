using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateGroundTowerUI : MonoBehaviour
{
    private List<Tower> selectedTower = new List<Tower>();
    private Tower _tower;
    
    [SerializeField] private Texture2D targetCursor;

    public void SetTower(Tower tower)
    {
        _tower = tower;
        GetTowers();
    }

    private void GetTowers()
    {
        selectedTower.Clear();
        Tile _tile = _tower.belowTile;
        foreach (var tile in _tile.ParentGround.Tiles)
        {
            if (tile.GetObjectOnTile() != null)
            {
                var tower = tile.GetObjectOnTile().GetComponent<Tower>();
                if (tower != null)
                {
                    if(tower.TowerName.CompareTo(_tower.TowerName)==0)
                    {
                        selectedTower.Add(tower);

                        tower.IsSelected(true);
                    }
                }
            }
        }
        
        Debug.Log("SelectedTower count : " + selectedTower.Count);
    }

    public void SetTarget()
    {
        ChangeMouseCursor();
        GameManager.instance.isSettingTarget = 2;
    }
    private void ChangeMouseCursor()
    {
        Cursor.SetCursor(targetCursor,Vector2.zero, CursorMode.ForceSoftware);
    }

    public void AttackEnemy(GameObject obj)
    {
        foreach (var tower in selectedTower)
        {
            tower.SetTarget(obj);
        }

        GameManager.instance.isSettingTarget = 0;
    }

    public void ClearTowers()
    {
        foreach (var tower in selectedTower)
        {
            tower.IsSelected(false);
        }
    }
}
