using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform SpawnPoint;

    public int WaveNumber = 1;                  // ���� �Ŵ������� ��������
    public int Route = 0;

    int EnemyNumber = 6;
    int SpawnEnemyNumber;

    // �� Ȯ���� ���� ��

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space))     // if(�� Ȯ�� ��ư�� ������)���� ��ü
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
        for (int i = 0; i < SpawnEnemyNumber; i++)     // WaveManager �� ������ (���� ���� ����) 
        {                                       // WaveNumber �� ���� EnemyNumber�� Ư���ο��� �޶�����
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
