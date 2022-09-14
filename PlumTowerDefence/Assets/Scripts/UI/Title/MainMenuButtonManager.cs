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
        JsonManager.instance.usingList = JsonManager.instance.SaveData.upgradedCard.ToList();
        
        Debug.Log("upgradedCard : "+JsonManager.instance.SaveData.upgradedCard.Count);
        Debug.Log("usinglist : " + JsonManager.instance.usingList.Count);
        
        SelectApplicationUpgrade();
        
        Debug.Log("After usinglist : " + JsonManager.instance.usingList.Count);
        
        GameManager.instance.CallBackClear();
        
        MoveScene.MoveDefenceScene();
        Debug.Log("게임시작");
    }

    public void OnClickUpgrade()   //강화 버튼을 눌렀을 때 호출 할 함수
    {
        _panel.transform.DOLocalMoveX(-1920, 1).SetEase(Ease);
        Debug.Log("강화");
    }

    public void OnClickOption()    //환경설정 버튼을 눌렀을 때 호출 할 함수
    {
        GameObject obj = ObjectPools.Instance.GetPooledObject("Option");
        obj.transform.SetParent(transform.parent);
        obj.transform.localScale = new Vector3(1f, 1f, 1f);
        obj.transform.position = new Vector3(0, 0, 0);
        Debug.Log("설정");
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

    private void SelectApplicationUpgrade()
    {
        /*
        for (int i = 1; i <= Tables.UpgradeButton.Get(3)._CategoryNum; i++)
        {
            for (int j = 1; j <= Tables.UpgradeCategory.Get(30000 + i * 100)._CardNum; j++)
            {
                if (JsonManager.instance.usingList.Contains(30000 + i * 100 + j))       //패시브 증강체 이면
                {
                    //적용시키기
                    Debug.Log("적용" + (30000 + i * 100 + j));
                    //제거
                    JsonManager.instance.usingList.Remove(30000+i*100+j);
                }
            }
        }
        */
        foreach (var card in JsonManager.instance.SaveData.upgradedCard)
        {
            if (card / 10000 == 3)      //패시브
            {
                //적용
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
    }
}
