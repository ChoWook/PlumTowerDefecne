using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //public GameObject enemyPrefab;
    public Transform enemyPrefab;
    //private MemoryPool memoryPool;
    public Transform SpawnPoint;

    public int WaveNumber = 1;

    /*private void Awake()
    {
        memoryPool = new MemoryPool(enemyPrefab);
    }
*/
    // �� Ȯ���� ���� ��

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space))     // if(�� Ȯ�� ��ư�� ������)���� ��ü
        {
            StartCoroutine(SpawnWave());
        }
    }

    /*private void OnApplicationQuit()
    {
        memoryPool.DestroyObjects();
    }*/

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
        Instantiate(enemyPrefab, SpawnPoint.position, SpawnPoint.rotation);
        /*GameObject clone = memoryPool.ActivatePoolItem();
        clone.transform.position = SpawnPoint.position;
        clone.GetComponent<Enemy>().Setup(memoryPool);*/

    }

}
