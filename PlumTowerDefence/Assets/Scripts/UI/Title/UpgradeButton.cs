using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class UpgradeButton : MonoBehaviour
{
    /// <summary>
    /// 강화 씬의 버튼을 관리하는 스크립트
    /// xp 값을 지속적으로 업데이트 하는 함수도 포함되어 있음
    /// </summary>
    [SerializeField] private GameObject _panel;
    [SerializeField] private GameObject xp;
    private TextMeshProUGUI xpText;

    public Ease Ease;

    private void Awake()
    {
        xpText = xp.GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        xpText.text = String.Format(Tables.StringUI.Get(xp.name)._Korean, GameManager.instance.totalxp);
    }

    public void MoveMainMenu()
    {
        _panel.transform.DOLocalMoveX(0, 1).SetEase(Ease);
        JsonManager.instance.WriteJson();
    }

    public void ResetXP()
    {
        Debug.Log("XP Reset");
    }
}
