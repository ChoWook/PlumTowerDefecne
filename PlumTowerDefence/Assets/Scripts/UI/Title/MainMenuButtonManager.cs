using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.Rendering;

public class MainMenuButtonManager : MonoBehaviour
{
    /// <summary>
    /// 메인화면의 4가지 버튼을 담고 있는 스크립트
    /// 각 버튼을 눌렀을때의 효과와 버튼의 텍스트를 변경하는 함수를 가지고 있음
    /// </summary>
    
    [SerializeField] private GameObject[] ButtonText;   //버튼의 텍스트를 담을 배열, 이름 지정용
    [SerializeField] private GameObject _panel;
    public Ease Ease;
    private void Start()
    {
        ChangeText();
    }

    public void OnClickGameStart()   //게임시작 버튼을 눌렀을 때 호출 할 함수
    {
        //JsonManager.instance.usingList = JsonManager.instance.SaveData.upgradedCard.ToList();
        
        //GameManager.instance.InitGame();
        
        //SelectApplicationUpgrade();
        
        MoveScene.MoveDefenceScene();
        Debug.Log("게임시작");
    }

    public void OnClickUpgrade()   //강화 버튼을 눌렀을 때 호출 할 함수
    {
        _panel.transform.DOLocalMoveX(-1920, 1).SetEase(Ease);
        Debug.Log("강화");
    }

    public void OnClickGameEnd()   //게임종료 버튼을 눌렀을 때 호출 할 함수
    {
        #if UNITY_EDITOR        //에디터에서만 실행되는 코드
        UnityEditor.EditorApplication.isPlaying = false;
        #else                   //빌드된 게임에서만 실행되는 코드
        Application.Quit();
        #endif
    }

    private void ChangeText()
    {
        for (int i = 0; i < ButtonText.Length; i++)     //버튼 텍스트 변경
        {
            ButtonText[i].GetComponent<TextMeshProUGUI>().text = Tables.StringUI.Get(i+2)._Korean;
        }
    }

    public static void SelectApplicationUpgrade()
    {
        Debug.Log(JsonManager.instance.usingList.Count);

        foreach (var card in JsonManager.instance.SaveData.upgradedCard)
        {
            if (card / 10000 == 3)      //패시브
            {
                //적용
                ApplicationUpgrade.instance.ApplicationPassiveUpgrade(card);
                Debug.Log("적용" + card);
                //제거
                JsonManager.instance.usingList.Remove(card);
            }

            if ((card % 100 == 1) && (card/10000 == 1))        //설명칸
            {
                JsonManager.instance.usingList.Remove(card);
            }

            int cardParent = Tables.UpgradeCard.Get(card)._Parent;

            if (!(cardParent / 10000 == 1 && cardParent % 100 == 1) && !(card / 10000 == 2 && cardParent == 1)) 
            {
                JsonManager.instance.usingList.Remove(card);
            }
        }

        Debug.Log(JsonManager.instance.usingList.Count);
    }
}
