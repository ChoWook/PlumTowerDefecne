using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public float speed;
    public GameObject target;

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
        } 
        else
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
            transform.LookAt(target.transform);
        }
    }
}
