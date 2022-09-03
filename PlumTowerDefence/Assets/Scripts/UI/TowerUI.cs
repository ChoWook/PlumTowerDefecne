using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerUI : MonoBehaviour, IPointerClickHandler
{
    private float clickTime;
    private float checkDoubleClickTime = 0.2f;
    private bool doubleClicked = false;
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        bool checkTime = Time.realtimeSinceStartup - clickTime < checkDoubleClickTime;
        if (doubleClicked && checkTime)
        {
            Debug.Log("3");
            doubleClicked = false;
        }
        else if (checkTime)
        {
            ShowGroundTowerUI();
        }
        else
        {
            ShowTowerUI();
        }
            
    }

    private void ShowTowerUI()
    {
        Debug.Log("ShowTowerUI");
        
        UIManager.instance.ShowTowerUI(GetComponent<Tower>());

        clickTime = Time.realtimeSinceStartup;

        doubleClicked = false;
    }

    private void ShowGroundTowerUI()
    {
        Debug.Log("ShowGroundTowerUI");
        
        UIManager.instance.ShowGroundTowerUI(GetComponent<Tower>());

        clickTime = Time.realtimeSinceStartup;

        doubleClicked = true;
    }
}
