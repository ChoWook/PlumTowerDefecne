using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    /// <summary>
    /// 메인메뉴에서 들어가는 강화의 강화 카드 프리팹에 들어갈 스크립트
    /// </summary>

    [HideInInspector] public int id;
    private TextMeshProUGUI text;

    private void OnEnable()
    {
        text = transform.GetChild(0).GetComponent<TextMeshProUGUI>();   //텍스트 초기화
    }

    private void Update()
    {
        ChangeText();
    }

    void ChangeText()
    {
        text.text = id.ToString();
    }
}
