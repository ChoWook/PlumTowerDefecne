using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DigitalRuby.LightningBolt;

public class LaserTower : Tower
{

    public bool IsCoolTime = false ;

    public GameObject Laser;

    public GameObject LaserStart;

    private void Awake()
    {
        Setstat(ETowerName.Laser); // onenable에서 변수 값 정리하기
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        Laser.SetActive(false);
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
                multi += AttackMultiModifier[i];
            }

            for (int i = 0; i < AttackBuffTowers.Count; i++)
            {
                sum += AttackBuffTowers.ElementAt(i).Value;
            }

            return (BaseAttackStat + sum) * multi;
        }
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
                multi += SpeedMultiModifier[i];
            }

            for (int i = 0; i < SpeedBuffTowers.Count; i++)
            {
                sum += SpeedBuffTowers.ElementAt(i).Value;
            }

            return (BaseSpeedStat + sum) * multi;
        }
    }




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

            LightningBoltScript l = Laser.GetComponent<LightningBoltScript>();

            l.StartObject = LaserStart;
            l.EndObject = bulletGO;

            Laser.SetActive(true);
            
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
