using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    // Ÿ�� ���� (�ӽ÷� ȭ��Ÿ�� ���� ����)

    [SerializeField]
    public int TowerID;                                       // Ÿ�� ID (������ ���̺�)


    [Header("Attributes")]

    public float Range;                                       // ���� ��Ÿ�
    public float RealRange;                                   // ���� ��Ÿ�
    public float SpeedStat;                                   // ���� �ӵ� ����(���������̺�)
    private float FireCountdown = 0f;                         // �߻� ī��Ʈ�ٿ�


    public ETowerName TowerName;                              // Ÿ�� �̸�
    protected EAttackSpecialization AttackSpecialization;    // ���� �Ӽ�(���������̺�)
    protected ETowerType TypeID;                             // �Ӽ� ID (���������̺�)
    public int Size;                                      // Ÿ�� ũ�� (���������̺�)
    public float AttackStat;                                 // ���ݷ� ����(���������̺�)
    public float AbilityStat;

    public Transform PartToRotate;                           //ȸ�� ������Ʈ
    public float TurnSpeed = 10f;                            //ȸ�� �ӵ�



    [Header("Interactions")]

    public Tile belowTile;

    public bool Selected = false;                           //Ÿ�� ���� ����
    public bool Fixed = false;                              //Ÿ�� ��ġ ����

    public int AttackPriorityID = 0;                        //�켱 ���� �Ӽ� ID

    protected EUpgradeStat UpgradeStat;                    // ���׷��̵� ���
    protected int UpgradePrice;                            // ���׷��̵� ����(���������̺�)
    public int UpgradeCount = 0;                           // ���׷��̵� Ƚ��
    protected float UpgradeAmount;                         // ���׷��̵� ��ȭ��

    protected int Price;                                   // ���� ����(���������̺�)
    private int SellPrice;                                 // �Ǹ� ����

    public bool CheckAttackBuff;                           //���� �ް� �ִ��� Ȯ��
    public bool CheckSpeedBuff;                           //���� �ް� �ִ��� Ȯ��

    public GameObject BulletPrefab;
    public Transform FirePoint;
    protected float ProjectileSpeed;                       //����ü �ӵ�




    // �� ���� (Ÿ�� ����)

    [Header("Enemy")]

    public List<GameObject> EnemyList = new List<GameObject>();

    public GameObject Target;

    public string enemyTag = "Enemy";



    // Start is called before the first frame update
    void Start()
    {

    }


    private void OnEnable()
    {

        RealRange = Range * GameManager.instance.unitTileSize; //TileSize ���߿� GameManager�� �ޱ�


        /*
        //��Ÿ� ���� Range �� �ֱ�
        Transform parent = transform.parent;
        transform.parent = null;
        //Boundary.transform.localScale = new Vector3(RealRange, 0.05f, RealRange);
        transform.parent = parent;
        */

        StartCoroutine(IE_GetTargets());
        //InvokeRepeating("UpdateTarget", 0, 0.5f); // 0.5�� ���� �ݺ��ϱ�

    }

    // ���� �˰��� �ڷ�ƾ
    IEnumerator IE_GetTargets()
    {
        WaitForSeconds ws = new WaitForSeconds(0.5f);

        //��Ÿ� �ȿ� ���� ���� EnemyList�� ���� + ��Ÿ����� ������ �����.

        while (true)
        {
            //SortAttackPriority();
            UpdateTarget();

            yield return ws;
        }

    }


    // Ÿ�� ������Ʈ ( ü�� �켱, �� �켱, ���� ���� �� �켱 �߰��ϱ�)
    protected virtual void UpdateTarget()
    {

        

        if (Target == null || Target.GetComponent<Enemy>().IsAlive == false || Vector3.Distance(transform.position, Target.transform.position) > RealRange)
        {
            EnemyList.Clear();

            GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag); // Enemy  �±׷� �� ã��
            float shortestDistance = Mathf.Infinity;
            GameObject nearestEnemy = null;

            foreach (GameObject enemy in enemies)
            {
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position); // ������ �Ÿ� ���ϱ�


                if(distanceToEnemy <= RealRange) // ��Ÿ� �ȿ� �ִ� Ÿ�ٵ�
                {
                    EnemyList.Add(enemy);
                }

                if (distanceToEnemy < shortestDistance)  // �켱���� ã�� SortAttackPriority();
                {
                    shortestDistance = distanceToEnemy;
                    nearestEnemy = enemy;
                }

            }


            if (nearestEnemy != null && shortestDistance <= RealRange)
            {
                Target = nearestEnemy;

            }
            else
            {
                Target = null;
            }
        }
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {

        if (Target == null)
        {
            return;

        } else if (Target.GetComponent<Enemy>().IsAlive == false)
        {
            return;
        }


        if (PartToRotate != null)
        {
            // Ÿ�� ȸ��
            Vector3 dir = Target.transform.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = Quaternion.Lerp(PartToRotate.rotation, lookRotation, Time.deltaTime * TurnSpeed).eulerAngles;
            PartToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        }
        

        // �߻�
        if (FireCountdown <= 0f)
        {
            Shoot();
            FireCountdown = 1f / SpeedStat;
        }

        FireCountdown -= Time.deltaTime;

       
    }



    public void SetTarget(GameObject Sender)
    {
        if (Vector3.Distance(transform.position, Sender.transform.position) <= RealRange)
            Target = Sender;
    }




    
    // ���� �켱���� ���ϴ� �Լ�
    private void SortAttackPriority()
    {
        switch(AttackPriorityID)
        {
            case 0:
                // ���� ���� ����
                if (EnemyList.Count != 0 )
                {
                    Target = EnemyList[0];
                }
                    
                break;

            case 1:
                // ü�� �켱 ���� -> �� ���� �� ���� Ÿ�����ϰ� �� ������ ó�� ���� ����
                
                break;

            case 2:
                // �� ���� ���� �켱 ���� Enemy.CurrentShield
                
                break;

            case 3:
                // ���� ���� �� �켱 ����
                //EnemyList.Sort(gameObject.GetComponent<Enemy>().) <- �� ���� public���� �ٲ��ּ���~

                break;
        }    
    }

    
    public virtual void Shoot() // ����
    {

        if (BulletPrefab != null)
        {
            GameObject bulletGO = ObjectPools.Instance.GetPooledObject(BulletPrefab.name);
            bulletGO.transform.position = FirePoint.position;

            bulletGO.GetComponent<Bullet>()?.Seek(Target, ProjectileSpeed, AttackStat, AttackSpecialization);
        }

    }

    //���ݷ� ����
    public void GetAttackBuff(float _BuffAmount)
    {
        AttackStat += _BuffAmount;
    }


    // ���ݼӵ� ����
    public void GetSpeedBuff(float _BuffAmount)
    {
        SpeedStat += _BuffAmount;
    }


    // ��ȣ�ۿ� �Լ�

    //Upgrade
    void UpgradeTower() // ������ �����ؼ� �����ϱ�
    {
        //Attackstat + 5 �� �س���
        
        switch(UpgradeStat)
        {
            case EUpgradeStat.Attack:
                {
                    AttackStat += UpgradeAmount;
                    break;
                }
               
            case EUpgradeStat.Ability:
                {
                    AbilityStat += UpgradeAmount;
                    break;
                }
                
            case EUpgradeStat.Speed:
                {
                    SpeedStat += UpgradeAmount;
                    break;
                }
                

        }

        //�� 40 �ұ�

        GameManager.instance.money -= 40;

        UpgradeCount++;

    }

    //Sell
    void SellTower()
    {
        // Ÿ�� �ݳ��ϱ�
        Destroy(gameObject); // Ÿ�� Ǯ�� �߰��� �� �ٲٱ� ? 


        // �� �ޱ� (Ÿ�� ��ġ��� + ���׷��̵� ��� ) * 0.6
        float _SellPrice = (Price + UpgradeCount * UpgradePrice) * 0.6f;

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
        float _MovePrice = (Price + UpgradeCount * UpgradePrice) * 0.5f;


        int MovePrice = (int)_MovePrice;


        // ��ȭ ���� �Լ�

        GameManager.instance.money -= MovePrice;

    }

    public void Setstat(ETowerName _TowerName)
    {
        Tables.Tower tower = Tables.Tower.Get(_TowerName);

        TowerID = tower._ID;
        TowerName = tower._Name;
        AttackSpecialization = tower._AttackSepcialization;
        TypeID = tower._Type;
        Size = tower._Size;
        AttackStat = tower._Attack;
        SpeedStat = tower._Speed;
        AbilityStat = tower._Ability;
        ProjectileSpeed = tower._ProjectileSpeed;
        UpgradeStat = tower._UpgradeStat;
        UpgradeAmount = tower._UpgradeAmount;
        UpgradePrice = tower._UpgradePrice;
        Range = tower._Range;
        Price = tower._Price;
        
    }



}
