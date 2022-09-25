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


    // �� ���� �ϰ� ��� �ؾ��ϳ�

    private void OnTriggerEnter(Collider other)
    {
        if(!(isTriggered))
        {
            if (other.transform.parent.CompareTag(enemyTag)) // �� �Ųٷ� ����?
            {
                StartCoroutine(nameof(IE_Delay));
                
                isTriggered = true;
            }
        }
        // �ϳ� trigger �ɸ��� trigger �ݱ�?
            
    }


    // 3�� �ڿ�

    IEnumerator IE_Delay()
    {
        WaitForSeconds delay = new (3f);
        
        yield return delay;
        
        Explosion();

    }

    // �ݰ� ���� ���� �����ֱ�

    void Explosion()
    { 
        
        GameObject[] Enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        for (int i = 0; i < Enemies.Length; i++)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, Enemies[i].transform.position); // ������ �Ÿ� ���ϱ�

            if (distanceToEnemy <= RealRange) // ��Ÿ� �ȿ� �ִ� Ÿ�ٵ�
            {
                Enemies[i].GetComponent<Enemy>().TakeDamage(AbilityStat, AttackSpecialization, TowerName);
            }
        }

        ObjectPools.Instance.ReleaseObjectToPool(gameObject);
        isTriggered=false;
    }

}
