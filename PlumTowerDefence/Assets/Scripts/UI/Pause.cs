using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Pause : MonoBehaviour
{
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
