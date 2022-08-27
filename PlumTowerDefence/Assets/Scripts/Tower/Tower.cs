using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    /*
     할 일
    1. 타워 자체 특성
     - 속성
     - 
     
    2. 타깃 조정
     
     */

    // 변수 : 속성, 공격력, 사거리, 크기, 타워 가격, 업그레이드 단계, 공격 종류(기본, 범위, 디버프 등), 이동 가격


    // 타워 스텟 (임시로 화살타워 스텟 설정)

    [SerializeField]
    private int TowerID = 0;                            // 타워 ID (데이터 테이블)


    public string TowerName = "화살타워";                        // 타워 이름
    private int AttackPropertyID = 0;                   // 공격 속성(데이터테이블)
    private int TypeID = 0;                             // 속성 ID (데이터테이블)
    private int SizeID = 0;                             // 타워 크기 (데이터테이블)
    private int AttackStat = 25;                       // 공격력 스텟(데이터테이블)
    private int SpeedStat = 4;                         // 공격 속도 스텟(데이터테이블)
    private int AbilityStat;                        // 특수 능력 스텟(데이터테이블)
    public int Range = 3;                       // 공격 사거리
    private int UpgradePrice = 40;                       // 업그레이드 가격(데이터테이블)
    private int Price = 100;                              // 구매 가격(데이터테이블)
    private double Damage;                             // 데미지 
    private int SellPrice;                             // 판매 가격


    // 적 스텟 (타겟 지정)

    public Transform Target;

    public string enemyTag = "Enemy";

    public int DefenceStat = 10;

    // todo 적 변수 가져 오기



    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0, 0.5f); // 0.5초 마다 반복하기
    }

    // 타겟 업데이트 (체력 우선, 방어구 우선, 방어력 높은 적 우선 추가하기)
    void UpdateTarget()
    {
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
            Target = nearestEnemy.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Target == null)
        {
            return;
        }
        else
        {
            Target = null;
        }
    }

    // 사거리 표시
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Range);
    }


    // 타워 공격 속성별 데미지 설정 (적 기획과 조율 필요)
    private void SetDamageWithAttackProperty()
    {
        // DefenceStat 방어력 어떻게 받을지 상의 필요함.

        switch(AttackPropertyID)
        {
            case 0:                                // 타워의 공격력 x {0.01x (100 - 적의 방어력)} 만큼 대상에게 데미지를 준다.
                Damage = AttackStat;
                break;
            case 1:                               // 체력에 타워의 공격력 x 1.2 x{0.01x(100-방어력)} 만큼 대상에게 데미지를 준다, 방어구에는 기본 알고리즘과 같다

                break;
            case 2 :

                break;

            case 3 :
                break;
                
        }    
    }



}
