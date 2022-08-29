using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public static List<List<Transform>> points;               // 배열
    int Route = 0;
    int RouteCount = 1; // 루트 개수를 받아와야됨 (길이 나뉘어지면 루트가 추가됨)   

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
