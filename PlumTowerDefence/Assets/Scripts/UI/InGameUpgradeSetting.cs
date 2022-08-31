using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InGameUpgradeSetting : MonoBehaviour
{
    /// <summary>
    /// 인게임 내에서 증강첵 보유하는 스크립트
    /// </summary>
    
    GameObject SelectButton;
    Toggle _toggle;
    void OnEnable()
    {
        TMP_Text titleText = gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        titleText.text = Tables.StringUI.Get(titleText.transform.name)._Korean;         //증강체 제목 설정

        TMP_Text contentsText = gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        contentsText.text = Tables.StringUI.Get(contentsText.transform.name)._Korean;   //증강체 내용 설정

        SelectButton = gameObject.transform.GetChild(2).gameObject;

        TMP_Text selectButtonText = SelectButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        selectButtonText.text = Tables.StringUI.Get(selectButtonText.transform.name)._Korean;   //선택버튼 텍스트 설정

        _toggle = gameObject.GetComponent<Toggle>();
        _toggle.isOn = false;
    }

    private void Update()
    {
        if(_toggle.isOn)
            SelectButton.SetActive(true);
        else
            SelectButton.SetActive(false);
    }
}
