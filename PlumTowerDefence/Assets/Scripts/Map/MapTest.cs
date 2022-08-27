using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MapTest : MonoBehaviour
{
    public void SpawnEnemy()
    {
        var enemy = ObjectPools.Instance.GetPooledObject("BasicEnemy");

        enemy.GetComponent<Enemy>().enabled = false;
        enemy.GetComponent<EnemyMovement>().enabled = false;
    }
   
    public void DoTweenTest()
    {
        RectTransform rectTransform = new();
        rectTransform.anchoredPosition = new Vector2(rectTransform.sizeDelta.x, 0);
    }
}
