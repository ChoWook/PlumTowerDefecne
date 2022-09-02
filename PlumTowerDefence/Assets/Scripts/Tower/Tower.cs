using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    // 타워 스텟 (임시로 화살타워 스텟 설정)

    [SerializeField]
    public int TowerID;                            // 타워 ID (데이터 테이블)


    [Header("Attributes")]

    public float Range;                               // 공격 사거리
    public float SpeedStat;                       // 공격 속도 스텟(데이터테이블)
    private float FireCountdown = 0f;                      // 발사 카운트다운

    

    public string TowerName = "화살타워";                  // 타워 이름
    protected EAttackSepcialization AttackSpecialization;                      // 공격 속성(데이터테이블)
    protected ETowerType TypeID;                             // 속성 ID (데이터테이블)
    protected int Size;                                // 타워 크기 (데이터테이블)
    protected float AttackStat;                           // 공격력 스텟(데이터테이블)
    protected int AbilityStat;  

    public Transform PartToRotate;                         //회전 오브젝트
    public float TurnSpeed = 10f;                          //회전 속도



    [Header("Interactions")]

    public bool Selected = false;                           //타워 선택 여부
    public bool Fixed = false;                              //타워 설치 여부

    // public int AttackPriorityID =0;                         //우선 공격 속성 ID

    protected EUpgradeStat UpgradeStat;                                 // 업그레이드 대상
    protected int UpgradePrice;                          // 업그레이드 가격(데이터테이블)
    private int UpgradeCount = 0;                           // 업그레이드 횟수
    protected float UpgradeAmount;                          // 업그레이드 강화량

    protected int Price;                                // 구매 가격(데이터테이블)
    private int SellPrice;                                  // 판매 가격



    public GameObject BulletPrefab;
    public Transform FirePoint;
    public GameObject Boundary;                             //사거리 Cylinder
    protected float ProjectileSpeed;                      //투사체 속도

    public GameObject ObjectPool;



    // 적 스텟 (타겟 지정)

    [Header("Enemy")]

    public List<GameObject> EnemyLIst = new List<GameObject>();

    public GameObject Target;

    public string enemyTag = "Enemy";

    

    // Start is called before the first frame update
    void Start()
    {

        //사거리 지정 Range 값 넣기
        Transform parent = transform.parent;
        transform.parent = null;
        Boundary.transform.localScale = new Vector3(Range, 0.05f, Range);
        transform.parent = parent;

        InvokeRepeating("UpdateTarget", 0, 0.5f); // 0.5초 마다 반복하기

    }

    // 타겟 업데이트 ( 체력 우선, 방어구 우선, 방어력 높은 적 우선 추가하기)
    void UpdateTarget()
    {
        /*
        
        SortAttackPriority();

        */


        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag); // Enemy  태그로 적 찾기
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position); // 적과의 거리 구하기
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
        // 타워 회전
        
        Vector3 dir = Target.transform.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(PartToRotate.rotation,lookRotation,Time.deltaTime * TurnSpeed).eulerAngles;
        PartToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        // 발사

        if (FireCountdown <= 0f)
        {
            Shoot();
            FireCountdown = 1f / SpeedStat;
        }

        FireCountdown -= Time.deltaTime;
    }

    // 추적 알고리즘 코루틴
    IEnumerator IE_GetTargets()
    {
        //사거리 안에 들어온 적들 EnemyList에 정리 + 사거리에서 나가면 지우기.
    

        yield return new WaitForSeconds(0.5f);
    }

    /*
    // 공격 우선순위 정하는 함수
    private void SortAttackPriority()
    {
        switch(AttackPriorityID)
        {
            case 0:
                // 먼저 들어온 몬스터
                // 이렇게 하면 되나??????
                Target = EnemyLIst[0];

                if (Target == null)
                {
                    EnemyLIst.RemoveAt(0);
                   }
                break;

            case 1:
                // 체력 우선 공격 -> 방어구 없는 적 먼저 다 같으면 처음 들어온 몬스터
                
                break;

            case 2:
                // 방어구 가진 몬스터 우선 공격
                
                break;

            case 3:
                // 방어력 높은 적 우선 공격

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

    // 상호작용 함수

    //Upgrade
    void UpgradeTower()
    {
        //Attackstat + 5 로 해놓기
        AttackStat += UpgradeAmount;


        //돈 40 잃기

        GameManager.instance.money -= 40;

        UpgradeCount++;

    }

    //Sell
    void SellTower()
    {
        // 타워 반납하기
        Destroy(gameObject); // 타워 풀에 추가한 뒤 바꾸기


        // 돈 받기 (타워 설치비용 + 업그레이드 비용 ) * 0.6
        double _SellPrice = (Price + UpgradeCount * UpgradePrice) * 0.6;

        SellPrice = (int)_SellPrice;


        // 재화 연결 함수

        GameManager.instance.money += SellPrice;

    }

    //Move
    void MoveTower()
    {
        // 설치 상태로 돌아감

        // 중간에 취소 가능하게

        // 돈 감소
        double _MovePrice = (Price + UpgradeCount * UpgradePrice) * 0.5;


        int MovePrice = (int)_MovePrice;


        // 재화 연결 함수

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
