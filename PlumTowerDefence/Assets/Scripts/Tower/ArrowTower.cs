using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTower : Tower
{

    private void Awake()
    {
        Setstat();  
    }


    public void Setstat()
    {
        TowerID = Tables.Tower.Get(0)._ID;
        TowerName = Tables.Tower.Get(0)._Name;
        AttackSpecialization = Tables.Tower.Get(0)._AttackSepcialization;
        TypeID = Tables.Tower.Get(0)._Type;
        Size = Tables.Tower.Get(0)._Size;
        AttackStat = Tables.Tower.Get(0)._Attack;
        SpeedStat = Tables.Tower.Get(0)._Speed;
        ProjectileSpeed = Tables.Tower.Get(0)._ProjectileSpeed;
        UpgradeStat = Tables.Tower.Get(0)._UpgradeStat;
        UpgradeAmount = Tables.Tower.Get(0)._UpgradeAmount;
        UpgradePrice = Tables.Tower.Get(0)._UpgradePrice;
        Range = Tables.Tower.Get(0)._Range;
        Price = Tables.Tower.Get(0)._Price;
    }
}
