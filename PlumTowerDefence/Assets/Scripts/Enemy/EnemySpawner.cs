using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform SpawnPoint;

    public int WaveNumber = 1;                  // 게임 매니저한테 받을수도
    public int Route = 0;

    int EnemyNumber = 6;
    int SpawnEnemyNumber;

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
        SpawnEnemyNumber = GetSpawnNumber(WaveNumber);
        Debug.Log("Wave Start");
        Debug.Log("Wave number: " + WaveNumber);
        Debug.Log(SpawnEnemyNumber);
        for (int i = 0; i < SpawnEnemyNumber; i++)     // WaveManager 을 만들자 (추후 수정 예정) 
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

    int GetSpawnNumber(int waveNumber)
    {
        if(waveNumber == 1)
        {
            EnemyNumber = 6;
        }
        else if(waveNumber <= 10)
        {
            EnemyNumber += 4;
        }
        else if(waveNumber <= 40)
        {
            EnemyNumber = Mathf.RoundToInt((float)EnemyNumber * 120 / 100);
        }
        else if(waveNumber <= 50)
        {
            EnemyNumber = Mathf.RoundToInt((float)EnemyNumber * 140 / 100);
        }

        return EnemyNumber;
    }
    

}
