using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[ExecuteInEditMode]
public class EditModeText : MonoBehaviour
{
    #if UNITY_EDITOR
    void Awake()
    {
        Tables.Load();
        
        TextMeshProUGUI[] txt = FindObjectsOfType<TextMeshProUGUI>();

        foreach (TextMeshProUGUI text in txt)
        {
            text.text = Tables.StringUI.Get(text.gameObject.name)._Korean;
        }
    }
    #endif
}
