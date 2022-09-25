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


        GameManager.instance.IsClickedTower = false;
        GameManager.instance.IsSettingTarget = 0;
        
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    public void ShowTowerUI(Tower tower)
    {
        UIClear();
        GameManager.instance.IsClickedTower = true;
        TowerUI.gameObject.SetActive(true);
        TowerUI.SetTower(tower);
    }

    public void ShowObstacleUI(Obstacle Sender)
    {
        UIClear();
        ObstacleUI.SetActive(true);
        ObstacleUI.GetComponent<ObstacleUI>().SetObstacle(Sender);
    }

    public void ShowGroundTowerUI(Tower tower)
    {
        UIClear();
        GroundTowerUI.gameObject.SetActive(true);
        GroundTowerUI.SetTower(tower);
    }

    public void ShowAllTowerUI(Tower tower)
    {
        UIClear();
        AllTowerUI.gameObject.SetActive(true);
        AllTowerUI.SetTower(tower);
    }

    public void ShowMiningUI(Resource resource)
    {
        UIClear();
        MiningUI.SetActive(true);
        MiningUI.GetComponent<MiningUI>().SetResource(resource);
    }

    public void ShowLaneBuffUI(LaneBuff laneBuff)
    {
        UIClear();
        LaneBuffUI.SetActive(true);
        LaneBuffUI.GetComponent<LaneBuffUI>().SetLaneBuff(laneBuff);
    }
}
