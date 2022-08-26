using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTest : MonoBehaviour
{
    public void SpawnEnemy()
    {
        var enemy = ObjectPools.Instance.GetPooledObject("BasicEnemy");

        enemy.GetComponent<Enemy>().enabled = false;
        enemy.GetComponent<EnemyMovement>().enabled = false;
    }
}
