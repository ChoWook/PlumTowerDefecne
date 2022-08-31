using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    // Ÿ�� ���� (�ӽ÷� ȭ��Ÿ�� ���� ����)

    [SerializeField]
    private int TowerID = 0;                            // Ÿ�� ID (������ ���̺�)


    [Header("Attributes")]

    public int Range = 3;                                  // ���� ��Ÿ�
    private float SpeedStat = 0.25f;                       // ���� �ӵ� ����(���������̺�)
    private float FireCountdown = 0f;                      // �߻� ī��Ʈ�ٿ�

    

    public string TowerName = "ȭ��Ÿ��";                  // Ÿ�� �̸�
    private int AttackPropertyID = 0;                      // ���� �Ӽ�(���������̺�)
    private int TypeID = 0;                                // �Ӽ� ID (���������̺�)
    private int SizeID = 0;                                // Ÿ�� ũ�� (���������̺�)
    private int AttackStat = 25;                           // ���ݷ� ����(���������̺�)
    private int AbilityStat;                               // Ư�� �ɷ� ����(���������̺�)
    

    public Transform PartToRotate;                         //ȸ�� ������Ʈ
    public float TurnSpeed = 10f;                          //ȸ�� �ӵ�



    [Header("Interactions")]

    public bool Selected = false;                           //Ÿ�� ���� ����
    public bool Fixed = false;                              //Ÿ�� ��ġ ����

    public int AttackPriorityID =0;                         //�켱 ���� �Ӽ� ID

    private int UpgradePrice = 40;                          // ���׷��̵� ����(���������̺�)
    private int UpgradeCount = 0;                           // ���׷��̵� Ƚ��
    private int UpgradeAmount = 5;                          // ���׷��̵� ��ȭ��

    private int Price = 100;                                // ���� ����(���������̺�)
    private int SellPrice;                                  // �Ǹ� ����



    public GameObject BulletPrefab;
    public Transform FirePoint;
    public GameObject Boundary;                             //��Ÿ� Cylinder
    private int ProjectileSpeed = 100;                      //����ü �ӵ�

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
        if (Target == null || Target.GetComponent<Enemy>().IsAlive == false)
        {
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


        // Ÿ�� ���� ǥ��
        /*
        if (Input.GetMouseButtonDown(0))

        {

            RaycastHit hit;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            Physics.Raycast(ray, out hit);



            if (hit.collider != null)

            {

                CurrentTouch = hit.transform.gameObject;

                EventActivate();

            }

        }

        */
    }

    // ���� �˰���� �ڷ�ƾ
    IEnumerator IE_GetTargets()
    {
        //��Ÿ� �ȿ� ���� ���� EnemyList�� ���� + ��Ÿ����� ������ �����.
    

        yield return new WaitForSeconds(0.5f);
    }

    // ���� �켱���� ���ϴ� �Լ�
    private void SortAttackPriority()
    {
        switch(AttackPriorityID)
        {
            case 0:
                // ���� ���� ����

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

    void Shoot()
    {
        ObjectPool = GameObject.Find("ObjectPool");

        GameObject bulletGO = ObjectPool.GetComponent<ObjectPools>().GetPooledObject("Arrow");
        bulletGO.transform.position = FirePoint.position;

        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
            bullet.Seek(Target, AttackStat, AttackPropertyID);

    }

    // ��ȣ�ۿ� �Լ�

    //Upgrade
    void UpgradeTower()
    {
        //Attackstat + 5 �� �س���
        AttackStat += UpgradeAmount;


        //�� 40 �ұ�

        GameManager.instance.money -= 40;

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
