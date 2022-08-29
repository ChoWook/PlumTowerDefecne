using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public static List<List<Transform>> points;               // �迭
    int Route = 0;
    int RouteCount = 1; // ��Ʈ ������ �޾ƿ;ߵ� (���� ���������� ��Ʈ�� �߰���)   

    private void Awake()
    {
        points = new List<List<Transform>>();

        points.Add(new List<Transform>());                  // �ʱ�ȭ
                      
        for(int i = transform.childCount - 1 ; i >=0 ; i--) 
        {
            points[Route].Add(transform.GetChild(i));
        }

    }
}
