using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChangeText : MonoBehaviour
{
    private void Awake()
    {
        Tables.Load();
        TextMeshProUGUI[] texts = GetComponentsInChildren<TextMeshProUGUI>();
        foreach (var txt in texts)
        {
            //txt.text = Tables.StringUI.Get(txt.gameObject.name)?._Korean;
        }
    }
}
