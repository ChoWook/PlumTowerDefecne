using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bullet : MonoBehaviour
{
    private GameObject target;            // 타겟

    public Tower tower;

    //public GameObject ObjectPool;

    private float Damage;               // 타겟에게 가할 데미지

    //private int AttackPropertyID;        // 속성 아이디

    private EAttackSpecialization AttackSpecialization; // 공격 종류

    public float Speed;

    public float MissileRange;

    public float ElectricRange;

    public float ElecRangeStat = 2f;

    private void OnEnable()
    {
        if (tower == null) return;

        MissileRange = tower.AbilityStat * GameManager.instance.unitTileSize;
        ElectricRange = ElecRangeStat * GameManager.instance.unitTileSize; // 타일 2개
    }

    public void Seek(GameObject _target, float _Speed, float _Damage, EAttackSpecialization _AttackSpecialization)
    {
        target = _target;
        Speed = _Speed;
        Damage = _Damage;
        AttackSpecialization = _AttackSpecialization;
    }



    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            DestroyBullet();
            return;
        }

        if (target.activeSelf == false)
        {
            target = null;
            DestroyBullet();
            return;
        }

        Vector3 dir = target.transform.position - transform.position;
        float distanceThisFrame = Speed * Time.deltaTime;
        transform.LookAt(target.transform);

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    private void DestroyBullet()
    {
        ObjectPools.Instance.ReleaseObjectToPool(gameObject);
    }


    void HitTarget()
    {
        // 데미지 전달함수 추가

        target.GetComponent<Enemy>().TakeDamage(Damage, AttackSpecialization); //Damage 전달

        DestroyBullet();

        if (tower != null)
        {
            switch (tower.TowerName)
            {
                case ETowerName.Missile:
                    {
                        GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Enemy");

                        for (int i = 0; i < Enemies.Length; i++)
                        {
                            float distanceToEnemy = Vector3.Distance(target.transform.position, Enemies[i].transform.position); // 적과의 거리 구하기

                            // Debug.Log("DistanceEnemy" + distanceToEnemy);

                            if (distanceToEnemy <= MissileRange) // 사거리 안에 있는 타겟들
                            {
                                Enemies[i].GetComponent<Enemy>().TakeDamage(Damage, AttackSpecialization);
                            }
                        }

                        break;
                    }
                case ETowerName.Electric:
                    {
                        // 타일 2 까지 Ability 수만큼 찾기


                        List<GameObject> EnemiesInRange = new List<GameObject>(); // 범위 안의 몬스터
                        List<float> DistanceInrange = new List<float>(); // 값 비교용 list

                        GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Enemy");


                        for (int i = 0; i < Enemies.Length; i++)
                        {
                            float distanceToEnemy = Vector3.Distance(target.transform.position, Enemies[i].transform.position); // 적과의 거리 구하기

                            if (distanceToEnemy <= ElectricRange)
                            {
                                if (EnemiesInRange.Count == 0)
                                {
                                    EnemiesInRange.Add(Enemies[i]);
                                    DistanceInrange.Add(distanceToEnemy);
                                    continue;
                                }

                                // 길이 비교 함수

                                int idx = 0;

                                for (int j = 0; j < EnemiesInRange.Count; j++)
                                {
                                    if(distanceToEnemy > DistanceInrange[j])
                                    {
                                        idx = j;
                                        break;
                                    }    
                                }
                                
                                EnemiesInRange.Insert(idx,Enemies[i]);
                                DistanceInrange.Insert(idx, distanceToEnemy);
                            }

                        }

                        // AbilityStat만큼 공격하기
                        for(int i=0; i < tower.AbilityStat; i++)
                        {
                            EnemiesInRange[i].GetComponent<Enemy>().TakeDamage(Damage, AttackSpecialization);
                        }

                        break;

                    }


            }

        }

    }

}
