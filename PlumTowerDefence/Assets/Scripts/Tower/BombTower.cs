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



    // �� ���� �ϰ� ��� �ؾ��ϳ�

    private void OnTriggerEnter(Collider other)
    {
        // �ϳ� trigger �ɸ��� trigger �ݱ�?
        if(other.gameObject.CompareTag(enemyTag))
        {
            StartCoroutine(nameof(IE_Delay));
        }
    }


    // 3�� �ڿ�

    IEnumerator IE_Delay()
    {
        WaitForSeconds delay = new WaitForSeconds(3f);
        
        yield return delay;

        Explosion();

    }

    // �ݰ� ���� ���� �����ֱ�

    void Explosion()
    {
        GameObject[] Enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        for (int i = 0; i < Enemies.Length; i++)
        {
            float distanceToEnemy = Vector3.Distance(Target.transform.position, Enemies[i].transform.position); // ������ �Ÿ� ���ϱ�


            if (distanceToEnemy <= RealRange) // ��Ÿ� �ȿ� �ִ� Ÿ�ٵ�
            {
                Enemies[i].GetComponent<Enemy>().TakeDamage(AbilityStat, AttackSpecialization, TowerName);
            }
        }
    }

}
