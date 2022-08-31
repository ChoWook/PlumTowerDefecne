using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSize : MonoBehaviour
{
    public float Range = 3f;


    private void Start()
    {
        Transform parent = transform.parent;
        transform.parent = null;
        transform.localScale = new Vector3(Range, 0.05f, Range);
        transform.parent = parent;
    }



}
