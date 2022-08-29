using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private float MoveSpeed;

    //[SerializeField]
    //private GameObject enemyPrefab;           필요 없음


    private Transform Target;
    private int WaypointIndex = 0;

    int Route = 0;


    private void Awake()
    {
        MoveSpeed = GetComponent<Enemy>().Speed;

    }

    private void Start()
    {
        Target = Waypoints.points[Route][Waypoints.points[Route].Count - 1];         // 첫번째 지점으로 이동

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
        if(WaypointIndex < 0)
        {
            Destroy(gameObject);
        }

        WaypointIndex--;                 // 지점 인덱스 -1

        Target = Waypoints.points[Route][WaypointIndex];   // 타깃을 변경
    }

}
