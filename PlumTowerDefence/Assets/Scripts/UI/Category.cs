using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Category : MonoBehaviour
{
    /// <summary>
    /// 오브젝트 풀을 이용한 카테고리 생성 스크립트
    /// </summary>

    [SerializeField] private TextMeshProUGUI towerText;
    [SerializeField] private TextMeshProUGUI resourecText;
    [SerializeField] private TextMeshProUGUI passiveText;
    
    private int number_Of_TowerCategory;
    private int number_Of_ResourceCategory;
    private int number_Of_PassiveCategory;
    private void Awake()
    {
        towerText.text = Tables.UpgradeButton.Get(1)._Korean;
        resourecText.text = Tables.UpgradeButton.Get(2)._Korean;
        passiveText.text = Tables.UpgradeButton.Get(3)._Korean;
    }

    private void Start()
    {
        number_Of_TowerCategory = Tables.UpgradeButton.Get(1)._CategoryNum;
        number_Of_ResourceCategory = Tables.UpgradeButton.Get(2)._CategoryNum;
        number_Of_PassiveCategory = Tables.UpgradeButton.Get(3)._CategoryNum;
    }

    public void SelectTower(bool isOn)
    {
        if (isOn)
        {
            DeleteAllChild();
            GenerateCategory(number_Of_TowerCategory, ECategoryType.Tower);
        }
    }

    public void SelectResource(bool isOn)
    {
        if (isOn)
        {
            DeleteAllChild();
            GenerateCategory(number_Of_ResourceCategory, ECategoryType.Resource);
        }
    }

    public void SelectPassive(bool isOn)
    {
        if (isOn)
        {
            DeleteAllChild();
            GenerateCategory(number_Of_PassiveCategory, ECategoryType.Passive);
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
    
    private void GenerateCategory(int num, ECategoryType type)
    {
        for (int i = 1; i <= num; i++)
        {
            GameObject obj = ObjectPools.Instance.GetPooledObject("UpgradeCategoryButton");

            obj.transform.localScale = new Vector3(1f, 1f, 1f);
            obj.transform.SetParent(transform);
            obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 400 - (i - 1) * 67);

            switch (type)
            {
                case ECategoryType.Tower:
                    obj.GetComponent<CategoryButton>().SetId(i * 100 + 10000);
                    break;
                case ECategoryType.Resource:
                    obj.GetComponent<CategoryButton>().SetId(i * 100 + 20000);
                    break;
                case ECategoryType.Passive:
                    obj.GetComponent<CategoryButton>().SetId(i * 100 + 30000);
                    break;
            }
            
        }
    }
}
