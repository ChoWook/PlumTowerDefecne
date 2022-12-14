using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class SpeedBuffTower : Tower
{
    public const string TowerTag = "Tower";

    public List<GameObject> TowerList = new List<GameObject>();

    public GameObject particle;

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

            for(int i = 0; i < AbilityPlusModifier.Count; i++)
            {
                sum += AbilityPlusModifier[i];
            }

            float multi = 1f;

            for (int i = 0; i < AbilityMultiModifier.Count; i++)
            {
                multi += AbilityMultiModifier[i];
            }

            return (BaseAbilityStat + sum) * multi;
        }
    }

    // attackRange, additionalAttackDamage;

    public override float CurrentRange
    {
        get
        {
            List<float> list = TowerUpgradeAmount.instance._SpeedBuffTowerStat.RangePlusModifier;

            float sum = 0f;

            for(int i = 0; i < list.Count; i++)
            {
                sum += list[i];
            }

            return Range + sum;
        }
        
    }

    public static float UpgradeRange
    {
        get
        {
            List<float> list = TowerUpgradeAmount.instance._SpeedBuffTowerStat.RangePlusModifier;

            float sum = 0f;

            for (int i = 0; i < list.Count; i++)
            {
                sum += list[i];
            }

            return sum ;
        }
    }

    protected override void UpdateTarget()
    {
        TowerList.Clear();

        GameObject[] Towers = GameObject.FindGameObjectsWithTag(TowerTag);

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
        if (GameManager.instance.IsPlayingGame)
        {
            particle.SetActive(true);
        }
        else
        {
            particle.SetActive(false);
        }

        if (TowerList.Count == 0)
        {
            return;
        }
        

        for (int i = 0; i < TowerList.Count; i++)
        {

            Tower t = TowerList[i].GetComponent<Tower>();
            if(t.SpeedStat != 0f)
            {
                if (!(t.CheckSpeedBuffTowers.ContainsKey(this)))
                {

                    t.SpeedBuffTowers.TryAdd(this, AbilityStat);
                    t.CheckSpeedBuffTowers.TryAdd(this, true);

                }
                else
                {
                    t.SpeedBuffTowers[this] = AbilityStat;
                    t.CheckSpeedBuffTowers[this] = true;
                }
            }
            
        }
        
    }

    public override void Shoot()
    {
        
    }

    public override void UpgradeTower()
    {
        base.UpgradeTower();
        
        for (int i = 0; i < TowerList.Count; i++)
        {

            Tower t = TowerList[i].GetComponent<Tower>();

            if ((t.CheckSpeedBuffTowers.ContainsKey(this)))
            {
                t.SpeedBuffTowers[this] = AbilityStat;
            }
        }
        
    }


    public override void SellTower()
    {
        base.SellTower();


        RemoveBuff();
    }

    public override void MoveTower(Tile tile)
    {
        if(gameObject.activeSelf)
        {
            StopCoroutine(nameof(IE_GetTargets));
        }
        

        RemoveBuff();

        base.MoveTower(tile);

        if(gameObject.activeSelf)
        {
            StartCoroutine(nameof(IE_GetTargets));
        }
        

    }


    public void RemoveBuff()
    {
        for (int i = 0; i < TowerList.Count; i++)
        {

            Tower t = TowerList[i].GetComponent<Tower>();

            if ((t.CheckSpeedBuffTowers.ContainsKey(this)))
            {

                t.SpeedBuffTowers[this] = 0f;
                t.CheckSpeedBuffTowers[this] = false;

            }
        }
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
