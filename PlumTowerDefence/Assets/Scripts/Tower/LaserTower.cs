using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTower : Tower
{

    public bool IsCoolTime = false ;

    private void Awake()
    {
        Setstat(ETowerName.Laser); // onenable에서 변수 값 정리하기
    }

    
    public override float AttackStat
    {
        get
        {
            List<float> list = TowerUpgradeAmount.instance._LaserTowerStat.AttackPlusModifier;

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
        } // 순서?
    }

    public override float SpeedStat
    {
        get
        {
            List<float> list = TowerUpgradeAmount.instance._LaserTowerStat.SpeedPlusModifier;


            float sum = 0f;

            for (int i = 0; i < list.Count; i++)
            {
                sum += list[i];
            }

            float multi = 1f;

            for (int i = 0; i < SpeedMultiModifier.Count; i++)
            {
                multi *= SpeedMultiModifier[i];
            }

            return (BaseSpeedStat + sum + SpeedBuffAmount) * multi;
        }
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
        else if (IsCoolTime)
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

            Bullet b = bulletGO.GetComponent<Bullet>();

            b?.SetTower(this);
            b?.Seek(Target, ProjectileSpeed, AttackStat, AttackSpecialization);
      

            StopCoroutine(nameof(IE_GetTargets));
        }
    }

    public IEnumerator IE_CoolTime()
    {
        WaitForSeconds cooltime = new WaitForSeconds(1f/SpeedStat);

        yield return cooltime;
        IsCoolTime=false;

        StartCoroutine(nameof(IE_GetTargets));
    }

}
