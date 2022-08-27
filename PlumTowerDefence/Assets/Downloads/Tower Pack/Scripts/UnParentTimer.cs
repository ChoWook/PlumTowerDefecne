using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnParentTimer : MonoBehaviour
{
    public float timer;

    void Start()
    {
        GetComponent<MoveForward>().enabled = false;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            transform.parent = null;
            GetComponent<MoveForward>().enabled = true;
            Destroy(GetComponent<UnParentTimer>());
        }
    }
}
