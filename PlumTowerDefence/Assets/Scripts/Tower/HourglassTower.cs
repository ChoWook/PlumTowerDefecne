using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HourglassTower : Tower
{
    private void Awake()
    {
        Setstat(ETowerName.Hourglass);
    }

    
    public override float AbilityStat
    {
        get
        {

            float sum = 0f;

            for (int i = 0; i < AbilityPlusModifier.Count; i++)
            {
                sum += AbilityPlusModifier[i];
            }

            List<float> list = TowerUpgradeAmount.instance._HourglassTowerStat.AbilityMultiModifier;

            float multi = 1f;

            for (int i = 0; i < list.Count; i++)
            {
                multi *= list[i];
            }


            return (BaseAbilityStat + sum) * multi;
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
            return;
        }
            

        Shoot();


    }




    public override void Shoot()
    {
        for (int i = 0; i < EnemyList.Count; i++)
        {
            EnemyList[i].GetComponent<Enemy>().SlowEnemy(AbilityStat);
        }
    }

}
