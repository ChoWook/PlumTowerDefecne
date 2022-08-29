using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private float MoveSpeed;

    private Transform Target;
    private int WaypointIndex;

    int Route = 0;


    private void OnEnable()
    {
        init();
    }

    void init()
    {
        MoveSpeed = GetComponent<Enemy>().Speed;
        WaypointIndex = Waypoints.points[Route].Count;
        Target = Waypoints.points[Route][Waypoints.points[Route].Count - 1];
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
        
        if(Vector3.Distance(transform.position, Target.position) <= 0.4f)
        {
            GetNextWayPoint();
        }
    }

    void GetNextWayPoint()
    {
        if(WaypointIndex <= 0)
        {
            ObjectPools.Instance.ReleaseObjectToPool(gameObject);
            Debug.Log("Enemy destroyed");
            return;
        }

        WaypointIndex--;                 // 지점 인덱스 -1

        Target = Waypoints.points[Route][WaypointIndex];   // 타깃을 변경
    }

}
