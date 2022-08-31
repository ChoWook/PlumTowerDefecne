using UnityEngine;


public class Bullet : MonoBehaviour
{
    private Transform target;            // Ÿ��

    public GameObject ObjectPool;

    private double Damage;               // Ÿ�ٿ��� ���� ������

    private int AttackPropertyID;        // �Ӽ� ���̵�


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
        // ������ �����Լ� �߰�

        target.GetComponent<Enemy>().TakeDamage((float)Damage); //Damage ���� -> ���߿� �Ӽ� �߰�
        
        DestroyBullet();
    }




}
