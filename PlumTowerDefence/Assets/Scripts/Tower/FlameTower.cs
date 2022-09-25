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

    
    public override float AttackStat
    {
        get
        {
            List<float> list = TowerUpgradeAmount.instance._FlameTowerStat.AttackPlusModifier;

            float sum = 0f;

            for (int i = 0; i < list.Count; i++)
            {
                sum += list[i];
            }

            float multi = 1f;

            for (int i = 0; i < AttackMultiModifier.Count; i++)
            {
                multi *= AttackMultiModifier[i];
            }

            return (BaseAttackStat + sum + AttackBuffAmount) * multi;
        }
         
    }

    public override float SpeedStat
    {
        get
        {
            List<float> list = TowerUpgradeAmount.instance._FlameTowerStat.SpeedPlusModifier;


            float sum = 0f;

            for (int i = 0; i < list.Count; i++)
            {
                sum += list[i];
            }

            float multi = 1f;

            for (int i = 0; i < SpeedMultiModifier.Count; i++)
            {
                multi *= SpeedMultiModifier[i];
            }

            return (BaseSpeedStat + sum + SpeedBuffAmount) * multi;
        }
    }

    


    // attackrange, slowrate, angle



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
