using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InGameButtonManager : MonoBehaviour
{
    /// <summary>
    /// 인게임 내에서의 UI를 전체적으로 관리함
    /// UI에서의 체력, 돈, xp등 게임 진행상황중 변하는 수치또한 이 스크립트에서 관리하고 있으며
    /// 확장하기버튼, 게임시작버튼의 함수를 관리하고 있음
    /// 현재는 test목적의 게임 진행을 나타내는 코루틴 함수를 포함하고 있음
    /// </summary>
    [SerializeField]  private TextMeshProUGUI levelText;
    [SerializeField]  private TextMeshProUGUI xpText;
    [SerializeField]  private TextMeshProUGUI hpText;
    [SerializeField]  private TextMeshProUGUI moneyText;
    
    [SerializeField] private GameObject expandButton;
    [SerializeField] private GameObject startButton;

    private GameObject InGameUpgradePanel;
    
    private void Awake()
    {
        InGameUpgradePanel = GameObject.Find("InGameUpgradePanel");
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            OnFastButtonClick();
        }
    }

    public void SetValueChangeCallback()
    {
        GameManager.instance.AddStageClearCallBack(StageClear);
        GameManager.instance.AddLevelChangeCallBack(ChangeLevelText);
        GameManager.instance.AddXpChangeCallBack(ChangeXpText);
        GameManager.instance.AddHpChangeCallBack(ChangeHpText);
        GameManager.instance.AddMoneyChangeCallBack(ChangeMoneyText);
    }

    private void Start()
    {
        ChangeLevelText();
        ChangeXpText();
        ChangeHpText();
        ChangeMoneyText();
    }

    private void ChangeLevelText()
    {
        levelText.text = String.Format(Tables.StringUI.Get(levelText.transform.name)._Korean,GameManager.instance.Level);    //Level Update
        ReplaceR(levelText);
    }

    private void ChangeXpText()
    {
        xpText.text = String.Format(Tables.StringUI.Get(xpText.transform.name)._Korean,GameManager.instance.XP);       //XP Update
        ReplaceR(xpText);
    }

    private void ChangeHpText()
    {
        hpText.text = String.Format(Tables.StringUI.Get(hpText.transform.name)._Korean,GameManager.instance.CurrentHp,GameManager.instance.MaxHp); //HP Update
        ReplaceR(hpText);
    }
    private void ChangeMoneyText()
    {
        moneyText.text = String.Format(Tables.StringUI.Get(moneyText.transform.name)._Korean,GameManager.instance.Money);    //Money Update
        ReplaceR(moneyText);
    }

    private void ReplaceR(TMP_Text tmpText)     //문자 겹침문제 해결용
    {
        tmpText.text = tmpText.text.Replace("\r", "");
    }
    
    public void ExpandArea()    //확장하기 버튼을 누르면 호출
    {
        //영토 확장
        if (!GameManager.instance.IsPlayingGame)    //게임중이 아니라면
        {
            Map.Instance.ShowNextGrounds();
            GameManager.instance.Level++;
            expandButton.SetActive(false);
            startButton.SetActive(true);

        }
    }

    public void PlayGame() //게임 시작시 호출
    {
        GameManager.instance.IsPlayingGame = true;
        startButton.SetActive(false);
        //expandButton.SetActive(true);
        Map.Instance.StartEnemySpawn();
        //게임시작
    }

    public void StageClear()
    {
        if (GameManager.instance.Level % 3 == 0)                    //3 level 마다 증강체
        {
            //expandButton.SetActive(false);
            InGameUpgradePanel.GetComponent<InGameUpgrade>().ShowInGameUpgrade();
        }
        else
        {
            expandButton.SetActive(true);
        }
    }

    public void ShowExpandButton()          //다른 스크립트에서 버튼 접근을 위한 함수
    {
        expandButton.SetActive(true);
    }

    public void OnFastButtonClick()
    {
        if (!GameManager.instance.IsFast)
        {
            Time.timeScale = 5f;
            GameManager.instance.IsFast = true;
        }
        else
        {
            Time.timeScale = 1;
            GameManager.instance.IsFast = false;
        }
    }
}
