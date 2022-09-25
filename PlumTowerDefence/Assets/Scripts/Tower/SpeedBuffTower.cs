using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBuffTower : Tower
{
    public const string TowerTag = "Tower";

    public List<GameObject> TowerList = new List<GameObject>();


    private void Awake()
    {
        Setstat(ETowerName.SpeedBuff);
    }

    
    public override float AbilityStat
    {
        get
        {
            List<float> list = TowerUpgradeAmount.instance._SpeedBuffTowerStat.AbilityPlusModifier;

            float sum = 0f;

            for (int i = 0; i < list.Count; i++)
            {
                sum += list[i];
            }

            float multi = 1f;

            for (int i = 0; i < AbilityMultiModifier.Count; i++)
            {
                multi *= AbilityMultiModifier[i];
            }


            return (BaseAbilityStat + sum) * multi;
        }
    }
    
    // attackRange, additionalAttackDamage;





    protected override void UpdateTarget()
    {
        TowerList.Clear();

        GameObject[] Towers = GameObject.FindGameObjectsWithTag(TowerTag); // Collider�� �ٲٸ� ���� �����ٵ��̤�

        foreach (GameObject tower in Towers)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, tower.transform.position);

            if (distanceToEnemy <= RealRange)
            {
                TowerList.Add(tower);
            }
        }

    }

    protected override void Update()
    {
        if (TowerList.Count == 0)
        {
            return;
        }
        /*

        for (int i = 0; i < TowerList.Count; i++)
        {

            Tower t = TowerList[i].GetComponent<Tower>();

            if (!(t.CheckSpeedBuffTowers.ContainsKey(this))) // ���� ����
            {

                t.SpeedBuffTowers.Add(this, AbilityStat);
                t.CheckSpeedBuffTowers.Add(this, true);

            }
        }
        */
    }

    public override void Shoot()
    {
        
    }

    public override void UpgradeTower()
    {
        base.UpgradeTower();
        /*
        for (int i = 0; i < TowerList.Count; i++)
        {

            Tower t = TowerList[i].GetComponent<Tower>();

            if ((t.CheckSpeedBuffTowers.ContainsKey(this))) // ���� ����
            {

                t.SpeedBuffTowers[this] = AbilityStat;

            }
        }
        */
    }


    public override void SellTower()
    {
        base.SellTower();

        // ����, ���� ���� Ÿ���� ��  ���� ���� ȿ�� �־��ֱ�

        /*
        for (int i = 0; i < TowerList.Count; i++)
        {

            Tower t = TowerList[i].GetComponent<Tower>();

            if ((t.CheckSpeedBuffTowers.ContainsKey(this))) // ���� ����
            {

                t.SpeedBuffTowers[this] = AbilityStat;
                t.CheckSpeedBuffTowers[this] = false;

            }
        }
        */
    }

    public override void MoveTower(Tile tile)
    {
        base.MoveTower(tile);

        // ���� üũ�ؼ� �ٲ���� �ϳ�?

    }



}


public class SpeedBuffTowerComparer : IEqualityComparer<SpeedBuffTower>
{
    public bool Equals(SpeedBuffTower x, SpeedBuffTower y)
    {
        return x.GetHashCode() == y.GetHashCode();
    }

    public int GetHashCode(SpeedBuffTower pos)
    {
        return pos.GetHashCode() ^ pos.GetHashCode() << 2;
    }

}
