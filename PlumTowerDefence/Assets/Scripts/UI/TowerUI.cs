using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerUI : MonoBehaviour, IPointerClickHandler
{
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        ShowTowerUI();
    }

    private void ShowTowerUI()
    {
        Debug.Log("Clicked");
    }
}
