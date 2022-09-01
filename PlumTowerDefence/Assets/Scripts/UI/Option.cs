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
    private void Awake()
    {
        TextMeshProUGUI[] texts = transform.GetComponentsInChildren<TextMeshProUGUI>();

        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].text = Tables.StringUI.Get(texts[i].gameObject.name)._Korean;
        }
    }

    public void ShowOption()
    {
        gameObject.SetActive(true);
    }

    public void CloseOption()
    {
        gameObject.SetActive(false);
    }
}
