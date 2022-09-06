using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    /// <summary>
    /// 메인메뉴에서 들어가는 강화의 강화 카드 프리팹에 들어갈 스크립트
    /// </summary>

    [HideInInspector] public int id;

    [SerializeField] private Sprite[] _sprites;
    
    private TextMeshProUGUI title;
    private TextMeshProUGUI context;
    private GameObject button;
    private TextMeshProUGUI requiredXp;

    private bool canBuy = true;
    
    private void OnEnable()
    {
        title = transform.GetChild(0).GetComponent<TextMeshProUGUI>();   //텍스트 초기화
        context = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        button = transform.GetChild(2).gameObject;
        requiredXp = button.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    public void SetID(int _id)
    {
        id = _id;
        title.text = Tables.UpgradeCard.Get(id)._Title;
        context.text = Tables.UpgradeCard.Get(id)._Contents;
        requiredXp.text = string.Format(Tables.StringUI.Get(requiredXp.name)._Korean,Tables.UpgradeCard.Get(id)._XpCost);
        name = id.ToString();
        
        ChangeSprite();
    }

    public void OnBuyButtonClick()
    {
        if (canBuy && GameManager.instance.xp >= Tables.UpgradeCard.Get(id)._XpCost)
        {
            GameManager.instance.xp -= Tables.UpgradeCard.Get(id)._XpCost;
            JsonManager.instance.BuyUpgrade(id);
            button.SetActive(false);
            ChangeChildSprite();
        }
    }

    public void ChangeSprite()
    {
        if (JsonManager.instance.upgradedCard.Contains(id))     //이미 삼
        {
            GetComponent<Image>().sprite = _sprites[0];
            button.SetActive(false);
            canBuy = false;
        }
        else if (JsonManager.instance.upgradedCard.Contains(Tables.UpgradeCard.Get(id)._Parent))
        {
            GetComponent<Image>().sprite = _sprites[0];         //살 수 있음
            button.SetActive(true);
            canBuy = true;
        }  
        else if (id / 10000 == 3 && Tables.UpgradeCard.Get(id)._Depth == 1)
        {
            GetComponent<Image>().sprite = _sprites[0];         //살 수 있음
            button.SetActive(true);
            canBuy = true;
        }
        else
        {
            GetComponent<Image>().sprite = _sprites[1];         //살 수 없음
            button.SetActive(true);
            canBuy = false;
        }
    }

    private void ChangeChildSprite()
    {
        if (Tables.UpgradeCard.Get(id)._Depth < transform.parent.parent.childCount)
        {
            var children = transform.parent.parent.GetChild(Tables.UpgradeCard.Get(id)._Depth).GetComponentsInChildren<Upgrade>();

            if (children != null)
            {
                foreach (var child in children)
                { 
                    child.ChangeSprite(); 
                    Debug.Log("child : " + child.name);
                }
            }
        }
    }
}
