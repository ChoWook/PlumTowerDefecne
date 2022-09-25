using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTower : Tower
{
    bool isTriggered = false;


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
        AttackBuffAmount = 0f;
        SpeedBuffAmount = 0f;

        UpgradeCount = 0;

        RealRange = Range * GameManager.instance.UnitTileSize;

        RealSize = Size * GameManager.instance.UnitTileSize;
    }

    protected override void Update()
    {

    }


    // 적 감지 하고 어떻게 해야하나

    private void OnTriggerEnter(Collider other)
    {
        if(!(isTriggered))
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
        WaitForSeconds delay = new (3f);
        
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

        ObjectPools.Instance.ReleaseObjectToPool(gameObject);
        isTriggered=false;
    }

}
