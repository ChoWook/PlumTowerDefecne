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
        ChangeText();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameManager.instance.isPausing)
            {
                resume();   //일시정지 해제
            }
            else
            {
                pause();    //일시정지
            }
        }
    }

    private void pause()
    {
        Time.timeScale = 0;
        GameManager.instance.isPausing = true;
        pauseUI.SetActive(true);
        pauseBackGround.SetActive(true);
    }

    public void resume()
    {
        Time.timeScale = 1;
        GameManager.instance.isPausing = false;
        pauseUI.SetActive(false);
        pauseBackGround.SetActive(false);
    }

    public void MoveMainMenu()
    {
        resume();
        MoveScene.MoveTitleScene();
    }
}
