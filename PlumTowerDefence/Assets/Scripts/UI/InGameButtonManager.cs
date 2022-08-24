using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InGameButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject[] Texts;    //UI의 텍스트를 담을 배열, 이름 지정용 
    private TextMeshProUGUI levelText;
    private TextMeshProUGUI xpText;
    private TextMeshProUGUI hpText;
    private TextMeshProUGUI moneyText;
    
    private void Awake()
    {
        ChangeText();
        levelText = Texts[0].GetComponent<TextMeshProUGUI>();
        xpText = Texts[1].GetComponent<TextMeshProUGUI>();
        hpText = Texts[2].GetComponent<TextMeshProUGUI>();
        moneyText = Texts[3].GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        UpdateGameInfo();
    }

    private void ChangeText()
    {
        for (int i = 4; i < Texts.Length; i++)  //텍스트 변경, HP,Money,Level등 변경될 값들은 추후 변경예정
        {
            Texts[i].GetComponent<TextMeshProUGUI>().text = Tables.StringUI.Get(i+6)._Korean;
        }
    }

    private void UpdateGameInfo()
    {
        levelText.text = Tables.StringUI.Get(6)._Korean + GameManager.GetLevel();    //Level Update
        levelText.text = levelText.text.Replace("\r", "");
        xpText.text = Tables.StringUI.Get(7)._Korean + GameManager.GetXp();       //XP Update
        xpText.text = xpText.text.Replace("\r", "");
        hpText.text = Tables.StringUI.Get(8)._Korean + GameManager.GetCurrentHp()+"/"+GameManager.GetMaxHp(); //HP Update
        hpText.text = hpText.text.Replace("\r", "");
        moneyText.text = Tables.StringUI.Get(9)._Korean + GameManager.GetMoney();    //Money Update
        moneyText.text = moneyText.text.Replace("\r", "");
    }
}
