using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    // 타워 스텟 (임시로 화살타워 스텟 설정)

    [SerializeField]
    public int TowerID;                                       // 타워 ID (데이터 테이블)


    [Header("Attributes")]

    //public GameObject MarkSizePrefab;                                               // Size 표시 오브젝트
    //public GameObject MarkRangePrefab;                                              // Range 표시 오브젝트


    public float Range;                                                              // 공격 사거리
    public float RealRange;                                                          // 실제 사거리


    public ETowerName TowerName;                                                     // 타워 이름
    protected EAttackSpecialization AttackSpecialization;                            // 공격 속성(데이터테이블)
    protected ETowerType TypeID;                                                     // 속성 ID (데이터테이블)

    public int Size;                                                                 // 타워 크기 (데이터테이블)
    public float RealSize;                                                             // 실제 타워 크기

    // AttackStat
    public float BaseAttackStat;                                                     // 공격력 스텟(데이터테이블)
                                                                                     // public float AttackStat;                                                       // 최종 공격력 스텟
    static List<float> AttackPlusModifier = new List<float>();                       // AttackStat 반영 리스트 (덧셈)
    static List<float> AttackMultiModifier = new List<float>();                      // AttackStat 반영 리스트 (곱셈)

    // AbilityStat
    public float BaseAbilityStat;                                                       //어빌리티 스텟(데이터테이블)
    //float AbilityStat;                                                                //최종 어빌리티 스텟
    static List<float> AbilityPlusModifier = new List<float>();                        //AbilityStat 반영 리스트 (덧셈)
    static List<float> AbilityMultiModifier = new List<float>();                        //AbilityStat 반영 리스트 (곱셈)


    // SpeedStat
    public float BaseSpeedStat;                                                              // 공격 속도 스텟(데이터테이블)
    // float SpeedStat;                                                                     // 최종 속도 스텟
    static List<float> SpeedPlusModifier = new List<float>();                              // SpeedStat 반영리스트(덧셈)
    static List<float> SpeedMultiModifier = new List<float>();                             // SpeedStat 반영리스트(곱셈)

    private float FireCountdown = 0f;                                                       // 발사 카운트다운

    // Buff량
    public float AttackBuffAmount;
    public float SpeedBuffAmount;

    public Transform PartToRotate;                                                          //회전 오브젝트
    public float TurnSpeed = 10f;                                                           //회전 속도




    [Header("Interactions")]

    public Tile belowTile;

    public bool Selected = false;                                                           // 타워 선택 여부
    public bool Fixed = false;                                                             // 타워 설치 여부 < -  필요한가?

    public int AttackPriorityID = 0;                                                       // 우선 공격 속성 ID

    public EUpgradeStat UpgradeStat;                                                       // 업그레이드 대상
    public int UpgradePrice;                                                               // 업그레이드 가격(데이터테이블)
    public int UpgradeCount;                                                               // 업그레이드 횟수
    public float UpgradeAmount;                                                            // 업그레이드 강화량

    protected int Price;                                                                   // 구매 가격(데이터테이블)
    public int SellPrice;                                                                  // 판매 가격
    public int MovePrice;                                                                  // 이동 가격

    public bool CheckAttackBuff;                                                          //버프 받고 있는지 확인
    public bool CheckSpeedBuff;                                                           //버프 받고 있는지 확인

    public GameObject BulletPrefab;
    public Transform FirePoint;
    public float ProjectileSpeed;                                                         //투사체 속도




    // 적 스텟 (타겟 지정)

    [Header("Enemy")]

    public List<GameObject> EnemyList = new List<GameObject>();

    public GameObject Target;

    public const string enemyTag = "Enemy";


    //get 받아주기 <- class로 하나를 만들면 좋겠다!

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
        } // 순서?
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




    // Select일 때 사거리 표시

    public void IsSelected(bool sender)
    {
        Selected = sender;

        //사거리, 타워 사이즈 표시 활성화

        //MarkSizePrefab.SetActive(sender);
        //MarkRangePrefab.SetActive(sender);

        GetComponent<TowerSizeController>().IsSelected(sender);

    }


    private void OnEnable()
    {
        //스탯 초기화
        AttackBuffAmount = 0f;
        SpeedBuffAmount = 0f;

        UpgradeCount = 0;

        RealRange = Range * GameManager.instance.unitTileSize;

        RealSize = Size * GameManager.instance.unitTileSize;

        Debug.Log("RealRange : " + RealRange);
        Debug.Log("RealSize : " + RealSize);

        // 사거리, 타워 사이즈 설정하기 <- 안 되나!

        //MarkRangePrefab.transform.localScale = new Vector3(RealRange, 0.05f, RealRange);
        //MarkSizePrefab.transform.localScale = new Vector3(RealSize, 0.05f, RealSize);




        /*
        //사거리 지정 Range 값 넣기
        Transform parent = transform.parent;
        transform.parent = null;
        //Boundary.transform.localScale = new Vector3(RealRange, 0.05f, RealRange);
        transform.parent = parent;
        */

        StartCoroutine(IE_GetTargets());

    }

    // 추적 알고리즘 코루틴
    protected virtual IEnumerator IE_GetTargets()
    {
        WaitForSeconds ws = new WaitForSeconds(0.5f);

        //사거리 안에 들어온 적들 EnemyList에 정리 + 사거리에서 나가면 지우기.

        while (true)
        {
            //SortAttackPriority();
            UpdateTarget();

            yield return ws;
        }

    }


    // 타겟 업데이트 ( 체력 우선, 방어구 우선, 방어력 높은 적 우선 추가하기)
    protected virtual void UpdateTarget()
    {

        if (Target == null || Target.GetComponent<Enemy>().IsAlive == false || Vector3.Distance(transform.position, Target.transform.position) > RealRange)
        {
            EnemyList.Clear();

            GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag); // Enemy  태그로 적 찾기
            float shortestDistance = Mathf.Infinity;
            GameObject nearestEnemy = null;

            foreach (GameObject enemy in enemies)
            {
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position); // 적과의 거리 구하기


                if (distanceToEnemy <= RealRange) // 사거리 안에 있는 타겟들
                {
                    EnemyList.Add(enemy);
                }

                if (distanceToEnemy < shortestDistance)  // 우선순위 찾기 SortAttackPriority();
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
            // 타워 회전
            Vector3 dir = Target.transform.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = Quaternion.Lerp(PartToRotate.rotation, lookRotation, Time.deltaTime * TurnSpeed).eulerAngles;
            PartToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        }


        // 발사
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


    // 공격 우선순위 정하는 함수
    private void SortAttackPriority()
    {
        switch (AttackPriorityID)
        {
            case 0:
                // 먼저 들어온 몬스터
                if (EnemyList.Count != 0)
                {
                    Target = EnemyList[0];
                }

                break;

            case 1:
                // 체력 우선 공격 -> 방어구 없는 적 먼저 타겟팅하고 다 같으면 처음 들어온 몬스터

                break;

            case 2:
                // 방어구 가진 몬스터 우선 공격 Enemy.CurrentShield

                break;

            case 3:
                // 방어력 높은 적 우선 공격
                //EnemyList.Sort(gameObject.GetComponent<Enemy>().) <- 적 변수 public으로 바꿔주세요~

                break;
        }
    }


    public virtual void Shoot() // 수정
    {

        if (BulletPrefab != null)
        {
            GameObject bulletGO = ObjectPools.Instance.GetPooledObject(BulletPrefab.name);
            bulletGO.transform.position = FirePoint.position;

            bulletGO.GetComponent<Bullet>()?.Seek(Target, ProjectileSpeed, AttackStat, AttackSpecialization);
        }

    }

    //버프타워 없어지면 List에서 어떻게 빼지?

    //공격력 버프
    public void GetAttackBuff(float _BuffAmount)
    {
        AttackBuffAmount += _BuffAmount;
    }


    // 공격속도 버프
    public void GetSpeedBuff(float _BuffAmount)
    {
        SpeedBuffAmount += _BuffAmount;
    }


    // 상호작용 함수

    //Upgrade
    void UpgradeTower() // 데이터 연동해서 수정하기
    {
        //Attackstat + 5 로 해놓기

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

        //돈 40 잃기

        GameManager.instance.money -= 40;

        UpgradeCount++;

    }

    //Sell
    void SellTower()
    {
        // 타워 반납하기
        Destroy(gameObject); // 타워 풀에 추가한 뒤 바꾸기 ?  <- 맵에서 다룬다!


        // 돈 받기 (타워 설치비용 + 업그레이드 비용 ) * 0.6
        float _SellPrice = (Price + UpgradeCount * UpgradePrice) * 0.6f;

        SellPrice = (int)_SellPrice;


        // 재화 연결 함수

        GameManager.instance.money += SellPrice;

        // 공격, 공속 버프 타워일 때  버프 삭제 효과 넣어주기

    }

    //Move
    void MoveTower()
    {
        // 설치 상태로 돌아감

        // 중간에 취소 가능하게

        // 돈 감소
        float _MovePrice = (Price + UpgradeCount * UpgradePrice) * 0.5f;


        MovePrice = (int)_MovePrice;


        // 재화 연결 함수

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
