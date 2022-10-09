using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveScene : MonoBehaviour
{
    /// <summary>
    /// 메인메뉴와 게임씬을 이동하는 스크립트
    /// </summary>
    
    public static void MoveDefenceScene()
    {
        SceneManager.LoadScene("LoadingScene");
    }

    public static void MoveTitleScene()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
