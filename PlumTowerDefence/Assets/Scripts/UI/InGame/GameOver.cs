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

    private void Start()
    {
        StartCoroutine(nameof(IE_AddCallBack));
    }

    IEnumerator IE_AddCallBack()
    {
        yield return new WaitForEndOfFrame();
        GameManager.instance.AddGameOverCallBack(ShowGameOverUI);
    }

    public void ShowGameOverUI()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        Time.timeScale = 0;
        JsonManager.instance.WriteJson();
    }

    public void MoveMainMenu()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        
        Time.timeScale = 1;
        MoveScene.MoveTitleScene();
    }
}
