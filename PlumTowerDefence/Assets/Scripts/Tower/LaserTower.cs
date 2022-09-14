using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTower : Tower
{
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
        if (BulletPrefab != null)
        {
            GameObject bulletGO = ObjectPools.Instance.GetPooledObject(BulletPrefab.name);
            bulletGO.transform.position = Target.transform.position;

            bulletGO.GetComponent<Bullet>()?.Seek(Target, ProjectileSpeed, AttackStat, AttackSpecialization);
        }
    }


    
    IEnumerator IE_GetCoolTime() //�̷��� �ϸ� ������ �³�?
    {
        WaitForSeconds cooltime = new WaitForSeconds(SpeedStat);

        yield return cooltime;
    }



}
