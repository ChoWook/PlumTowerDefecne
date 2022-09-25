using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObstacleUI : MonoBehaviour
{
    [SerializeField] Button CancelBtn;

    [SerializeField] Button AcceptBtn;

    [SerializeField] TextMeshProUGUI RequireMoney;

    [SerializeField] TextMeshProUGUI NameText;

    Obstacle _Obstacle;

    public void SetObstacle(Obstacle Sender)
    {
        _Obstacle = Sender;

        UpdateInfo();
    }

    public void OnCancelBtnClick()
    {
        gameObject.SetActive(false);
    }

    public void OnAcceptBtnClicK()
    {
        if (_Obstacle.DeletePrice > GameManager.instance.Money)
        {
            return;
        }

        GameManager.instance.Money -= _Obstacle.DeletePrice;

        _Obstacle.DeleteObstacle();

        gameObject.SetActive(false);
    }

    public void UpdateInfo()
    {
        // TODO String UI에 추가 필요
        NameText.text = "장애물";

        RequireMoney.text = string.Format(Tables.StringUI.Get("Obstacle_Remove_Cost")._Korean, _Obstacle.DeletePrice);
    }
}
