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
        // 데미지 전달함수 추가

        target.GetComponent<Enemy>().TakeDamage(Damage, AttackSpecialization); //Damage 전달
        
        DestroyBullet();

        if(tower != null)
        {
            if (tower.TowerName == ETowerName.Missile)
            {

                GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Enemy");

                for (int i = 0; i < Enemies.Length; i++) //타워 사거리 상관없어서 바꿔야 함~~~.
                {
                    float distanceToEnemy = Vector3.Distance(target.transform.position, tower.EnemyList[i].transform.position); // 적과의 거리 구하기

                    // Debug.Log("DistanceEnemy" + distanceToEnemy);

                    if (distanceToEnemy <= MissileRange) // 사거리 안에 있는 타겟들
                    {
                        target.GetComponent<Enemy>().TakeDamage(Damage, AttackSpecialization);
                    }
                }
            }
        }

    }




}
