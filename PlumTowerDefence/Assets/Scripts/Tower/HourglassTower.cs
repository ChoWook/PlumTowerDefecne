using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;

public class HourglassTower : Tower
{
    public GameObject PS_Fire;

    bool isShooting= false;

    private void Awake()
    {
        Setstat(ETowerName.Hourglass);

        PS_Fire.SetActive(false);
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
                multi += list[i];
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
            PS_Fire.SetActive(false);
            isShooting = false;
            return;
        }
            

        Shoot();


    }




    public override void Shoot()
    {
        if(!isShooting)
        {
            isShooting = true;
            PS_Fire.SetActive(true);
        }

        for (int i = 0; i < EnemyList.Count; i++)
        {
            EnemyList[i].GetComponent<Enemy>().SlowEnemy(AbilityStat);
        }
    }

}
