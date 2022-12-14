using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Pause : MonoBehaviour
{
    /// <summary>
    /// 일시정지 화면에 있는 버튼을 관리하는 스크립트
    /// 버튼에 들어갈 함수와 버튼의 텍스트를 변경하는 함수를 보우함
    /// </summary>
    
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private GameObject pauseBackGround;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameManager.instance.IsPausing)
            {
                resume();   //일시정지 해제
            }
            else
            {
                pause();    //일시정지
            }
        }
    }

    public void pause()
    {
        Time.timeScale = 0;
        GameManager.instance.IsPausing = true;
        pauseUI.SetActive(true);
        pauseBackGround.SetActive(true);
    }

    public void resume()
    {
        if (GameManager.instance.IsFast)
        {
            Time.timeScale = 5;
        }
        else
        {
            Time.timeScale = 1;
        }
        GameManager.instance.IsPausing = false;
        pauseUI.SetActive(false);
        pauseBackGround.SetActive(false);
    }

    public void MoveMainMenu()
    {
        resume();
        
        MoveScene.MoveTitleScene();
    }
}
