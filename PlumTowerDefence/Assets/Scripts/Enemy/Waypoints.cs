using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public static List<List<Transform>> points;               // 배열
    public int Route = 0;

    private void Awake()
    {
        points = new List<List<Transform>>();

        points.Add(new List<Transform>());                  // 초기화
                      
        for(int i = transform.childCount - 1 ; i >=0 ; i--) 
        {
            points[Route].Add(transform.GetChild(i));
        }

    }
}
