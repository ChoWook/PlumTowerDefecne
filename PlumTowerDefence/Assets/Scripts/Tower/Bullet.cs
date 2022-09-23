using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bullet : MonoBehaviour
{
    private GameObject target;            // Ÿ��

    public Tower tower;

    public LaserTower Lt;

    //public GameObject ObjectPool;

    private float Damage;               // Ÿ�ٿ��� ���� ������

    //private int AttackPropertyID;        // �Ӽ� ���̵�

    private EAttackSpecialization AttackSpecialization; // ���� ����

    public float Speed;

    public float MissileRange;

    public float ElectricRange;

    public float ElecRangeStat = 2f;

    public float LaserLength;

    public Vector3 LaserDestination = new Vector3();



    public void SetTower(Tower tower)
    {
        this.tower = tower;

        MissileRange = tower.AbilityStat * GameManager.instance.unitTileSize;
        ElectricRange = ElecRangeStat * GameManager.instance.unitTileSize;              // Ÿ�� 2��
        LaserLength = tower.AbilityStat * GameManager.instance.unitTileSize;            // ������ ����


        if (tower.TowerName == ETowerName.Laser)
        {

            // �Ҹ� ũ�� ����
            Transform parent = transform.parent;
            transform.parent = null;
            transform.localScale = new Vector3(GameManager.instance.unitTileSize, 1, GameManager.instance.unitTileSize); // ���� ���� ����
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

    private void OnTriggerEnter(Collider other)
    {
        if(tower.TowerName == ETowerName.Laser)
        {
            other.gameObject.GetComponentInParent<Enemy>()?.TakeDamage(Damage, AttackSpecialization, tower.TowerName); //Damage ����
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
            // ���� �� �ް�
            // Ÿ�� ��ġ���� ����(Ÿ������ ����)
            // ���� y�� �����ϰ� �ش� �������� ability��ŭ ������(���� 1Ÿ��ũ�⸸ŭ �������ֱ�)

            transform.position = Vector3.MoveTowards(transform.position, LaserDestination , distanceThisFrame); // �̰� �ƴϴ�!!!!



            if(Vector3.Distance(transform.position, LaserDestination) <= 0.01f) // �� ���� ���?
            {

                DestroyBullet();
       
                Lt.StartCoroutine(nameof(Lt.IE_CoolTime));
            }
            
            // �̵��ϴ� ���� �ݶ��̴� �޾Ƽ� �ε����� ���� ������ ������ -> Trigger�� �޴� ��
            // �� ���� ������ �־��ֱ�
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

        Vector3 dir = - temp.forward;

        dir.y = 0f; // y�� ����

        dir = dir.normalized * LaserLength;

        LaserDestination = temp.position + dir ;
    }


    private void DestroyBullet()
    {
        ObjectPools.Instance.ReleaseObjectToPool(gameObject);
    }


    void HitTarget()
    {
        // ������ �����Լ� �߰�

        target.GetComponent<Enemy>().TakeDamage(Damage, AttackSpecialization, tower.TowerName); //Damage ����

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


                            if (distanceToEnemy <= MissileRange) // ��Ÿ� �ȿ� �ִ� Ÿ�ٵ�
                            {
                                Enemies[i].GetComponent<Enemy>().TakeDamage(Damage, AttackSpecialization, tower.TowerName);
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
                            EnemiesInRange[i].GetComponent<Enemy>().TakeDamage(Damage, AttackSpecialization, tower.TowerName);
                        }

                        break;

                    }

            }

        }

    }

}
