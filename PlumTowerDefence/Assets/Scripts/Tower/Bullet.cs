using UnityEngine;


public class Bullet : MonoBehaviour
{
    private GameObject target;            // Ÿ��

    //public GameObject ObjectPool;

    private double Damage;               // Ÿ�ٿ��� ���� ������

    //private int AttackPropertyID;        // �Ӽ� ���̵�

    private EAttackSpecialization AttackSepcialization; // ���� ����


    public float Speed;

    public void Awake()
    {
        
    }

    public void Seek (GameObject _target, float _Speed,float _Damage, EAttackSepcialization _AttackSpecialization)
    {
        target = _target;
        Speed = _Speed;
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
        ObjectPools.Instance.ReleaseObjectToPool(gameObject);
    }


    void HitTarget()
    {
        // ������ �����Լ� �߰�

        target.GetComponent<Enemy>().TakeDamage((float)Damage); //Damage ���� -> ���߿� �Ӽ� �߰�
        
        DestroyBullet();
    }




}
