using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class UpgradeButton : MonoBehaviour
{
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
        xpText.text = String.Format(Tables.StringUI.Get(xp.name)._Korean, GameManager.instance.xp);
    }

    public void MoveMainMenu()
    {
        _panel.transform.DOLocalMoveX(0, 1).SetEase(Ease);
    }

    public void ResetXP()
    {
        Debug.Log("XP Reset");
    }
}
