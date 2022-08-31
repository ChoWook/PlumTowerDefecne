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

    [SerializeField] private GameObject[] texts;
    
    private void ChangeText()
    {
        for (int i = 0; i < texts.Length; i++)     //버튼 텍스트 변경
        {
            texts[i].GetComponent<TextMeshProUGUI>().text = Tables.StringUI.Get(texts[i].gameObject.name)._Korean;
        }
    }

    private void Awake()
    {
        gameObject.SetActive(false);
        ChangeText();
    }

    public void ShowGameOverUI()
    {
        gameObject.SetActive(true);
    }

    public void MoveMainMenu()
    {
        gameObject.SetActive(false);
        MoveScene.MoveTitleScene();
    }
}
