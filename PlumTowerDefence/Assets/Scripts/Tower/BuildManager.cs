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
            Debug.LogError("BuildManager�� ���� �ϳ��� ����");
            return;
        }
        instance = this;
    }

    public GameObject TowerPrefab; //��ġ�� Ÿ�� prefab

    void Start ()
    {
        TowerToBuild = TowerPrefab;
    }

    public GameObject GetTowerToBuild()
    {
        return TowerToBuild;
    }

}
