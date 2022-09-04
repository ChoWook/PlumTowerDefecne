using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[ExecuteInEditMode]
public class EditModeText : MonoBehaviour
{
    /// <summary>
    /// 에딧 모드에서 텍스트를 변경해주는 스크립트
    /// </summary>
    
    #if UNITY_EDITOR
    void Awake()
    {
        Tables.Load();
        
        TextMeshProUGUI[] txt = FindObjectsOfType<TextMeshProUGUI>();

        foreach (TextMeshProUGUI text in txt)
        {
            //text.text = Tables.StringUI.Get(text.gameObject.name)?._Korean;
        }
    }
    #endif
}
