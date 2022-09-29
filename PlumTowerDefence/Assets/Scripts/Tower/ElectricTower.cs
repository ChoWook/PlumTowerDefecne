using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DigitalRuby.LightningBolt;

public class ElectricTower : Tower
{
    public GameObject LightningStart;

    public List<float> SlowMultiModifier = new List<float>();

    public float ElectricRange;

    public float ElecRangeStat = 2f;


    private void Awake()
    {
        Setstat(ETowerName.Electric);
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


            return (BaseAbilityStat + sum);
        }
    }

    // SlowMulti TODO discuss with Enemy part : how to slow the target
    public float SlowAmount
    {
        get
        {
            float multi = 1f;

            for(int i = 0; i < SlowMultiModifier.Count; i++)
            {
                multi -= SlowMultiModifier[i];
            }

            return multi;
        }
    }




    public override void Shoot() // 수정
    {
        GameObject L = ObjectPools.Instance.GetPooledObject(BulletPrefab.name);

        LightningBoltScript l = L.GetComponent<LightningBoltScript>();

        l.StartObject = LightningStart;
        l.EndObject = Target;

        Target.GetComponent<Enemy>().TakeDamage(AttackStat, AttackSpecialization, TowerName);

        StartCoroutine(nameof(IE_ShowLightning), L);

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
                EnemiesInRange[i].GetComponent<Enemy>().TakeDamage(AttackStat, AttackSpecialization, TowerName);

                // TODO : set bolt between target and enemiesInRange
                LightningChain(EnemiesInRange[i]);

            }
        }
        else
        {
            for (int i = 0; i < EnemiesInRange.Count; i++)
            {
                EnemiesInRange[i].GetComponent<Enemy>().TakeDamage(AttackStat, AttackSpecialization, TowerName);
                
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


        Target.GetComponent<Enemy>().TakeDamage(AttackStat, AttackSpecialization, TowerName);


        StartCoroutine(nameof(IE_ShowLightning), L);
    }


    
    IEnumerator IE_ShowLightning(GameObject Effect) // release effect
    {
        WaitForSeconds time = new(0.5f);

        yield return time;

        ObjectPools.Instance.ReleaseObjectToPool(Effect);

    }



}
