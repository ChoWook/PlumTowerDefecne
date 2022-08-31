using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public static List<List<Transform>> points;               // �迭
    public int Route = 0;
    public List<Transform> testTransform;

    private void Awake()
    {
        points = new List<List<Transform>>();

        points.Add(new List<Transform>());

        if(testTransform != null && testTransform.Count > 0)
        {
            points[0] = testTransform;
        }

    }
}
