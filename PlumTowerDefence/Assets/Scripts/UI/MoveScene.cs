using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveScene : MonoBehaviour
{
    public static void MoveDefenceScene()
    {
        SceneManager.LoadScene("DefenceScene");
    }

    public static void MoveTitleScene()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
