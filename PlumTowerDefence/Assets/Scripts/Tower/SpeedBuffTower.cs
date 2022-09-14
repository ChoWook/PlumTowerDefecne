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

    protected override void UpdateTarget()
    {
        GameObject[] Towers = GameObject.FindGameObjectsWithTag(TowerTag); // Collider로 바꾸면 정말 좋을텐데ㅜㅜ

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
           

        for (int i = 0; i < TowerList.Count; i++)
        {
            Tower t = TowerList[i].GetComponent<Tower>();
            if (!(t.CheckSpeedBuff))
            {

                t.GetSpeedBuff(AbilityStat);
                t.CheckSpeedBuff = true;

            }
        }
    }




    public override void Shoot()
    {
        
    }
}
