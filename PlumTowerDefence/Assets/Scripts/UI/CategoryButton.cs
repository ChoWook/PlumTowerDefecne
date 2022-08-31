using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CategoryButton : MonoBehaviour
{
    /// <summary>
    /// 각 카테고리 안에 들어가는 스크립트
    /// 현재 버튼이 가지는 정보를 포함하고 있음
    /// </summary>
    
    [HideInInspector] public int id;
    private TextMeshProUGUI text;
    private int id_count = 14;
    
    private void OnEnable()     //초기화
    {
        text = transform.GetChild(0).GetComponent<TextMeshProUGUI>();   //텍스트 초기화
    }

    private void Update()
    {
        ChangeText();
    }

    void ChangeText()       //순서 떄문에 update로 지속적인 초기화를 시켜줘야함
    {
        text.text = id.ToString();
    }

    public void GenerateCategoryCard()  //해당 버튼 클릭
    {
        DeleteAll();
        for (int i = 0; i < id_count; i++)
        {
            GameObject obj = ObjectPools.Instance.GetPooledObject("UpgradeSelect");
            obj.transform.SetParent(transform.parent.parent.parent.GetChild(0));

            obj.GetComponent<Upgrade>().id = id + i;
            obj.transform.localScale = new Vector3(1f, 1f, 1f);
            obj.transform.position = new Vector3(0, 0, 0);
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
