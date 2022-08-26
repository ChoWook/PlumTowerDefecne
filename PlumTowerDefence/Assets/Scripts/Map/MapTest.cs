using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTest : MonoBehaviour
{
    public void SpawnEnemy()
    {
        ObjectPools.Instance.GetPooledObject("BasicEnemy");
    }
}
