using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject TowerUI;
    public GameObject GroundTowerUI;
    public GameObject AllTowerUI;
    public GameObject ObstacleUI;
    
    public static UIManager instance = null;

    private void Awake()
    {
        instance = this;
    }

    public void UIClear()
    {
        TowerUI.SetActive(false);
        GroundTowerUI.SetActive(false);
        ObstacleUI.SetActive(false);
        
        GameManager.instance.isClickedTower = false;
        GameManager.instance.isSettingTarget = 0;
        
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    public void ShowTowerUI(Tower tower)
    {
        UIClear();
        GameManager.instance.isClickedTower = true;
        TowerUI.GetComponent<UpdateTowerUI>().SetTower(tower);
        TowerUI.SetActive(true);
    }

    public void ShowObstacleUI(Obstacle Sender)
    {
        UIClear();
        ObstacleUI.GetComponent<ObstacleUI>().SetObstacle(Sender);
        ObstacleUI.SetActive(true);
    }

    public void ShowGroundTowerUI(Tower tower)
    {
        UIClear();
        GroundTowerUI.GetComponent<UpdateGroundTowerUI>().SetTower(tower);
        GroundTowerUI.SetActive(true);
    }

    public void ShowAllTowerUI(Tower tower)
    {
        UIClear();
        AllTowerUI.GetComponent<UpdateAllTowerUI>().SetTower(tower);
        AllTowerUI.SetActive(true);
    }
}
