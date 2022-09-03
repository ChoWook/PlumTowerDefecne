using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    /// <summary>
    /// 게임오버 UI를 관리하는 스크립트
    /// </summary>

    private void Awake()
    {
        GameManager.instance.AddGameOverCallBack(ShowGameOverUI);
        gameObject.SetActive(false);
    }

    public void ShowGameOverUI()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void MoveMainMenu()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
        MoveScene.MoveTitleScene();
    }

    public void TESTGameOver()
    {
        GameManager.instance.currentHp -= 5;
    }
}
