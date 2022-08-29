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

        Debug.Log("Wave Start");
        for(int i = 0; i < WaveNumber; i++)     // WaveManager �� ������ (���� ���� ����) 
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

}
