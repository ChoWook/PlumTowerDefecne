using DigitalRuby.LightningBolt;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ElectricTower : Tower
{
    public GameObject LightningStart;

    public GameObject PT_Hovering;

    SoundPlay Source;


    public float ElectricRange;

    public float ElecRangeStat = 2f;


    private void Awake()
    {
        Setstat(ETowerName.Electric);

        PT_Hovering.SetActive(false); 

        Source = GetComponent<SoundPlay>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        //LightningEffect.SetActive(false);
        ElectricRange = ElecRangeStat * GameManager.instance.UnitTileSize;
    }


    public override float AttackStat
    {
        get
        {
            List<float> list = TowerUpgradeAmount.instance._ElectricTowerStat.AttackPlusModifier;

            float sum = 0f;

            for (int i = 0; i < list.Count; i++)
            {
                sum += list[i];
            }

            for (int i = 0; i < AttackBuffTowers.Count; i++)
            {
                sum += AttackBuffTowers.ElementAt(i).Value;
            }


            return (BaseAttackStat + sum);
        }
    }
    public override float AbilityStat
    {
        get
        {
            List<float> list = TowerUpgradeAmount.instance._ElectricTowerStat.AbilityPlusModifier;

            float sum = 0f;

            for (int i = 0; i < list.Count; i++)
            {
                sum += list[i];
            }

            for (int i = 0; i < AbilityPlusModifier.Count; i++)
            {
                sum += AbilityPlusModifier[i];
            }

            return (BaseAbilityStat + sum);
        }
    }

    public float SlowAmount
    {
        get
        {
            float plus = 0f;

            List<float> list = TowerUpgradeAmount.instance._ElectricTowerStat.SlowMultiModifier;

            for (int i = 0; i < list.Count; i++)
            {
                plus += list[i];
            }

            return plus;
        }
    }

    public override void Shoot()
    {
        Source.Play();
        PT_Hovering.SetActive(false);
        PT_Hovering.SetActive(true);

        GameObject L = ObjectPools.Instance.GetPooledObject(BulletPrefab.name);

        LightningBoltScript l = L.GetComponent<LightningBoltScript>();

        l.StartObject = LightningStart;
        l.EndObject = Target;

        AttackEnemy(Target);

        if(gameObject.activeSelf)
        {
            StartCoroutine(nameof(IE_ShowLightning), L);
        }
            

        ShockWave();

    }


    public void ShockWave() // calculate distance
    {

        List<GameObject> EnemiesInRange = new List<GameObject>(); // 범위 안의 몬스터
        List<float> DistanceInrange = new List<float>(); // 값 비교용 list

        GameObject[] Enemies = GameObject.FindGameObjectsWithTag(enemyTag);


        for (int i = 0; i < Enemies.Length; i++)
        {
            float distanceToEnemy = Vector3.Distance(Target.transform.position, Enemies[i].transform.position); // 적과의 거리 구하기

            if (distanceToEnemy <= ElectricRange && Enemies[i] != Target)
            {
                if (EnemiesInRange.Count == 0)
                {
                    EnemiesInRange.Add(Enemies[i]);
                    DistanceInrange.Add(distanceToEnemy);
                    continue;
                }

                // 길이 비교 함수

                int idx = 0;

                for (int j = 0; j < EnemiesInRange.Count; j++)
                {
                    if (distanceToEnemy > DistanceInrange[j])
                    {
                        idx = j;
                        break;
                    }
                }

                EnemiesInRange.Insert(idx, Enemies[i]);
                DistanceInrange.Insert(idx, distanceToEnemy);
            }

        }

        // AbilityStat만큼 공격하기
        if (EnemiesInRange.Count >= AbilityStat)
        {
            for (int i = 0; i < AbilityStat; i++)
            {
                AttackEnemy(EnemiesInRange[i]);

                LightningChain(EnemiesInRange[i]);

            }
        }
        else
        {
            for (int i = 0; i < EnemiesInRange.Count; i++)
            {
                AttackEnemy(EnemiesInRange[i]);

                LightningChain(EnemiesInRange[i]);
            }
        }

    }

    public void LightningChain(GameObject enemy) // attack other enemies.
    {
        GameObject L = ObjectPools.Instance.GetPooledObject(BulletPrefab.name);

        LightningBoltScript l = L.GetComponent<LightningBoltScript>();

        l.StartObject = Target;
        l.EndObject = enemy;

        AttackEnemy(Target);

        if(gameObject.activeSelf)
        {
            StartCoroutine(nameof(IE_ShowLightning), L);
        }

        
    }


    public void AttackEnemy(GameObject other)
    {
        Enemy enemy = other.GetComponent<Enemy>();

        enemy.TakeDamage(AttackStat, AttackSpecialization, TowerName);
        enemy.TakeTowerDebuff(ETowerDebuffType.Slow, SlowAmount);
    }


    IEnumerator IE_ShowLightning(GameObject Effect) // release effect
    {
        WaitForSeconds time = new(0.5f);

        yield return time;

        ObjectPools.Instance.ReleaseObjectToPool(Effect);

    }



}
