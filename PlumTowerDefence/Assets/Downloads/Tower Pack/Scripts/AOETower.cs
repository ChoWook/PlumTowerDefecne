using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOETower : MonoBehaviour
{
    public float attackRange = 1;
    public float attackSpeed = 1;
    public int attackDamage = 1;
    public Animator controller;
    private float timer = 1;
    private float startTime;

    void Start()
    {
        startTime = timer;
    }

    void Update()
    {
        //Deals damage to all nearby targets whenever timer hits 0
        timer -= Time.deltaTime * attackSpeed;
        if (timer <= 0)
        {
            var overlappingObjects = Physics.OverlapSphere(transform.position, attackRange);
            foreach(var collider in overlappingObjects)
            {
                if(collider.CompareTag("enemy"))
                {
                    collider.gameObject.GetComponent<HealthScript>().DealDamage(attackDamage, transform.position);
                }
            }
            timer = startTime;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(
            Vector3.zero,
            Vector3.up
        );

        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.white;
    }
}
