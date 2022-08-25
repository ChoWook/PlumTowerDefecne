using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private float MoveSpeed;

    private Transform Target;
    private int WavepointIndex = 0;



    private void Awake()
    {
        MoveSpeed = GetComponent<Enemy>().Speed;
    }

    private void Start()
    {
        Target = Waypoints.points[0];
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = Target.position;
        transform.position += Vector3.right * MoveSpeed * Time.deltaTime;

    }
}
