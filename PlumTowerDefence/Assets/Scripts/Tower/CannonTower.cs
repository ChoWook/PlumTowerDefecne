using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CannonTower : Tower
{
    public GameObject PS_FireBullet;
    public GameObject PS_Particlepoint;


    private void Awake()
    {
        Setstat(ETowerName.Cannon);

        controller = AnimatorObject.GetComponent<Animator>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        controller.speed = 1 / SpeedStat;
        PS_FireBullet.SetActive(false);
    }

    public override float AttackStat
    {
        get
        {
            float sum = 0f;

            List<float> list = TowerUpgradeAmount.instance._CannonTowerStat.AttackPlusModifier;

            for (int i =0; i< list.Count; i++)
            {
                sum += list[i];
            }

            for (int i = 0; i < AttackPlusModifier.Count; i++)
            {
                sum += AttackPlusModifier[i];
            }

            float multi = 1f;

            for (int i = 0; i < AttackMultiModifier.Count; i++)
            {
                multi += AttackMultiModifier[i];
            }

            for (int i=0; i< AttackBuffTowers.Count; i++)
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
            float sum = 0f;

            List<float> list = TowerUpgradeAmount.instance._CannonTowerStat.SpeedPlusModifier;

            for (int i =0; i< list.Count; i++)
            {
                sum += list[i];
            }

            for (int i = 0; i < SpeedPlusModifier.Count; i++)
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

    // 발사 애니메이터

    public override void Shoot()
    {
        AnimatorExists(controller, true);

        GameObject ex = PS_FireBullet;

        ex.SetActive(true);
        ParticleSystem parts = ex.GetComponent<ParticleSystem>();
        float totalDuration = parts.main.duration /2 ;

        StartCoroutine(IE_psDelay(totalDuration, ex));
        Debug.Log("Shoot");
        base.Shoot();
       
    }

    IEnumerator IE_psDelay(float time, GameObject ps)
    {
        WaitForSeconds ws = new(time);

        yield return ws;

        ps.SetActive(false);
    }


    protected override void Update()
    {
        AnimatorExists(controller, false);
        base.Update();
    }


}
