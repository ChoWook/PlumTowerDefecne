using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTower : Tower
{

    public bool IsCoolTime = false ;

    private void Awake()
    {
        Setstat(ETowerName.Laser); // onenable���� ���� �� �����ϱ�
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
       
        if (BulletPrefab != null && Target != null)
        {
            GameObject bulletGO = ObjectPools.Instance.GetPooledObject(BulletPrefab.name);
            bulletGO.transform.position = Target.transform.position;

            bulletGO.GetComponent<Bullet>()?.Seek(Target, ProjectileSpeed, AttackStat, AttackSpecialization);

            StopCoroutine(IE_GetTargets());
        }
    }

    public IEnumerator IE_CoolTime()
    {
        WaitForSeconds cooltime = new WaitForSeconds(SpeedStat);

        yield return cooltime;
        StartCoroutine(IE_GetTargets());
    }

}
