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

    [SerializeField] TextMeshProUGUI TowerGenerateKey;

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

        char key = '\0';

        switch (Sender)
        {
            case ETowerName.Arrow:
                key = 'A';
                break;
            case ETowerName.Hourglass:
                key = 'H';
                break;
            case ETowerName.Poison:
                key = 'P';
                break;
            case ETowerName.Flame:
                key = 'F';
                break;
            case ETowerName.AttackBuff:
                key = 'T';
                break;
            case ETowerName.Laser:
                key = 'L';
                break;
            case ETowerName.Electric:
                key = 'E';
                break;
            case ETowerName.Gatling:
                key = 'G';
                break;
            case ETowerName.Cannon:
                key = 'C';
                break;
            case ETowerName.Bomb:
                key = 'B';
                break;
            case ETowerName.SpeedBuff:
                key = 'S';
                break;
            case ETowerName.Missile:
                key = 'I';
                break;
        }

        TowerGenerateKey.text = $"(<color=red>{key}</color>)";
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
