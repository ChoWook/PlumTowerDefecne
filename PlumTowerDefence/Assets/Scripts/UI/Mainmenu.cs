using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mainmenu : MonoBehaviour
{
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
        #if UNITY_EDITOR    //에디터에서만 실행되는 코드
        UnityEditor.EditorApplication.isPlaying = false;
        #else //빌드된 게임에서만 실행되는 코드
        Application.Quit();
        #endif
    }
}
