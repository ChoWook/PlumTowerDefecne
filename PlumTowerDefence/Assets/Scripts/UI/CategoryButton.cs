using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CategoryButton : MonoBehaviour
{
    /// <summary>
    /// 각 카테고리 안에 들어가는 스크립트
    /// 현재 버튼이 가지는 정보를 포함하고 있음
    /// </summary>
    
    [HideInInspector] public int id;
    private TextMeshProUGUI text;
    private int id_count;
    
    private void OnEnable()     //초기화
    {
        text = transform.GetChild(0).GetComponent<TextMeshProUGUI>();   //텍스트 초기화
    }

    public void SetId(int _id)
    {
        id = _id;
        text.text = Tables.UpgradeCategory.Get(id)._Text;
        id_count = Tables.UpgradeCategory.Get(id)._CardNum;
    }

    public void GenerateCategoryCard()  //해당 버튼 클릭
    {
        DeleteAll();
        
        for (int i = 1; i <= id_count; i++)
        {
            GameObject obj = ObjectPools.Instance.GetPooledObject("UpgradeSelect");
            obj.transform.SetParent(transform.parent.parent.parent.GetChild(0));

            obj.GetComponent<Upgrade>().SetID(id + i);
            obj.transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    private void DeleteAll()
    {
        Transform panel = transform.parent.parent.parent.GetChild(0);
        int cnt = panel.childCount;
        for (int i = 0; i < cnt; i++)
        {
            ObjectPools.Instance.ReleaseObjectToPool(panel.GetChild(0).gameObject);
        }
    }
}
