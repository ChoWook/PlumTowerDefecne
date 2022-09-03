using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObstacleUI : MonoBehaviour
{
    [SerializeField] Button Cancel;

    [SerializeField] Button Accept;

    [SerializeField] TextMeshProUGUI RequireMoney;

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
        _Obstacle.DeleteObstacle();
    }

    public void UpdateInfo()
    {
        RequireMoney.text = _Obstacle.DeletePrice.ToString();
    }
}
