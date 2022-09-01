using UnityEngine;


public class Bullet : MonoBehaviour
{
    private GameObject target;            // 타겟

    public GameObject ObjectPool;

    private double Damage;               // 타겟에게 가할 데미지

    //private int AttackPropertyID;        // 속성 아이디

    private EAttackSepcialization AttackSepcialization; // 공격 종류


    public float Speed = 1f;

    public void Seek (GameObject _target, double _Damage, EAttackSepcialization _AttackSpecialization)
    {
        target = _target;
        Damage = _Damage;
        AttackSepcialization = _AttackSpecialization;
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
