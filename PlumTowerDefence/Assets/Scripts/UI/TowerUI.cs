using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerUI : MonoBehaviour, IPointerClickHandler
{
    private float clickTime;
    private float checkDoubleClickTime = 0.2f;
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        if(Time.realtimeSinceStartup-clickTime<checkDoubleClickTime)
            ShowGroundTowerUI();
        else
            ShowTowerUI();
    }

    private void ShowTowerUI()
    {
        Debug.Log("ShowTowerUI");
        
        UIManager.instance.ShowTowerUI(GetComponent<Tower>());

        clickTime = Time.realtimeSinceStartup;
    }

    private void ShowGroundTowerUI()
    {
        Debug.Log("ShowGroundTowerUI");
        
        UIManager.instance.ShowGroundTowerUI();
    }
}
