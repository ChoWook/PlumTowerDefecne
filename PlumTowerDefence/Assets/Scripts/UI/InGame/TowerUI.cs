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
            ShowAllTowerUI();
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

        Tower t = GetComponent<Tower>();

        if( t == null)
        {
            t = GetComponentInParent<Tower>();
        }

        UIManager.instance.ShowTowerUI(t);

        clickTime = Time.realtimeSinceStartup;

        doubleClicked = false;
    }

    private void ShowGroundTowerUI()
    {
        Debug.Log("ShowGroundTowerUI");

        Tower t = GetComponent<Tower>();

        if (t == null)
        {
            t = GetComponentInParent<Tower>();
        }

        UIManager.instance.ShowGroundTowerUI(t);

        clickTime = Time.realtimeSinceStartup;

        doubleClicked = true;
    }

    private void ShowAllTowerUI()
    {
        Tower t = GetComponent<Tower>();

        if (t == null)
        {
            t = GetComponentInParent<Tower>();
        }

        UIManager.instance.ShowAllTowerUI(t);
    }
}
