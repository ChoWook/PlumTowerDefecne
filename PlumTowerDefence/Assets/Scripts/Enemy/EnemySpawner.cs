using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    //private MemoryPool memoryPool;
    public Transform SpawnPoint;

    public int WaveNumber = 1;
    public int Route = 0;

    // 맵 확장을 누른 뒤

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space))     // if(맵 확장 버튼이 눌리면)으로 대체
        {
            StartCoroutine(SpawnWave());
        }
    }


    IEnumerator SpawnWave()
    {

        Debug.Log("Wave Start");
        for(int i = 0; i < WaveNumber; i++)     // WaveManager 을 만들자 (추후 수정 예정) 
        {                                       // WaveNumber 에 따라 EnemyNumber와 특성부여가 달라진다
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }
        WaveNumber++;

    }

    void SpawnEnemy()
    {
        var enemy = ObjectPools.Instance.GetPooledObject("BasicEnemy");
        enemy.GetComponent<EnemyMovement>().Route = Route;
        enemy.transform.position = SpawnPoint.position;
        //Instantiate(enemyPrefab, SpawnPoint.position, SpawnPoint.rotation);
    }

}
