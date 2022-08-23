using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Mainmenu : MonoBehaviour
{
    [SerializeField] private GameObject[] ButtonText;   //버튼의 텍스트를 담을 배열, 이름 지정용

    private void Start()
    {
        for (int i = 0; i < ButtonText.Length; i++)     //버튼 텍스트 변경
        {
            ButtonText[i].GetComponent<TextMeshProUGUI>().text = Tables.StringUI.Get(i+2)._Korean;
        }
    }

    public void OnClickGameStart()   //게임시작 버튼을 눌렀을 때 호출 할 함수
    {
        Debug.Log("게임시작");
    }

    public void OnClickUpgrade()   //강화 버튼을 눌렀을 때 호출 할 함수
    {
        Debug.Log("강화");
    }

    public void OnClickOption()    //환경설정 버튼을 눌렀을 때 호출 할 함수
    {
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
    
    
}
