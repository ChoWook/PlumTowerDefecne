using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GatilingTower : Tower
{
    public GameObject PS_FireBullet;
    public GameObject PS_Particlepoint;


    private void Awake()
    {
        Setstat(ETowerName.Gatling);

        controller = AnimatorObject.GetComponent<Animator>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        controller.speed = SpeedStat;
    }


    public override float AttackStat
    {
        get
        {
            List <float> plus = TowerUpgradeAmount.instance._GatlingTowerStat.AttackPlusModifier;

            float sum = 0f;

            for (int i = 0; i < plus.Count; i++)
            {
                sum += plus[i];
            }

            for (int i =0; i<AttackPlusModifier.Count; i++)
            {
                sum += AttackPlusModifier[i];
            }

            for (int i = 0; i < AttackBuffTowers.Count; i++)
            {
                sum += AttackBuffTowers.ElementAt(i).Value;
            }

            return (BaseAttackStat + sum);

        }

    }

    public override float SpeedStat
    {
        get
        {
            List<float> plus = TowerUpgradeAmount.instance._GatlingTowerStat?.SpeedPlusModifier;

            float sum = 0f;

            for (int i = 0; i < plus.Count; i++)
            {
                sum += plus[i];
            }

            for (int i = 0; i < SpeedBuffTowers.Count; i++)
            {
                sum += SpeedBuffTowers.ElementAt(i).Value;
            }

            return (BaseSpeedStat + sum);

        }
    }

    // 회전 애니메이션

    public override void Shoot()
    {
        AnimatorExists(controller, true);
        base.Shoot();

        GameObject ex = ObjectPools.Instance.GetPooledObject(PS_FireBullet.name);
        ex.transform.position = PS_Particlepoint.transform.position;
        ParticleSystem parts = ex.GetComponent<ParticleSystem>();
        //Vector3 dir = Target.transform.position - transform.position;
        float totalDuration = parts.main.duration;

        StartCoroutine(IE_psDelay(totalDuration, ex));

    }

    IEnumerator IE_psDelay(float time, GameObject ps)
    {
        WaitForSeconds ws = new(time);

        yield return ws;

        ObjectPools.Instance.ReleaseObjectToPool(ps);
    }


    protected override void Update()
    {
        AnimatorExists(controller, false);
        base.Update();
    }

    


    // 파티클 효과





}
