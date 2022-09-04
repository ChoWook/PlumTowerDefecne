using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TowerBtnItem : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI TowerNameText;

    [SerializeField] TextMeshProUGUI TowerPriceText;

    ETowerName _Name;

    public void SetTowerName(ETowerName Sender)
    {
        _Name = Sender;

        Tables.Tower _Tower = Tables.Tower.Get(Sender);

        if(_Tower == null)
        {
            return;
        }

        TowerNameText.text = _Tower._Korean;

        TowerPriceText.text = _Tower._Price.ToString();
    }

}
