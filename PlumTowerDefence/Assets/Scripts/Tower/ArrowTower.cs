using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ArrowTower : Tower
{

    SoundPlay Source;

    public GameObject PT_Fire;

    ParticleSystem particle;

    private void Awake()
    {
        Setstat(ETowerName.Arrow);

        controller = AnimatorObject.GetComponent<Animator>();

        Source = GetComponent<SoundPlay>();

        particle = PT_Fire.GetComponent<ParticleSystem>();
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
            float sum = 0f;

            List<float> plus = TowerUpgradeAmount.instance._ArrowTowerStat.AttackPlusModifier;


            for (int i = 0; i < plus.Count; i++)
            {
                sum += plus[i];
            }
            
            for(int i=0; i<AttackPlusModifier.Count; i++)
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
            List<float> plus = TowerUpgradeAmount.instance._ArrowTowerStat?.SpeedPlusModifier;

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

    // TODO connect poisondamage

    

    // 시위 당기기
    public override void Shoot()
    {
        AnimatorExists(controller, true);
        Source.Play();
        particle.Play();

        base.Shoot();
        
        
    }

    protected override void Update()
    {
        AnimatorExists(controller, false);
        base.Update();
    }




}