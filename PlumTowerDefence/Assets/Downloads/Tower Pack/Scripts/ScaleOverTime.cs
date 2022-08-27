using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleOverTime : MonoBehaviour
{
    public float scaleUpSpeed;
    public Vector3 scaleTo;

    void Update()
    {
        if(transform.localScale.x < scaleTo.x)
        {
            transform.localScale += new Vector3(scaleUpSpeed, scaleUpSpeed, scaleUpSpeed) * 10 * scaleUpSpeed * Time.deltaTime;
        }
        else
        {
            transform.localScale = scaleTo;
            Destroy(GetComponent<ScaleOverTime>());
        }
    }
}
