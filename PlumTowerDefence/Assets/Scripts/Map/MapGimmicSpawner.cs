using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

[System.Serializable]
public class MapGimmicSpawner : MonoBehaviour
{
    [SerializeField] GameObject Obstacle;

    [SerializeField] GameObject Resource;

    [SerializeField] GameObject Chest;

    public void SpawnGimmick(EGimmickType GimmickType, int idx = -1)
    {
        // dix == -1 이면 맵 전체 소환
        if (idx == -1)
        {
            SpawnGimmickHoleMap(GimmickType);
        }
        else
        {
            SpawnGimmickOnlyNextGround(GimmickType, idx);
        }
    }

    void SpawnGimmickOnlyNextGround(EGimmickType GimmickType, int idx)
    {

    }

    void SpawnGimmickHoleMap(EGimmickType GimmickType)
    {

    }

}
