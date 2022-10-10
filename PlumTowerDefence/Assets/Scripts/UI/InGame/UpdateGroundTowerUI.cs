using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateGroundTowerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI TowerName;
    [SerializeField] private TextMeshProUGUI TowerLevel;
    [SerializeField] private TextMeshProUGUI TowerDamage;
    [SerializeField] private TextMeshProUGUI TowerFireRate;
    [SerializeField] private TextMeshProUGUI TowerAbility;
    //[SerializeField] private TextMeshProUGUI TowerPriority;
    [SerializeField] private TextMeshProUGUI TowerUpgrade;
    [SerializeField] private TextMeshProUGUI TowerMove;
    [SerializeField] private TextMeshProUGUI TowerDemolish;


    private List<Tower> selectedTower = new List<Tower>();
    private Tower _tower;
    
    [SerializeField] private Texture2D targetCursor;

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

        UpdateTowerInfo();
    }

    public void SetTarget()
    {
        ChangeMouseCursor();
        GameManager.instance.IsSettingTarget = 2;
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

        for(int i = 0; i < selectedTower.Count; i++)
        {
            if(selectedTower[i].UpgradeCount < Tables.GlobalSystem.Get("Tower_Upgrade_Max")._Value)
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

        // TODO �ִ� �������� �� String �߰� �ʿ�
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

        EnableInfoText();
    }

    void EnableInfoText()
    {
        if (_tower == null)
        {
            return;
        }

        switch (_tower.TowerName)
        {
            case ETowerName.Arrow:
                TowerDamage.gameObject.SetActive(true);
                TowerFireRate.gameObject.SetActive(true);
                TowerAbility.gameObject.SetActive(false);
                break;

            case ETowerName.Hourglass:
                TowerDamage.gameObject.SetActive(false);
                TowerFireRate.gameObject.SetActive(false);
                TowerAbility.gameObject.SetActive(true);
                TowerAbility.text = string.Format(Tables.StringUI.Get("Hourglass_Tower_Reduced_Speed_UI")._Korean, "*");
                break;

            case ETowerName.Poison:
                TowerDamage.gameObject.SetActive(false);
                TowerFireRate.gameObject.SetActive(false);
                TowerAbility.gameObject.SetActive(true);
                TowerAbility.text = string.Format(Tables.StringUI.Get("Poison_Tower_Damage_UI")._Korean, "*");
                break;

            case ETowerName.Flame:
                TowerDamage.gameObject.SetActive(true);
                TowerFireRate.gameObject.SetActive(true);
                TowerAbility.gameObject.SetActive(false);
                break;

            case ETowerName.AttackBuff:
                TowerDamage.gameObject.SetActive(false);
                TowerFireRate.gameObject.SetActive(false);
                TowerAbility.gameObject.SetActive(true);
                TowerAbility.text = string.Format(Tables.StringUI.Get("Attack_Buff_Tower_Ability_UI")._Korean, "*");
                break;

            case ETowerName.SpeedBuff:
                TowerDamage.gameObject.SetActive(false);
                TowerFireRate.gameObject.SetActive(false);
                TowerAbility.gameObject.SetActive(true);
                TowerAbility.text = string.Format(Tables.StringUI.Get("Speed_Buff_Tower_Ability_UI")._Korean, "*");
                break;

            case ETowerName.Laser:
                TowerDamage.gameObject.SetActive(true);
                TowerFireRate.gameObject.SetActive(true);
                TowerAbility.gameObject.SetActive(true);
                TowerAbility.text = string.Format(Tables.StringUI.Get("Laser_Tower_Rate_UI")._Korean, "*");
                break;

            case ETowerName.Missile:
                TowerDamage.gameObject.SetActive(true);
                TowerFireRate.gameObject.SetActive(true);
                TowerAbility.gameObject.SetActive(false);
                break;

            case ETowerName.Electric:
                TowerDamage.gameObject.SetActive(true);
                TowerFireRate.gameObject.SetActive(true);
                TowerAbility.gameObject.SetActive(true);
                TowerAbility.text = string.Format(Tables.StringUI.Get("Electric_Tower_Enemy_Attack_UI")._Korean, "*");
                break;

            case ETowerName.Gatling:
                TowerDamage.gameObject.SetActive(true);
                TowerFireRate.gameObject.SetActive(true);
                TowerAbility.gameObject.SetActive(false);
                break;

            case ETowerName.Cannon:
                TowerDamage.gameObject.SetActive(true);
                TowerFireRate.gameObject.SetActive(true);
                TowerAbility.gameObject.SetActive(false);
                break;

            case ETowerName.Bomb:
                TowerDamage.gameObject.SetActive(true);
                TowerFireRate.gameObject.SetActive(false);
                TowerAbility.gameObject.SetActive(false);
                break;
        }
    }

    public void OnUpgradeBtnClick()
    {
        if(upgradePrice > GameManager.instance.Money)
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

        if(upgradeTowerCnt > 0)
        {
            UpdateTowerInfo();
        }
    }

    public void OnSellBtnClick()
    {
        if(selectedTower.Count == 0)
        {
            return;
        }

        for(int i = 0; i< selectedTower.Count; i++)
        {
            selectedTower[i].SellTower();
        }

        gameObject.SetActive(false);
    }
}
