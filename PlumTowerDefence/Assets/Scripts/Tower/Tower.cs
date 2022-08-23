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


    // 타워 스텟

    private int TowerID;                            // 타워 ID (데이터 테이블)
    public string TowerName;                        // 타워 이름
    private int TypeID;                             // 속성 ID (데이터테이블)
    private int SizeID;                             // 타워 크기 (데이터테이블)
    private int AttackStat ;                        // 공격력 스텟(데이터테이블)
    private int SpeedStat ;                         // 공격 속도 스텟(데이터테이블)
    public float range = 15f;                            // 공격 사거리

    // 적 스텟 (타겟 지정)

    private Transform target;

    public string enemyTag = "Enemy";

    // todo 적 변수 가져 오기



    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0, 0.5f); // 0.5초 마다 반복하기
    }

    // 타겟 업데이트
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

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            return;
        }
        else
        {
            target = null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }


}
