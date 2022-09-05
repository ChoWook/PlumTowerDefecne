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

    private void OnEnable()
    {
        if (tower == null) return;
        MissileRange = tower.AbilityStat * GameManager.instance.unitTileSize;
    }

    public void Seek (GameObject _target, float _Speed,float _Damage, EAttackSpecialization _AttackSpecialization)
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

        if(tower != null)
        {
            if (tower.TowerName == ETowerName.Missile)
            {

                GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Enemy");

                for (int i = 0; i < Enemies.Length; i++) //Ÿ�� ��Ÿ� ������ �ٲ�� ��~~~.
                {
                    float distanceToEnemy = Vector3.Distance(target.transform.position, tower.EnemyList[i].transform.position); // ������ �Ÿ� ���ϱ�

                    // Debug.Log("DistanceEnemy" + distanceToEnemy);

                    if (distanceToEnemy <= MissileRange) // ��Ÿ� �ȿ� �ִ� Ÿ�ٵ�
                    {
                        target.GetComponent<Enemy>().TakeDamage(Damage, AttackSpecialization);
                    }
                }
            }
        }

    }




}
