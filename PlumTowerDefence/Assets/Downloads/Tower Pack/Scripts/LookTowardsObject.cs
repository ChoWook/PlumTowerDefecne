using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookTowardsObject : MonoBehaviour
{
    public Vector3 rotationOffset;
    public Transform ga;
    public float rotationSpeed;

    void Update()
    {
        if(ga != null)
        {
            var lookAt = Quaternion.LookRotation(transform.position - ga.position);
            lookAt.eulerAngles = new Vector3(rotationOffset.x, lookAt.eulerAngles.y, rotationOffset.z);
            transform.localRotation = lookAt;

            transform.rotation = Quaternion.Slerp(transform.rotation, lookAt, rotationSpeed * Time.deltaTime);
        }
    }
}
