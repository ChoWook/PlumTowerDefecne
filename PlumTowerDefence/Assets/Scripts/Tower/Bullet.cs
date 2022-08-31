using UnityEngine;


public class Bullet : MonoBehaviour
{
    private Transform target;            // 타겟

    public GameObject ObjectPool;

    private double Damage;               // 타겟에게 가할 데미지

    private int AttackPropertyID;        // 속성 아이디


    public float Speed = 10f;

    public void Seek (Transform _target, double _Damage, int _AttackPropertyID)
    {
        target = _target;
        Damage = _Damage;
        AttackPropertyID = _AttackPropertyID;
    }



    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            DestroyBullet();
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = Speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    private void DestroyBullet()
    {
        ObjectPool = GameObject.Find("ObjectPool");

        ObjectPool.GetComponent<ObjectPools>().ReleaseObjectToPool(gameObject);
    }


    void HitTarget()
    {
        // 데미지 전달함수 추가

        target.GetComponent<Enemy>().TakeDamage((float)Damage); //Damage 전달 -> 나중에 속성 추가
        
        DestroyBullet();
    }




}
