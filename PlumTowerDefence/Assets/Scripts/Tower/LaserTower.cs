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

        //사거리 안에 들어온 적들 EnemyList에 정리 + 사거리에서 나가면 지우기.

        WaitForSeconds cooltime = new WaitForSeconds(SpeedStat);


        while (true)
        {
            if(IsCoolTime) // 한 번 공격했으면 쿨타임 돌리기
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
