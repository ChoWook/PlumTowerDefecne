using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameClear : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(nameof(IE_AddCallBack));
    }
    
    IEnumerator IE_AddCallBack()
    {
        yield return new WaitForEndOfFrame();
        GameManager.instance.AddGameClearCallBack(ShowGameClearUI);
    }

    public void ShowGameClearUI()
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
