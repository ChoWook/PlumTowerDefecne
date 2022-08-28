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
    
    [SerializeField] private GameObject expandButton;
    [SerializeField] private GameObject startButton;

    private GameObject InGameUpgradeManager;
    
    private void Awake()
    {
        ChangeText();
        levelText = Texts[0].GetComponent<TextMeshProUGUI>();
        xpText = Texts[1].GetComponent<TextMeshProUGUI>();
        hpText = Texts[2].GetComponent<TextMeshProUGUI>();
        moneyText = Texts[3].GetComponent<TextMeshProUGUI>();
        
        InGameUpgradeManager = GameObject.Find("InGameUpgradeManager");
    }

    private void Update()
    {
        UpdateGameInfo();
    }

    private void ChangeText()
    {
        for (int i = 4; i < Texts.Length; i++)  //텍스트 변경, HP,Money,Level등 변경될 값들은 추후 변경예정
        {
            Texts[i].GetComponent<TextMeshProUGUI>().text = Tables.StringUI.Get(Texts[i].transform.name)._Korean;
        }
    }

    private void UpdateGameInfo()       //인게임 UI 업데이트
    {
        levelText.text = Tables.StringUI.Get(6)._Korean + GameManager.instance.level;    //Level Update
        ReplaceR(levelText);
        xpText.text = Tables.StringUI.Get(7)._Korean + GameManager.instance.xp;       //XP Update
        ReplaceR(xpText);
        hpText.text = Tables.StringUI.Get(8)._Korean + GameManager.instance.currentHp+"/"+GameManager.instance.maxHp; //HP Update
        ReplaceR(hpText);
        moneyText.text = Tables.StringUI.Get(9)._Korean + GameManager.instance.money;    //Money Update
        ReplaceR(moneyText);
    }

    private void ReplaceR(TMP_Text tmpText)     //문자 겹침문제 해결용
    {
        tmpText.text = tmpText.text.Replace("\r", "");
    }
    
    public void ExpandArea()    //확장하기 버튼을 누르면 호출
    {
        //영토 확장
        if (!GameManager.instance.isPlayingGame)    //게임중이 아니라면
        {
            expandButton.SetActive(false);
            startButton.SetActive(true);
        }
        else
        {
            Debug.Log("전투중입니다!");
        }
    }
    
    public void PlayGame()      //게임 시작시 호출
    {
        GameManager.instance.level++;
        GameManager.instance.isPlayingGame = true;
        startButton.SetActive(false);
        expandButton.SetActive(true);
        //게임시작

        StartCoroutine(IE_DebugPlayingGame());
    }

    IEnumerator IE_DebugPlayingGame()       //테스트용 코루틴
    {
        yield return new WaitForSeconds(5.0f);
        //게임종료 시점
        GameManager.instance.isPlayingGame = false;                 //Bool flase 만들어서 게임 끝을 알림
        GameManager.instance.xp += GameManager.instance.level;      //level만큼 xp를 얻음
        if (GameManager.instance.level % 3 == 0)                    //3 level 마다 증강체
        {
            expandButton.SetActive(false);
            InGameUpgradeManager.GetComponent<InGameUpgrade>().ShowInGameUpgrade();
        }
    }

    public void ShowExpandButton()          //다른 스크립트에서 버튼 접근을 위한 함수
    {
        expandButton.SetActive(true);
    }
}
