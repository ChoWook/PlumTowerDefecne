using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FlameTower : Tower
{
    public int Angle = 60;

    public GameObject PS_Fire;

    public GameObject forVector;

    private void Awake()
    {
        Setstat(ETowerName.Flame);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        PS_Fire.SetActive(false);
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
                multi += AttackMultiModifier[i];
            }

            for (int i = 0; i < AttackBuffTowers.Count; i++)
            {
                sum += AttackBuffTowers.ElementAt(i).Value;
            }

            return (BaseAttackStat + sum) * multi;
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

            for(int i = 0; i < SpeedPlusModifier.Count; i++)
            {
                sum += SpeedPlusModifier[i];
            }    

            float multi = 1f;

            for (int i = 0; i < SpeedMultiModifier.Count; i++)
            {
                multi += SpeedMultiModifier[i];
            }

            for (int i = 0; i < SpeedBuffTowers.Count; i++)
            {
                sum += SpeedBuffTowers.ElementAt(i).Value;
            }

            return (BaseSpeedStat + sum) * multi;

        }
    }

    // attackrange, slowrate, angle

    public override float CurrentRange
    {
        get
        {
            List<float> list = TowerUpgradeAmount.instance._FlameTowerStat.RangePlusModifier;

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
            float multi = 1f;

            List<float> list = TowerUpgradeAmount.instance._FlameTowerStat.SlowMultiModifier;

            for (int i = 0; i < list.Count; i++)
            {
                multi -= list[i];
            }

            return multi;
        }
    }

    public float RealAngle
    {
        get
        {
            List<float> list = TowerUpgradeAmount.instance._FlameTowerStat.AngleplusModifier;

            float sum = 0f;

            for (int i = 0; i < list.Count; i++)
            {
                sum += list[i];
            }

            return Angle + sum;
        }
    }

    protected override void Update()
    {
        if(GameManager.instance.IsPlayingGame)
        {
            PS_Fire.SetActive(true);
        }
        else
        {
            PS_Fire.SetActive(false);
        }
        

        base.Update();
    }

    protected override IEnumerator IE_GetTargets()
    {
        WaitForSeconds ws = new(0.1f);
        while (true)
        {
            //SortAttackPriority();
            UpdateTarget();
       
            yield return ws;
        }
    }


    public override void Shoot()
    {
        // 타깃과의 각도 받아서 그 공간 안에 있는 적들 공격하기.
        Debug.Log("Shoot");
        Vector3 basic = forVector.transform.position - transform.position;
        basic = basic.normalized;

        for (int i = 0; i < EnemyList.Count; i++)
        { 
            //EnemyList(사거리 안)에 있는 몬스터 하나씩 받아오기
            Transform target = EnemyList[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;

            
            float Dot = Vector3.Dot(basic, dirToTarget);

            float EachAngle = Mathf.Acos(Dot) * Mathf.Rad2Deg;
                
            if (EachAngle < RealAngle / 2 )
            {
                target.GetComponent<Enemy>().TakeDamage(AttackStat, AttackSpecialization, TowerName);
            }

        }

        
    }


    /*
    public void FireEffect()
    {
        GameObject f = PS_Fire;
        ParticleSystem parts = f.GetComponent<ParticleSystem>();
        f.SetActive(true);
        
        float totalDuration = parts.main.duration;

        //StartCoroutine(IE_psDelay(totalDuration, f));
    }

    IEnumerator IE_psDelay(float time, GameObject ps)
    {
        WaitForSeconds ws = new(time);

        yield return ws;

       ps.SetActive(false);
    }
    */


}