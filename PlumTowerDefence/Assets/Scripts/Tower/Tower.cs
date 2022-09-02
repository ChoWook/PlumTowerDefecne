using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    // Ÿ�� ���� (�ӽ÷� ȭ��Ÿ�� ���� ����)

    [SerializeField]
    public int TowerID;                            // Ÿ�� ID (������ ���̺�)


    [Header("Attributes")]

    public float Range;                               // ���� ��Ÿ�
    public float SpeedStat;                       // ���� �ӵ� ����(���������̺�)
    private float FireCountdown = 0f;                      // �߻� ī��Ʈ�ٿ�

    

    public string TowerName = "ȭ��Ÿ��";                  // Ÿ�� �̸�
    protected EAttackSepcialization AttackSpecialization;                      // ���� �Ӽ�(���������̺�)
    protected ETowerType TypeID;                             // �Ӽ� ID (���������̺�)
    protected int Size;                                // Ÿ�� ũ�� (���������̺�)
    protected float AttackStat;                           // ���ݷ� ����(���������̺�)
    protected int AbilityStat;  

    public Transform PartToRotate;                         //ȸ�� ������Ʈ
    public float TurnSpeed = 10f;                          //ȸ�� �ӵ�



    [Header("Interactions")]

    public bool Selected = false;                           //Ÿ�� ���� ����
    public bool Fixed = false;                              //Ÿ�� ��ġ ����

    // public int AttackPriorityID =0;                         //�켱 ���� �Ӽ� ID

    protected EUpgradeStat UpgradeStat;                                 // ���׷��̵� ���
    protected int UpgradePrice;                          // ���׷��̵� ����(���������̺�)
    private int UpgradeCount = 0;                           // ���׷��̵� Ƚ��
    protected float UpgradeAmount;                          // ���׷��̵� ��ȭ��

    protected int Price;                                // ���� ����(���������̺�)
    private int SellPrice;                                  // �Ǹ� ����



    public GameObject BulletPrefab;
    public Transform FirePoint;
    public GameObject Boundary;                             //��Ÿ� Cylinder
    protected float ProjectileSpeed;                      //����ü �ӵ�

    public GameObject ObjectPool;



    // �� ���� (Ÿ�� ����)

    [Header("Enemy")]

    public List<GameObject> EnemyLIst = new List<GameObject>();

    public GameObject Target;

    public string enemyTag = "Enemy";

    

    // Start is called before the first frame update
    void Start()
    {

        //��Ÿ� ���� Range �� �ֱ�
        Transform parent = transform.parent;
        transform.parent = null;
        Boundary.transform.localScale = new Vector3(Range, 0.05f, Range);
        transform.parent = parent;

        InvokeRepeating("UpdateTarget", 0, 0.5f); // 0.5�� ���� �ݺ��ϱ�

    }

    // Ÿ�� ������Ʈ ( ü�� �켱, �� �켱, ���� ���� �� �켱 �߰��ϱ�)
    void UpdateTarget()
    {
        /*
        
        SortAttackPriority();

        */


        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag); // Enemy  �±׷� �� ã��
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position); // ������ �Ÿ� ���ϱ�
            if(distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        
        }

        if (nearestEnemy != null && shortestDistance <= Range)
        {
            Target = nearestEnemy;

        } else
        {
            Target = null;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Target == null)
        {
            return;
        } else if (Target.GetComponent<Enemy>().IsAlive == false)
        {
            //EnemyLIst.RemoveAt(0);
            return;
        }
        // Ÿ�� ȸ��
        
        Vector3 dir = Target.transform.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(PartToRotate.rotation,lookRotation,Time.deltaTime * TurnSpeed).eulerAngles;
        PartToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        // �߻�

        if (FireCountdown <= 0f)
        {
            Shoot();
            FireCountdown = 1f / SpeedStat;
        }

        FireCountdown -= Time.deltaTime;
    }

    // ���� �˰��� �ڷ�ƾ
    IEnumerator IE_GetTargets()
    {
        //��Ÿ� �ȿ� ���� ���� EnemyList�� ���� + ��Ÿ����� ������ �����.
    

        yield return new WaitForSeconds(0.5f);
    }

    /*
    // ���� �켱���� ���ϴ� �Լ�
    private void SortAttackPriority()
    {
        switch(AttackPriorityID)
        {
            case 0:
                // ���� ���� ����
                // �̷��� �ϸ� �ǳ�??????
                Target = EnemyLIst[0];

                if (Target == null)
                {
                    EnemyLIst.RemoveAt(0);
                   }
                break;

            case 1:
                // ü�� �켱 ���� -> �� ���� �� ���� �� ������ ó�� ���� ����
                
                break;

            case 2:
                // �� ���� ���� �켱 ����
                
                break;

            case 3:
                // ���� ���� �� �켱 ����

                break;
        }    
    }

    */

    void Shoot()
    {
        ObjectPool = GameObject.Find("ObjectPool");

        GameObject bulletGO = ObjectPool.GetComponent<ObjectPools>().GetPooledObject("Arrow");
        bulletGO.transform.position = FirePoint.position;

        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
            bullet.Seek(Target, AttackStat, AttackSpecialization);

    }

    // ��ȣ�ۿ� �Լ�

    //Upgrade
    void UpgradeTower()
    {
        //Attackstat + 5 �� �س���
        AttackStat += UpgradeAmount;


        //�� 40 �ұ�

        GameManager.instance.money -= 40;

        UpgradeCount++;

    }

    //Sell
    void SellTower()
    {
        // Ÿ�� �ݳ��ϱ�
        Destroy(gameObject); // Ÿ�� Ǯ�� �߰��� �� �ٲٱ�


        // �� �ޱ� (Ÿ�� ��ġ��� + ���׷��̵� ��� ) * 0.6
        double _SellPrice = (Price + UpgradeCount * UpgradePrice) * 0.6;

        SellPrice = (int)_SellPrice;


        // ��ȭ ���� �Լ�

        GameManager.instance.money += SellPrice;

    }

    //Move
    void MoveTower()
    {
        // ��ġ ���·� ���ư�

        // �߰��� ��� �����ϰ�

        // �� ����
        double _MovePrice = (Price + UpgradeCount * UpgradePrice) * 0.5;


        int MovePrice = (int)_MovePrice;


        // ��ȭ ���� �Լ�

        GameManager.instance.money -= MovePrice;

    }

    /*
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Range);
    }
    */

}
