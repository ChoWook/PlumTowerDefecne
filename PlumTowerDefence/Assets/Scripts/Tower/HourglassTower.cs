using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HourglassTower : Tower
{
    private void Awake()
    {
        Setstat(ETowerName.Hourglass);
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
            return;

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
