using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdateTowerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI TowerName;
    [SerializeField] private TextMeshProUGUI TowerLevel;
    [SerializeField] private TextMeshProUGUI TowerDamage;
    [SerializeField] private TextMeshProUGUI TowerFireRate;
    [SerializeField] private TextMeshProUGUI TowerPriority;
    [SerializeField] private TextMeshProUGUI TowerUpgrade;
    [SerializeField] private TextMeshProUGUI TowerMove;
    [SerializeField] private TextMeshProUGUI TowerDemolish;
    private Tower _tower;

    [SerializeField] private Texture2D targetCursor;

    private void Update()
    {
        if (GameManager.instance.isClickedTower && Input.GetKeyDown(KeyCode.A))
        {
            SetTarget();
        }
    }

    public void SetTower(Tower tower)
    {
        _tower = tower;

        _tower.IsSelected(true);

        UpdateTowerInfo();
    }

    private void UpdateTowerInfo()
    {
        TowerName.text = _tower.TowerName.ToString();

        TowerLevel.text = string.Format(Tables.StringUI.Get(TowerLevel.gameObject.name)._Korean, _tower.UpgradeCount);
        TowerDamage.text = string.Format(Tables.StringUI.Get(TowerDamage.gameObject.name)._Korean, _tower.AttackStat);
        TowerFireRate.text =
            string.Format(Tables.StringUI.Get(TowerFireRate.gameObject.name)._Korean, _tower.SpeedStat);
        TowerPriority.text =
            string.Format(Tables.StringUI.Get(TowerPriority.gameObject.name)._Korean, _tower.AttackPriorityID);
        TowerUpgrade.text =
            string.Format(Tables.StringUI.Get(TowerUpgrade.gameObject.name)._Korean, _tower.UpgradePrice);
        TowerMove.text = string.Format(Tables.StringUI.Get(TowerMove.gameObject.name)._Korean, _tower.MovePrice);
        TowerDemolish.text =
            string.Format(Tables.StringUI.Get(TowerDemolish.gameObject.name)._Korean, _tower.SellPrice);

    }

    public void SetTarget()
    {
        ChangeMouseCursor();
        GameManager.instance.isSettingTarget = 1;
    }

    private void ChangeMouseCursor()
    {
        Cursor.SetCursor(targetCursor, Vector2.zero, CursorMode.ForceSoftware);
    }

    public void AttackEnemy(GameObject obj)
    {
        _tower.SetTarget(obj);
        
        GameManager.instance.isSettingTarget = 0;
    }

    public void ClearTower()
    {
        _tower?.IsSelected(false);
    }
}
