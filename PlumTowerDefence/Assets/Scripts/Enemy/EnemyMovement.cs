using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float MoveSpeed;

    private Transform Target;
    public int WaypointIndex;

    public int Route = 0;


    private void OnEnable()
    {
        //init();
    }

    public void InitSpeed(EMonsterType monsterType)
    {
        if(Waypoints.points == null)
        {
            return;
        }

        MoveSpeed = GetComponent<Enemy>().Speed;

        /*switch (monsterType)
        {
            case EMonsterType.Bet:
                MoveSpeed = GetComponent<Bat>().Speed;
                break;
            case EMonsterType.Mushroom:
                MoveSpeed = GetComponent<Mushroom>().Speed;
                break;
            case EMonsterType.Flower:
                MoveSpeed = GetComponent<Flower>().Speed;
                break;
            case EMonsterType.Fish:
                MoveSpeed = GetComponent<Fish>().Speed;
                break;
            case EMonsterType.Slime:
                MoveSpeed = GetComponent<Slime>().Speed;
                break;
            case EMonsterType.Pirate:
                MoveSpeed = GetComponent<Pirate>().Speed;
                break;
            case EMonsterType.Spider:
                MoveSpeed = GetComponent<Spider>().Speed;
                break;
            case EMonsterType.Bear:
                MoveSpeed = GetComponent<Bear>().Speed;
                break;
        }*/

        Target = Waypoints.points[Route][WaypointIndex];
    }

    private void Start()
    {
        
                 // 첫번째 지점으로 이동

    }

    // Update is called once per frame
    void Update()
    {

        Vector3 dir = Target.position - transform.position;
        transform.Translate(dir.normalized * MoveSpeed * Time.deltaTime, Space.World);
        Quaternion rotation = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, MoveSpeed * Time.deltaTime);
        
        if(Vector3.Distance(transform.position, Target.position) <= 0.4f)
        {
            GetNextWayPoint();
        }
    }

    void GetNextWayPoint()
    {
        if(WaypointIndex <= 0)
        {
            GameManager.instance.currentEnemyNumber--;
            ObjectPools.Instance.ReleaseObjectToPool(gameObject);
            Debug.Log("Enemy destroyed");
            return;
        }

        WaypointIndex--;                 // 지점 인덱스 -1

        Target = Waypoints.points[Route][WaypointIndex];   // 타깃을 변경
    }

}
