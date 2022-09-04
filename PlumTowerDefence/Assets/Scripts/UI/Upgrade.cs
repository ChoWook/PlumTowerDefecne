using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    /// <summary>
    /// 메인메뉴에서 들어가는 강화의 강화 카드 프리팹에 들어갈 스크립트
    /// </summary>

    [HideInInspector] public int id;
    private TextMeshProUGUI title;
    private TextMeshProUGUI context;

    private void OnEnable()
    {
        title = transform.GetChild(0).GetComponent<TextMeshProUGUI>();   //텍스트 초기화
        context = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    public void SetID(int _id)
    {
        id = _id;
        title.text = Tables.UpgradeCard.Get(id)._Title;
        context.text = Tables.UpgradeCard.Get(id)._Contents;
    }
}
