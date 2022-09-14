using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTower : Tower
{
    private void Awake()
    {
        Setstat(ETowerName.Laser);
    }

    // 타겟 잡으면
    // 공격하고 -> 불릿에서 처리
    // 불릿이 진행방향으로 움직임
    // 불릿 사라지면 레이저타워의 코루틴? 딜레이 호출
    // 그 후 또 공격


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


    
    IEnumerator IE_GetCoolTime() //이렇게 하면 딜레이 맞나?
    {
        WaitForSeconds cooltime = new WaitForSeconds(SpeedStat);

        yield return cooltime;
    }



}
