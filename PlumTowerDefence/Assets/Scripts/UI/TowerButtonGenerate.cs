using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerButtonGenerate : MonoBehaviour
{
    /// <summary>
    /// 타워버튼 생성을 관리할 스크립트
    /// </summary>

    private int tower_num = 12; //데이터베이스에서 받아야 함

    private void Awake()
    {
        for (int i = 0; i < tower_num; i++)
        {
            GameObject obj = ObjectPools.Instance.GetPooledObject("Tower");
            obj.transform.SetParent(transform);
            obj.transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
