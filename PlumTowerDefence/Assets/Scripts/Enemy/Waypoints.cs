using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public static List<List<Transform>> points;               // �迭
    public int Route = 0;

    private void Awake()
    {
        points = new List<List<Transform>>();

        points.Add(new List<Transform>());

    }
}
