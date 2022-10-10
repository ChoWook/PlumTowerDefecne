using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonTower : Tower
{
    SoundPlay Source;

    bool SoundIsLoop= false;

    public GameObject PS_Fire;

    private void Awake()
    {
        Setstat(ETowerName.Poison);

        Source = GetComponent<SoundPlay>();

        PS_Fire.SetActive(false);
    }

    
    public override float AbilityStat
    {
        get 
        {
            List<float> plus = TowerUpgradeAmount.instance._PoisonTowerStat.AbilityPlusModifier;

            float sum = 0f;

            for (int i = 0; i < plus.Count; i++)
            {
                sum += plus[i];
            }

            for (int i = 0; i < AbilityPlusModifier.Count; i++)
            {
                sum += AbilityPlusModifier[i];
            }


            return (BaseAbilityStat + sum);
        }
    }


    // range, slowrate TODO + else?
    public override float CurrentRange
    {
        get
        {
            List<float> list = TowerUpgradeAmount.instance._PoisonTowerStat.RangePlusModifier;

            float sum = 0f;

            for (int i = 0; i < list.Count; i++)
            {
                sum += list[i];
            }

            return Range + sum;
        }
    }

    public float SlowAmount
    {
        get
        {
            float plus = 0f;

            List<float> list = TowerUpgradeAmount.instance._PoisonTowerStat.SlowMultiModifier;

            for (int i = 0; i < list.Count; i++)
            {
                plus += list[i];
            }

            return plus;
        }
    }
    public static float UpgradeRange
    {
        get
        {
            List<float> list = TowerUpgradeAmount.instance._PoisonTowerStat.RangePlusModifier;

            float sum = 0f;

            for (int i = 0; i < list.Count; i++)
            {
                sum += list[i];
            }

            return sum;
        }
    }




    protected override void UpdateTarget()
    {
        EnemyList.Clear();

        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if (distanceToEnemy <= RealRange)
            {
                EnemyList.Add(enemy);
            }
        }

    }

    protected override void Update()
    {
        if (EnemyList.Count == 0)
        {
            SoundIsLoop = false;
            Source.SetLoop(false);
            PS_Fire.SetActive(false);
            return;
        }

        

        Shoot();
    }

    public override void Shoot()
    {
        if (!SoundIsLoop)
        {
            Source.SetLoop(true);
            Source.Play();
            SoundIsLoop = true;
            PS_Fire.SetActive(true);
        }

        for (int i = 0; i < EnemyList.Count; i++)
        {
            Enemy enemy = EnemyList[i].GetComponent<Enemy>();

            enemy.PoisonEnemy(AbilityStat);
            enemy.TakeTowerDebuff(ETowerDebuffType.Slow, SlowAmount);

        }
    }
}
