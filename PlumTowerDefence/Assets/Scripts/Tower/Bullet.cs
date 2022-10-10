using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bullet : MonoBehaviour
{
    private GameObject target;            // 타겟

    public Tower tower;

    public LaserTower Lt;

    public ElectricTower Et;

    //public GameObject ObjectPool;

    private float Damage;               // 타겟에게 가할 데미지

    //private int AttackPropertyID;        // 속성 아이디

    private EAttackSpecialization AttackSpecialization; // 공격 종류

    public float Speed;

    public float MissileRange;

    public float LaserLength;

    public Vector3 LaserDestination = new Vector3();

    public GameObject Explosion;

    public float PoisonAttackStat
    {
        get
        {
            float sum = 0f;

            List<float> list = TowerUpgradeAmount.instance._ArrowTowerStat.PoisonAttackMultiModifier;

            for (int i = 0; i < list.Count; i++)
            {
                sum += list[i];
            }

            return sum;
        }
    }

    public float PoisonDurationStat
    {
        get
        {
            float sum = 0f;

            List<float> list = TowerUpgradeAmount.instance._ArrowTowerStat.PoisonDurationPlusModifier;

            for (int i = 0; i < list.Count; i++)
            {
                sum += list[i];
            }

            return sum;
        }
    }

    //TODO connect burndamage
    public float BurnAttackStat
    {
        get
        {
            float sum = 0f;

            List<float> list = TowerUpgradeAmount.instance._ArrowTowerStat.BurnAttackMultiModifier;

            for (int i = 0; i < list.Count; i++)
            {
                sum += list[i];
            }

            return sum;
        }
    }

    public float BurnDurationStat
    {
        get
        {
            float sum = 0f;

            List<float> list = TowerUpgradeAmount.instance._ArrowTowerStat.BurnDurationPlusModifier;

            for (int i = 0; i < list.Count; i++)
            {
                sum += list[i];
            }

            return sum;
        }
    }




    public void SetTower(Tower tower)
    {
        this.tower = tower;

        MissileRange = tower.AbilityStat * GameManager.instance.UnitTileSize;

        LaserLength = tower.AbilityStat * GameManager.instance.UnitTileSize;            // 레이저 길이


        if (tower.TowerName == ETowerName.Laser)
        {

            // 불릿 크기 지정
            Transform parent = transform.parent;
            transform.parent = null;
            transform.localScale = new Vector3(GameManager.instance.UnitTileSize, 1, GameManager.instance.UnitTileSize); // 높이 추후 수정
            transform.parent = parent;

            Lt = tower.GetComponent<LaserTower>();

        }
    }

    public void Seek(GameObject _target, float _Speed, float _Damage, EAttackSpecialization _AttackSpecialization)
    {
        target = _target;
        Speed = _Speed;
        Damage = _Damage;
        AttackSpecialization = _AttackSpecialization;

        LaserTarget();

    }

    public void AttackEnemy(GameObject other)
    {
        Enemy enemy = other.GetComponentInParent<Enemy>();
        
        if(enemy == null)
        {
            return;
        }

        enemy.TakeDamage(Damage, AttackSpecialization, tower.TowerName);

        if (tower.TowerName == ETowerName.Arrow)
        {
            if(PoisonAttackStat != 0f)
            {
                enemy.TakeTowerDebuff(ETowerDebuffType.Poison, PoisonAttackStat, PoisonDurationStat);
            }

            if(BurnAttackStat != 0f)
            {
                enemy.TakeTowerDebuff(ETowerDebuffType.Burn, BurnAttackStat, BurnDurationStat);
            }
        }

    }


    private void OnTriggerEnter(Collider other)
    {
        if (tower.TowerName == ETowerName.Laser)
        {
            AttackEnemy(other.gameObject);
        }

    }



    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            DestroyBullet();
            return;
        }

        float distanceThisFrame = Speed * Time.deltaTime;

        if (tower.TowerName == ETowerName.Laser)
        {
            // 벡터 값 받고
            // 타겟 위치에서 생성(타워에서 관리)
            // 벡터 y축 고정하고 해당 방향으로 ability만큼 움직임(폭은 1타일크기만큼 변경해주기)

            transform.position = Vector3.MoveTowards(transform.position, LaserDestination, distanceThisFrame); // 이게 아니다!!!!



            if (Vector3.Distance(transform.position, LaserDestination) <= 0.01f) // 더 좋은 방법?
            {

                if (gameObject.activeSelf)
                {
                    Lt.StartCoroutine(nameof(Lt.IE_CoolTime));
                }


                DestroyBullet();

                Lt.Laser.SetActive(false);

                Lt.PS_Fire.SetActive(false);
                
                
            }

            // 이동하는 동안 콜라이더 받아서 부딪히는 적들 데미지 입히기 -> Trigger로 받는 것
            // 다 돌면 딜레이 넣어주기
        }
        else
        {
            if (target.activeSelf == false)
            {
                target = null;
                DestroyBullet();
                return;
            }


            Vector3 dir = target.transform.position - transform.position;
            transform.LookAt(target.transform);



            if (dir.magnitude <= distanceThisFrame)
            {
                HitTarget();
                return;
            }

            transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        }
    }

    void LaserTarget()
    {
        Transform temp = target.transform;

        Vector3 dir = -temp.forward;

        dir.y = 0f; // y축 고정

        dir = dir.normalized * LaserLength;

        LaserDestination = temp.position + dir;
    }


    private void DestroyBullet()
    {
        ObjectPools.Instance.ReleaseObjectToPool(gameObject);
    }


    void HitTarget()
    {
        // 데미지 전달함수 추가

        AttackEnemy(target);



        if (tower != null)
        {
            if (tower.TowerName == ETowerName.Missile)
            {

                GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Enemy");

                for (int i = 0; i < Enemies.Length; i++)
                {
                    float distanceToEnemy = Vector3.Distance(target.transform.position, Enemies[i].transform.position); // 적과의 거리 구하기


                    if (distanceToEnemy <= MissileRange) // 사거리 안에 있는 타겟들
                    {
                        AttackEnemy(Enemies[i]);
                    }
                }

                GameObject ex = ObjectPools.Instance.GetPooledObject(Explosion.name);
                ex.transform.position = transform.position;
                ex.transform.localScale = new Vector3(MissileRange, MissileRange, MissileRange) / 4;
                ParticleSystem parts = ex.GetComponent<ParticleSystem>();

                if (gameObject.activeSelf)
                {
                    StartCoroutine(IE_psDelay(1.4f, ex));
                }

            }

        }

        DestroyBullet();

    }

    IEnumerator IE_psDelay(float time, GameObject ps)
    {
        WaitForSeconds ws = new(time);

        yield return ws;

        ObjectPools.Instance.ReleaseObjectToPool(ps);
    }



}