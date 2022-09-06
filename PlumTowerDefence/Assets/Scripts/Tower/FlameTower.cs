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
        // Ÿ����� ���� �޾Ƽ� �� ���� �ȿ� �ִ� ���� �����ϱ�.

        Vector3 basic = Target.transform.position - transform.position;

        for (int i = 0; i < EnemyList.Count; i++)
        {
            //EnemyList(��Ÿ� ��)�� �ִ� ���� �ϳ��� �޾ƿ���
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
