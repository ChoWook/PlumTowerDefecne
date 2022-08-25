using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private GameObject pauseBackGround;
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
