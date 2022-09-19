using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public UpdateTowerUI TowerUI;
    public UpdateGroundTowerUI GroundTowerUI;
    public UpdateAllTowerUI AllTowerUI;
    public GameObject ObstacleUI;
    public GameObject MiningUI;
    public GameObject LaneBuffUI;
    
    public static UIManager instance = null;

    private void Awake()
    {
        instance = this;
    }

    public void UIClear()
    {
        if(TowerUI.gameObject.activeSelf)
        {
            TowerUI.ClearTower();
            TowerUI.gameObject.SetActive(false);
        }

        if (GroundTowerUI.gameObject.activeSelf)
        {
            GroundTowerUI.ClearTowers();
            GroundTowerUI.gameObject.SetActive(false);
        }

        if (AllTowerUI.gameObject.activeSelf)
        {
            AllTowerUI.ClearTowers();
            AllTowerUI.gameObject.SetActive(false);
        }

        ObstacleUI.SetActive(false);
        MiningUI.SetActive(false);
        LaneBuffUI.SetActive(false);


        GameManager.instance.isClickedTower = false;
        GameManager.instance.isSettingTarget = 0;
        
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    public void ShowTowerUI(Tower tower)
    {
        UIClear();
        GameManager.instance.isClickedTower = true;
        TowerUI.SetTower(tower);
        TowerUI.gameObject.SetActive(true);
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
        GroundTowerUI.SetTower(tower);
        GroundTowerUI.gameObject.SetActive(true);
    }

    public void ShowAllTowerUI(Tower tower)
    {
        UIClear();
        AllTowerUI.SetTower(tower);
        AllTowerUI.gameObject.SetActive(true);
    }

    public void ShowMiningUI(Resource resource)
    {
        UIClear();
        MiningUI.GetComponent<MiningUI>().SetResource(resource);
        MiningUI.SetActive(true);
    }

    public void ShowLaneBuffUI(LaneBuff laneBuff)
    {
        UIClear();
        LaneBuffUI.GetComponent<LaneBuffUI>().SetLaneBuff(laneBuff);
        LaneBuffUI.SetActive(true);
    }
}
