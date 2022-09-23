using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTower : Tower
{
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

            float multi = 1f;

            for (int i = 0; i < AttackMultiModifier.Count; i++)
            {
                multi *= AttackMultiModifier[i];
            }

            return (BaseAttackStat + sum + AttackBuffAmount) * multi;
        }
    }

    // Range
    



    protected override void OnEnable()
    {
        base.OnEnable();

        StopCoroutine(nameof(IE_GetTargets));
    }



    // 적 감지 하고 어떻게 해야하나

    private void OnTriggerEnter(Collider other)
    {
        // 하나 trigger 걸리면 trigger 닫기?
        if(other.gameObject.CompareTag(enemyTag))
        {
            StartCoroutine(nameof(IE_Delay));
        }
    }


    // 3초 뒤에

    IEnumerator IE_Delay()
    {
        WaitForSeconds delay = new WaitForSeconds(3f);
        
        yield return delay;

        Explosion();

    }

    // 반경 안의 몬스터 피해주기

    void Explosion()
    {
        GameObject[] Enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        for (int i = 0; i < Enemies.Length; i++)
        {
            float distanceToEnemy = Vector3.Distance(Target.transform.position, Enemies[i].transform.position); // 적과의 거리 구하기


            if (distanceToEnemy <= RealRange) // 사거리 안에 있는 타겟들
            {
                Enemies[i].GetComponent<Enemy>().TakeDamage(AbilityStat, AttackSpecialization, TowerName);
            }
        }
    }

}
