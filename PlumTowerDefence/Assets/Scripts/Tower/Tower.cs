using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    // Ÿ�� ���� (�ӽ÷� ȭ��Ÿ�� ���� ����)

    [SerializeField]
    public int TowerID;                                       // Ÿ�� ID (������ ���̺�)


    [Header("Attributes")]

    //public GameObject MarkSizePrefab;                                               // Size ǥ�� ������Ʈ
    //public GameObject MarkRangePrefab;                                              // Range ǥ�� ������Ʈ


    public float Range;                                                              // ���� ��Ÿ�
    public float RealRange;                                                          // ���� ��Ÿ�


    public ETowerName TowerName;                                                     // Ÿ�� �̸�
    protected EAttackSpecialization AttackSpecialization;                            // ���� �Ӽ�(���������̺�)
    protected ETowerType TypeID;                                                     // �Ӽ� ID (���������̺�)

    public int Size;                                                                 // Ÿ�� ũ�� (���������̺�)
    public float RealSize;                                                             // ���� Ÿ�� ũ��

    // AttackStat
    public float BaseAttackStat;                                                     // ���ݷ� ����(���������̺�)
                                                                                     // public float AttackStat;                                                       // ���� ���ݷ� ����
    static List<float> AttackPlusModifier = new List<float>();                       // AttackStat �ݿ� ����Ʈ (����)
    static List<float> AttackMultiModifier = new List<float>();                      // AttackStat �ݿ� ����Ʈ (����)

    // AbilityStat
    public float BaseAbilityStat;                                                       //�����Ƽ ����(���������̺�)
    //float AbilityStat;                                                                //���� �����Ƽ ����
    static List<float> AbilityPlusModifier = new List<float>();                        //AbilityStat �ݿ� ����Ʈ (����)
    static List<float> AbilityMultiModifier = new List<float>();                        //AbilityStat �ݿ� ����Ʈ (����)


    // SpeedStat
    public float BaseSpeedStat;                                                              // ���� �ӵ� ����(���������̺�)
    // float SpeedStat;                                                                     // ���� �ӵ� ����
    static List<float> SpeedPlusModifier = new List<float>();                              // SpeedStat �ݿ�����Ʈ(����)
    static List<float> SpeedMultiModifier = new List<float>();                             // SpeedStat �ݿ�����Ʈ(����)

    private float FireCountdown = 0f;                                                       // �߻� ī��Ʈ�ٿ�

    // Buff��
    public float AttackBuffAmount;
    public float SpeedBuffAmount;

    public Transform PartToRotate;                                                          //ȸ�� ������Ʈ
    public float TurnSpeed = 10f;                                                           //ȸ�� �ӵ�




    [Header("Interactions")]

    public Tile belowTile;

    public bool Selected = false;                                                           // Ÿ�� ���� ����
    public bool Fixed = false;                                                             // Ÿ�� ��ġ ���� < -  �ʿ��Ѱ�?

    public int AttackPriorityID = 0;                                                       // �켱 ���� �Ӽ� ID

    public EUpgradeStat UpgradeStat;                                                       // ���׷��̵� ���
    public int UpgradePrice;                                                               // ���׷��̵� ����(���������̺�)
    public int UpgradeCount;                                                               // ���׷��̵� Ƚ��
    public float UpgradeAmount;                                                            // ���׷��̵� ��ȭ��

    protected int Price;                                                                   // ���� ����(���������̺�)
    public int SellPrice;                                                                  // �Ǹ� ����
    public int MovePrice;                                                                  // �̵� ����

    public bool CheckAttackBuff;                                                          //���� �ް� �ִ��� Ȯ��
    public bool CheckSpeedBuff;                                                           //���� �ް� �ִ��� Ȯ��

    public GameObject BulletPrefab;
    public Transform FirePoint;
    public float ProjectileSpeed;                                                         //����ü �ӵ�




    // �� ���� (Ÿ�� ����)

    [Header("Enemy")]

    public List<GameObject> EnemyList = new List<GameObject>();

    public GameObject Target;

    public const string enemyTag = "Enemy";


    //get �޾��ֱ� <- class�� �ϳ��� ����� ���ڴ�!

    public float AttackStat 
    {
        get
        {
            float sum = 0f;

            for (int i = 0; i < AttackPlusModifier.Count; i++)
            {
                sum += AttackPlusModifier[i];
            }

            float multi = 1f;

            for (int i = 0; i < AttackMultiModifier.Count; i++)
            {
                multi *= AttackMultiModifier[i];
            }

            return (BaseAttackStat + sum + AttackBuffAmount) * multi;
        } // ����?
    }

    public float SpeedStat
    {
        get
        {
            float sum = 0f;

            for (int i = 0; i < SpeedPlusModifier.Count; i++)
            {
                sum += SpeedPlusModifier[i];
            }

            float multi = 1f;

            for (int i = 0; i < SpeedMultiModifier.Count; i++)
            {
                multi *= SpeedMultiModifier[i];
            }

            return (BaseSpeedStat + sum + SpeedBuffAmount) * multi;
        }
    }

    public float AbilityStat
    {
        get
        {

            float sum = 0f;

            for (int i = 0; i < AbilityPlusModifier.Count; i++)
            {
                sum += AbilityPlusModifier[i];
            }

            float multi = 1f;

            for (int i = 0; i < AbilityMultiModifier.Count; i++)
            {
                multi *= AbilityMultiModifier[i];
            }


            return (BaseAbilityStat + sum) * multi;
        }
    }




    // Select�� �� ��Ÿ� ǥ��

    public void IsSelected(bool sender)
    {
        Selected = sender;

        //��Ÿ�, Ÿ�� ������ ǥ�� Ȱ��ȭ

        //MarkSizePrefab.SetActive(sender);
        //MarkRangePrefab.SetActive(sender);

        GetComponent<TowerSizeController>().IsSelected(sender);

    }


    private void OnEnable()
    {
        //���� �ʱ�ȭ
        AttackBuffAmount = 0f;
        SpeedBuffAmount = 0f;

        UpgradeCount = 0;

        RealRange = Range * GameManager.instance.unitTileSize;

        RealSize = Size * GameManager.instance.unitTileSize;

        Debug.Log("RealRange : " + RealRange);
        Debug.Log("RealSize : " + RealSize);

        // ��Ÿ�, Ÿ�� ������ �����ϱ� <- �� �ǳ�!

        //MarkRangePrefab.transform.localScale = new Vector3(RealRange, 0.05f, RealRange);
        //MarkSizePrefab.transform.localScale = new Vector3(RealSize, 0.05f, RealSize);




        /*
        //��Ÿ� ���� Range �� �ֱ�
        Transform parent = transform.parent;
        transform.parent = null;
        //Boundary.transform.localScale = new Vector3(RealRange, 0.05f, RealRange);
        transform.parent = parent;
        */

        StartCoroutine(IE_GetTargets());

    }

    // ���� �˰��� �ڷ�ƾ
    protected virtual IEnumerator IE_GetTargets()
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


                if (distanceToEnemy <= RealRange) // ��Ÿ� �ȿ� �ִ� Ÿ�ٵ�
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

        }
        else if (Target.GetComponent<Enemy>().IsAlive == false)
        {
            return;
        }
        else if (Vector3.Distance(transform.position, Target.transform.position) > RealRange)
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
        {
            Target = Sender;
        }

    }


    // ���� �켱���� ���ϴ� �Լ�
    private void SortAttackPriority()
    {
        switch (AttackPriorityID)
        {
            case 0:
                // ���� ���� ����
                if (EnemyList.Count != 0)
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

    //����Ÿ�� �������� List���� ��� ����?

    //���ݷ� ����
    public void GetAttackBuff(float _BuffAmount)
    {
        AttackBuffAmount += _BuffAmount;
    }


    // ���ݼӵ� ����
    public void GetSpeedBuff(float _BuffAmount)
    {
        SpeedBuffAmount += _BuffAmount;
    }


    // ��ȣ�ۿ� �Լ�

    //Upgrade
    void UpgradeTower() // ������ �����ؼ� �����ϱ�
    {
        //Attackstat + 5 �� �س���

        switch (UpgradeStat)
        {
            case EUpgradeStat.Attack:
                {
                    AttackPlusModifier.Add(UpgradeAmount);
                    break;
                }

            case EUpgradeStat.Ability:
                {
                    AbilityPlusModifier.Add(UpgradeAmount);
                    break;
                }

            case EUpgradeStat.Speed:
                {
                    SpeedPlusModifier.Add(UpgradeAmount);
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
        Destroy(gameObject); // Ÿ�� Ǯ�� �߰��� �� �ٲٱ� ?  <- �ʿ��� �ٷ��!


        // �� �ޱ� (Ÿ�� ��ġ��� + ���׷��̵� ��� ) * 0.6
        float _SellPrice = (Price + UpgradeCount * UpgradePrice) * 0.6f;

        SellPrice = (int)_SellPrice;


        // ��ȭ ���� �Լ�

        GameManager.instance.money += SellPrice;

        // ����, ���� ���� Ÿ���� ��  ���� ���� ȿ�� �־��ֱ�

    }

    //Move
    void MoveTower()
    {
        // ��ġ ���·� ���ư�

        // �߰��� ��� �����ϰ�

        // �� ����
        float _MovePrice = (Price + UpgradeCount * UpgradePrice) * 0.5f;


        MovePrice = (int)_MovePrice;


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
        BaseAttackStat = tower._Attack;
        BaseSpeedStat = tower._Speed;
        BaseAbilityStat = tower._Ability;
        ProjectileSpeed = tower._ProjectileSpeed;
        UpgradeStat = tower._UpgradeStat;
        UpgradeAmount = tower._UpgradeAmount;
        UpgradePrice = tower._UpgradePrice;
        Range = tower._Range;
        Price = tower._Price;

    }



}
