using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MiningUI : MonoBehaviour
{
    [SerializeField] Button Cancel;

    [SerializeField] Button Accept;

    [SerializeField] TextMeshProUGUI RequireMoney;

    Resource _Resource;

    public void SetResource(Resource Sender)
    {
        _Resource = Sender;

        UpdateInfo();
    }

    public void OnCancelBtnClick()
    {
        gameObject.SetActive(false);
    }

    public void OnAcceptBtnClicK()
    {
        //_Resource.DeleteObstacle();

        gameObject.SetActive(false);
    }

    public void UpdateInfo()
    {
        //RequireMoney.text = _Resource.DeletePrice.ToString();
    }
}
