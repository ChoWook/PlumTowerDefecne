using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMarker : MonoBehaviour
{
    public new Camera camera;
    public GameObject test;
    public Vector2 mousePosition;

    void Update()
    {
        if(test != null)
        {
            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                mousePosition = new Vector2(hit.point.x, hit.point.z);
            }

            test.transform.position = new Vector3(mousePosition.x, test.transform.position.y, mousePosition.y);
        }
    }
}
