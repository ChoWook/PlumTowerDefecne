using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float MoveSpeed;

    private Transform Target;
    public int WaypointIndex;

    public int Route = 0;

    public void InitSpeed(EMonsterType monsterType)
    {
        if(Waypoints.points == null)
        {
            return;
        }

        MoveSpeed = GetComponent<Enemy>().Speed;
        Target = Waypoints.points[Route][WaypointIndex];
    }
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
            return;
        }

        WaypointIndex--;                 // ÁöÁ¡ ÀÎµ¦½º -1

        Target = Waypoints.points[Route][WaypointIndex];   // Å¸±êÀ» º¯°æ
    }

}
