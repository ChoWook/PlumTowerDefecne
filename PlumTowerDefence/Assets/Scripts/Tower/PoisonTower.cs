using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonTower : Tower
{

    private void Awake()
    {
        Setstat(ETowerName.Poison);
    }

    protected override void UpdateTarget()
    {
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
            EnemyList[i].GetComponent<Enemy>().PoisonEnemy(AbilityStat);
        }
    }
}
