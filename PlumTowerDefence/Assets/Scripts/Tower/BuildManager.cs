using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    private GameObject TowerToBuild;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("BuildManager는 씬당 하나만 가능");
            return;
        }
        instance = this;
    }

    public GameObject TowerPrefab; //설치할 타워 prefab

    void Start ()
    {
        TowerToBuild = TowerPrefab;
    }

    public GameObject GetTowerToBuild()
    {
        return TowerToBuild;
    }

}
