using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bullet : MonoBehaviour
{
    private GameObject target;            // Ÿ��

    public Tower tower;

    //public GameObject ObjectPool;

    private float Damage;               // Ÿ�ٿ��� ���� ������

    //private int AttackPropertyID;        // �Ӽ� ���̵�

    private EAttackSpecialization AttackSpecialization; // ���� ����

    public float Speed;

    public float MissileRange;

    public float ElectricRange;

    public float ElecRangeStat = 2f;

    private void OnEnable()
    {
        if (tower == null) return;

        MissileRange = tower.AbilityStat * GameManager.instance.unitTileSize;
        ElectricRange = ElecRangeStat * GameManager.instance.unitTileSize; // Ÿ�� 2��
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
        // ������ �����Լ� �߰�

        target.GetComponent<Enemy>().TakeDamage(Damage, AttackSpecialization); //Damage ����

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
                            float distanceToEnemy = Vector3.Distance(target.transform.position, Enemies[i].transform.position); // ������ �Ÿ� ���ϱ�

                            // Debug.Log("DistanceEnemy" + distanceToEnemy);

                            if (distanceToEnemy <= MissileRange) // ��Ÿ� �ȿ� �ִ� Ÿ�ٵ�
                            {
                                Enemies[i].GetComponent<Enemy>().TakeDamage(Damage, AttackSpecialization);
                            }
                        }

                        break;
                    }
                case ETowerName.Electric:
                    {
                        // Ÿ�� 2 ���� Ability ����ŭ ã��


                        List<GameObject> EnemiesInRange = new List<GameObject>(); // ���� ���� ����
                        List<float> DistanceInrange = new List<float>(); // �� �񱳿� list

                        GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Enemy");


                        for (int i = 0; i < Enemies.Length; i++)
                        {
                            float distanceToEnemy = Vector3.Distance(target.transform.position, Enemies[i].transform.position); // ������ �Ÿ� ���ϱ�

                            if (distanceToEnemy <= ElectricRange)
                            {
                                if (EnemiesInRange.Count == 0)
                                {
                                    EnemiesInRange.Add(Enemies[i]);
                                    DistanceInrange.Add(distanceToEnemy);
                                    continue;
                                }

                                // ���� �� �Լ�

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

                        // AbilityStat��ŭ �����ϱ�
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
