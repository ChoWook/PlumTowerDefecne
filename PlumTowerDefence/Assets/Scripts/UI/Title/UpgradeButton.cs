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
        xpText.text = String.Format(Tables.StringUI.Get(xp.name)._Korean, GameManager.instance.remainxp);
    }

    public void MoveMainMenu()
    {
        _panel.transform.DOLocalMoveX(0, 1).SetEase(Ease);
        JsonManager.instance.WriteJson();
        DeleteCard();
    }

    public void ResetXP()
    {
        JsonManager.instance.ClearUpgrade();
        GameManager.instance.remainxp = GameManager.instance.totalxp;
        JsonManager.instance.SaveData.remainXP = GameManager.instance.remainxp;
        JsonManager.instance.SaveData.totalXP = GameManager.instance.totalxp;
        DeleteCard();
    }

    public void DeleteCard()
    {
        Transform panel = transform.GetChild(0);
        for (int i = 0; i < 5; i++)
        {
            int cnt = panel.GetChild(i).childCount;
            for (int j = 0; j < cnt; j++)
            {
                ObjectPools.Instance.ReleaseObjectToPool(panel.GetChild(i).GetChild(0).gameObject);
            }
        }
    }
}
