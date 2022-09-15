using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTower : Tower
{

    public bool IsCoolTime = false ;

    private void Awake()
    {
        Setstat(ETowerName.Laser);
    }

    // Ÿ�� ������
    // �����ϰ� -> �Ҹ����� ó��
    // �Ҹ��� ����������� ������
    // �Ҹ� ������� ������Ÿ���� �ڷ�ƾ? ������ ȣ��
    // �� �� �� ����


    protected override void Update()
    {
        if (Target == null)
        {
            return;

        }
        else if (Target.GetComponent<Enemy>().IsAlive == false)
        {
            return;
        }

        Shoot();

    }


    public override void Shoot()
    {
        IsCoolTime = true;
        if (BulletPrefab != null)
        {
            GameObject bulletGO = ObjectPools.Instance.GetPooledObject(BulletPrefab.name);
            bulletGO.transform.position = Target.transform.position;

            bulletGO.GetComponent<Bullet>()?.Seek(Target, ProjectileSpeed, AttackStat, AttackSpecialization);

        }
    }



    protected override IEnumerator IE_GetTargets()
    {
        WaitForSeconds ws = new WaitForSeconds(0.5f);

        //��Ÿ� �ȿ� ���� ���� EnemyList�� ���� + ��Ÿ����� ������ �����.

        WaitForSeconds cooltime = new WaitForSeconds(SpeedStat);


        while (true)
        {
            if(IsCoolTime) // �� �� ���������� ��Ÿ�� ������
            {
                yield return cooltime;
                IsCoolTime=false;
            }
            //SortAttackPriority();
            UpdateTarget();

            yield return ws;
        }

    }



}
