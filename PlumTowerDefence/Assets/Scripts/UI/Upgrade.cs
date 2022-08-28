using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    private bool isRoot=true;
    private int parentNum;

    private void OnEnable()
    {
        if (isRoot)
        {
            Debug.Log("IsRoot!");
            transform.position = new Vector3(-5, 0, 0);
        }
        else
        {
            
        }
            
    }
}
