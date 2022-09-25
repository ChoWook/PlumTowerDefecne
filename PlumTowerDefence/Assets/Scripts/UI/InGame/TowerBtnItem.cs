using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TowerBtnItem : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI TowerNameText;

    [SerializeField] TextMeshProUGUI TowerPriceText;

    [SerializeField] GameObject TowerSprite;

    [SerializeField] GameObject RedDot;

    [SerializeField] Sprite[] TowerButtonImage;

    [HideInInspector]
    public ETowerName _Name;

    private void Start()
    {
        GameManager.instance.AddGetCouponCallBack(ChangeCouponImage);
    }

    public void SetTowerName(ETowerName Sender)
    {
        _Name = Sender;

        Tables.Tower _Tower = Tables.Tower.Get(Sender);

        if(_Tower == null)
        {
            return;
        }

        TowerNameText.text = _Tower._Korean;

        TowerPriceText.text = _Tower._Price.ToString();
    }

    public void SetTowerImage()
    {
        TowerSprite.GetComponent<Image>().sprite = TowerButtonImage[(int)_Name - 1];
        ChangeCouponImage();
    }

    private void ChangeCouponImage()
    {
        if ((int)_Name != 0)
        {
            if (GameManager.instance.HasCoupon(_Name))
            {
                RedDot.SetActive(true);
                RedDot.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text =
                    GameManager.instance.GetCoupon(_Name).ToString();
            }
            else
            {
                RedDot.SetActive(false);
            }
        }
    }
}
