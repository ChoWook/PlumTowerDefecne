using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject TowerUI;
    [SerializeField] private GameObject GroundTowerUI;
    [SerializeField] private GameObject ObstacleUI;
    
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
    }

    public void ShowTowerUI(Tower tower)
    {
        UIClear();
        TowerUI.GetComponent<UpdateTowerUI>().SetTower(tower);
        TowerUI.SetActive(true);
    }

    public void ShowObstacleUI()
    {
        UIClear();
        ObstacleUI.SetActive(true);
    }

    public void ShowGroundTowerUI()
    {
        UIClear();
        GroundTowerUI.SetActive(true);
    }
}
