using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BombTower : Tower
{
    bool isTriggered = false;

    public GameObject PS_Explosion;


    private void Awake()
    {
        Setstat(ETowerName.Bomb);
    }


    public override float AttackStat
    {
        get
        {
            List<float> list = TowerUpgradeAmount.instance._BombStat.AttackPlusModifier;

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

    // Range

    public override float CurrentRange
    {
        get
        {
            List<float> list = TowerUpgradeAmount.instance._BombStat.RangePlusModifier;

            float sum = 0f;

            for (int i = 0; i < list.Count; i++)
            {
                sum += list[i];
            }

            return Range + sum;
        }
    }



    protected override void OnEnable()
    {
        AttackBuffAmount = 0f;
        SpeedBuffAmount = 0f;

        UpgradeCount = 0;

        RealSize = Size * GameManager.instance.UnitTileSize;
    }

    // 적 감지 하고 어떻게 해야하나

    private void OnTriggerEnter(Collider other)
    {
        if (!(isTriggered))
        {
            if (other.transform.parent.CompareTag(enemyTag)) // 왜 거꾸로 되지?
            {
                StartCoroutine(nameof(IE_Delay));

                isTriggered = true;
            }
        }
        // 하나 trigger 걸리면 trigger 닫기?

    }


    // 3초 뒤에

    IEnumerator IE_Delay()
    {
        WaitForSeconds delay = new(3f);

        yield return delay;

        Explosion();

    }

    // 반경 안의 몬스터 피해주기

    void Explosion()
    {

        GameObject[] Enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        for (int i = 0; i < Enemies.Length; i++)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, Enemies[i].transform.position); // 적과의 거리 구하기

            if (distanceToEnemy <= RealRange) // 사거리 안에 있는 타겟들
            {
                Enemies[i].GetComponent<Enemy>().TakeDamage(AbilityStat, AttackSpecialization, TowerName);
            }
        }

        GameObject ex = ObjectPools.Instance.GetPooledObject(PS_Explosion.name);
        ex.transform.position = transform.position;
        ex.transform.localScale = new Vector3(RealRange, RealRange, RealRange) / 4;
        ParticleSystem parts = ex.GetComponent<ParticleSystem>();
        float totalDuration = parts.main.duration;

        StartCoroutine(IE_psDelay(totalDuration, ex));

        ObjectPools.Instance.ReleaseObjectToPool(gameObject);
        isTriggered = false;
    }

    IEnumerator IE_psDelay(float time, GameObject ps)
    {
        WaitForSeconds ws = new(time);

        yield return ws;

        ObjectPools.Instance.ReleaseObjectToPool(ps);
    }

}