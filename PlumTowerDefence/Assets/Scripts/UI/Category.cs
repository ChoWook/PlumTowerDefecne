using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Category : MonoBehaviour
{
    /// <summary>
    /// 오브젝트 풀을 이용한 카테고리 생성 스크립트
    /// </summary>
    
    private int number_Of_TowerCategory = 12;
    private int number_Of_ResourceCategory = 5;
    private int number_Of_PassiveCategory = 6;
    private void Awake()
    {
        //number_Of_TowerCategory = Tables.GlobalSystem.Get(~)
        //number_Of_ResourceCategory = Talbes.GlobalSystem.Get(~)
        //number_Of_PassiveCategory = Talbes.GlobalSystem.Get(~)
    }

    

    public void SelectTower(bool isOn)
    {
        if (isOn)
        {
            DeleteAllChild();
            GenerateCategory(number_Of_TowerCategory, CategoryType.Tower);
        }
    }

    public void SelectResource(bool isOn)
    {
        if (isOn)
        {
            DeleteAllChild();
            GenerateCategory(number_Of_ResourceCategory, CategoryType.Resource);
        }
    }

    public void SelectPassive(bool isOn)
    {
        if (isOn)
        {
            DeleteAllChild();
            GenerateCategory(number_Of_PassiveCategory, CategoryType.Passive);
        }
        
    }

    private void DeleteAllChild()
    {
        int cnt = transform.childCount;
        for (int i = 0; i < cnt; i++)
        {
            ObjectPools.Instance.ReleaseObjectToPool(transform.GetChild(0).gameObject);
        }
    }
    
    private void GenerateCategory(int num, CategoryType type)
    {
        for (int i = 0; i < num; i++)
        {
            GameObject obj = ObjectPools.Instance.GetPooledObject("UpgradeCategoryButton");

            obj.transform.localScale = new Vector3(1f, 1f, 1f);
            obj.transform.SetParent(transform);
        }
    }
}
