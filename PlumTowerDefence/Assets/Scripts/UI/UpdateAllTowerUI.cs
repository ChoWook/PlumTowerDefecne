using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateAllTowerUI : MonoBehaviour
{
    [SerializeField] private Texture2D targetCursor;
    
    private Tower _tower;
    private List<Tower> selectedTower = new List<Tower>();

    public void SetTower(Tower tower)
    {
        _tower = tower;
        GetTowers();
    }
    
    private void GetTowers()
    {
        selectedTower.Clear();
        for (int i = 0; i < Map.Instance.OpenGroundCnt; i++)
        {
            for (int j = 0; j < Map.Instance.Grounds[i].Tiles.Length; j++)
            {
                Tile tile = Map.Instance.Grounds[i].Tiles[j];
                if (tile.GetObjectOnTile() != null)
                {
                    var tower = tile.GetObjectOnTile().GetComponent<Tower>();
                    if (tower != null)
                    {
                        if(tower.TowerName.CompareTo(_tower.TowerName)==0)
                        {
                            selectedTower.Add(tower);
                        }
                    }
                }
            }
        }
    }

    public void SetTarget()
    {
        ChangeMouseCursor();
        GameManager.instance.isSettingTarget = 3;
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
}
