using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Option : MonoBehaviour
{
    /// <summary>
    /// option창을 담당하는 스크립트
    /// </summary>

    public void ShowOption()
    {
        gameObject.SetActive(true);
    }

    public void CloseOption()
    {
        gameObject.SetActive(false);
    }
}
