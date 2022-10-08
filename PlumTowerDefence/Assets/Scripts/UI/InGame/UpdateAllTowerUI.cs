using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateAllTowerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI TowerName;
    [SerializeField] private TextMeshProUGUI TowerLevel;
    [SerializeField] private TextMeshProUGUI TowerDamage;
    [SerializeField] private TextMeshProUGUI TowerFireRate;
    //[SerializeField] private TextMeshProUGUI TowerPriority;
    [SerializeField] private TextMeshProUGUI TowerUpgrade;
    [SerializeField] private TextMeshProUGUI TowerMove;
    [SerializeField] private TextMeshProUGUI TowerDemolish;

    [SerializeField] private Texture2D targetCursor;
    
    private Tower _tower;
    private List<Tower> selectedTower = new List<Tower>();

    int upgradePrice = 0;

    int sellPrice = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            OnUpgradeBtnClick();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            OnSellBtnClick();
        }
    }
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

                            tower.IsSelected(true);
                        }
                    }
                }
            }
        }

        UpdateTowerInfo();
        Debug.Log("SelectedTower count : " + selectedTower.Count);
    }

    public void SetTarget()
    {
        ChangeMouseCursor();
        GameManager.instance.IsSettingTarget = 3;
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
        
        GameManager.instance.IsSettingTarget = 0;
    }

    public void ClearTowers()
    {
        foreach (var tower in selectedTower)
        {
            tower.IsSelected(false);
        }
    }

    void UpdateTowerInfo()
    {
        upgradePrice = 0;

        sellPrice = 0;

        for (int i = 0; i < selectedTower.Count; i++)
        {
            if (selectedTower[i].UpgradeCount < Tables.GlobalSystem.Get("Tower_Upgrade_Max")._Value)
            {
                upgradePrice += selectedTower[i].UpgradePrice;
            }

            sellPrice += selectedTower[i].SellPrice;
        }
        TowerName.text = $"{Tables.Tower.Get(_tower.TowerName)._Korean} X {selectedTower.Count}";

        TowerLevel.text = string.Format(Tables.StringUI.Get(TowerLevel.gameObject.name)._Korean, "*");
        TowerDamage.text = string.Format(Tables.StringUI.Get(TowerDamage.gameObject.name)._Korean, "*");
        TowerFireRate.text =
            string.Format(Tables.StringUI.Get(TowerFireRate.gameObject.name)._Korean, "*");
        //TowerPriority.text =
        //    string.Format(Tables.StringUI.Get(TowerPriority.gameObject.name)._Korean, _tower.AttackPriorityID);

        // TODO 최대 업글했을 때 String 추가 필요
        if (upgradePrice == 0)
        {
            TowerUpgrade.text =
                string.Format(Tables.StringUI.Get(TowerUpgrade.gameObject.name)._Korean, "MAX");
        }
        else
        {
            TowerUpgrade.text =
                string.Format(Tables.StringUI.Get(TowerUpgrade.gameObject.name)._Korean, upgradePrice);
        }
        TowerMove.text = string.Format(Tables.StringUI.Get(TowerMove.gameObject.name)._Korean, "*");
        TowerDemolish.text =
            string.Format(Tables.StringUI.Get(TowerDemolish.gameObject.name)._Korean, sellPrice);

    }

    public void OnUpgradeBtnClick()
    {
        if (upgradePrice > GameManager.instance.Money)
        {
            return;
        }

        int upgradeTowerCnt = 0;

        for (int i = 0; i < selectedTower.Count; i++)
        {
            if (selectedTower[i].UpgradeCount < Tables.GlobalSystem.Get("Tower_Upgrade_Max")._Value)
            {
                selectedTower[i].UpgradeTower();

                upgradeTowerCnt++;
            }
        }

        if (upgradeTowerCnt > 0)
        {
            UpdateTowerInfo();
        }
    }

    public void OnSellBtnClick()
    {
        if (selectedTower.Count == 0)
        {
            return;
        }

        for (int i = 0; i < selectedTower.Count; i++)
        {
            selectedTower[i].SellTower();
        }

        gameObject.SetActive(false);
    }
}
