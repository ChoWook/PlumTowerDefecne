using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameTower : Tower
{
    public int Angle = 60;

    private void Awake()
    {
        Setstat(ETowerName.Flame);
    }

    public override void Shoot()
    {
        // 타깃과의 각도 받아서 그 공간 안에 있는 적들 공격하기.

        Vector3 basic = Target.transform.position - transform.position;

        for (int i = 0; i < EnemyList.Count; i++)
        {
            //EnemyList(사거리 안)에 있는 몬스터 하나씩 받아오기
            Transform target = EnemyList[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;

            float Dot = Vector3.Dot(basic, dirToTarget);

            float EachAngle = Mathf.Acos(Dot) * Mathf.Rad2Deg;

            if (EachAngle < Angle / 2)
            {
                target.GetComponent<Enemy>().TakeDamage(AttackStat, AttackSpecialization , TowerName);
            }

        }

    }







}
