using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InGameUpgrade : MonoBehaviour
{
    [SerializeField] private GameObject[] InGameUpgradeButtons;
    [SerializeField] private GameObject[] selectButtons;
    
    private GameObject ButtonManager;

    private const int InGameUpgradeCount = 3;

    private void Awake()
    {
        ButtonManager=GameObject.Find("ButtonManager");
        ChangeText();
    }
    
    private void ChangeText()
    {
        for (int i = 0; i < InGameUpgradeCount; i++)     //버튼 텍스트 변경
        {
            TextMeshProUGUI buttonText = selectButtons[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            buttonText.text = Tables.StringUI.Get(buttonText.gameObject.name)._Korean;
        }
    }

    public void SelectButton1()         //첫번째 증강체 클릭
    {
        selectButtons[0].SetActive(true);
        selectButtons[1].SetActive(false);
        selectButtons[2].SetActive(false);
    }
    public void SelectButton2()         //두번째 증강체 클릭
    {
        selectButtons[1].SetActive(true);
        selectButtons[0].SetActive(false);
        selectButtons[2].SetActive(false);
    }
    public void SelectButton3()         //세번째 증강체 클릭
    {
        selectButtons[2].SetActive(true);
        selectButtons[0].SetActive(false);
        selectButtons[1].SetActive(false);
    }

    public void ShowInGameUpgrade()     //증강체 3개를 화면에 띄움    (3 level 마다 호출)
    {
        //InGameUpgrade의 내용을 수정하는 부분
        for(int i=0;i<InGameUpgradeCount;i++)
            InGameUpgradeButtons[i].SetActive(true);
    }

    private void HideInGameUpgrade()    //증강체 및 선택버튼을 화면에서 숨김   (증강체를 선택한 이후 호출)
    {
        for (int i = 0; i < InGameUpgradeCount; i++)
        {
            InGameUpgradeButtons[i].SetActive(false);
            selectButtons[i].SetActive(false);
        }
    }

    public void SelectFirstUpgrade()    //첫번째 증강체를 획득
    {
        HideInGameUpgrade();
        //첫번째 증강체 적용
        ButtonManager.GetComponent<InGameButtonManager>().ShowExpandButton();
    }
    public void SelectSecondUpgrade()   //두번째 증강체를 획득
    {
        HideInGameUpgrade();
        //두번째 증강체 적용
        ButtonManager.GetComponent<InGameButtonManager>().ShowExpandButton();
    }
    public void SelectThirdUpgrade()    //세번째 증강체를 획득
    {
        HideInGameUpgrade();
        //세번째 증강체 적용
        ButtonManager.GetComponent<InGameButtonManager>().ShowExpandButton();
    }
}
