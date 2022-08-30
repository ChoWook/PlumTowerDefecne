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
     
          StartCoroutine(SpawnWave(WaveNumber));
  
        }
    }
    /*for (int i = 0; i<SpawnEnemyNumber; i++)     // WaveManager 을 만들자 (추후 수정 예정) 
        {                                       // WaveNumber 에 따라 EnemyNumber와 특성부여가 달라진다
            SpawnEnemy(EMonsterType.Bet);
    yield return new WaitForSeconds(0.5f);
}*/

IEnumerator SpawnWave(int waveNumber)
    {
        SpawnEnemyNumber = GetSpawnNumber(WaveNumber);
        Debug.Log("Wave Start");
        Debug.Log("Wave number: " + WaveNumber);
        Debug.Log(SpawnEnemyNumber);

        for (int i = 0; i < SpawnEnemyNumber; i++)     
        {                                       
            SpawnEnemy(EMonsterType.Bet);
            yield return new WaitForSeconds(0.5f);
        }

        WaveNumber++;

    }
   

    void SpawnEnemy(EMonsterType monsterType)
    {
        if (monsterType == EMonsterType.Bet)
        {
            var enemy = ObjectPools.Instance.GetPooledObject("Bat");        // GameObject
            enemy.GetComponent<EnemyMovement>().Route = Route;              // GetCompnent 이유: 다른 스크립트에 있는 원하는 변수가
                                                                            // 현재 게임오브젝트에 없기 때문에
            enemy.transform.position = SpawnPoint.position;
            enemy.GetComponent<EnemyMovement>().InitSpeed(monsterType);
        }
        else if (monsterType == EMonsterType.Mushroom)
        {
            var enemy = ObjectPools.Instance.GetPooledObject("Mushroom");        
            enemy.GetComponent<EnemyMovement>().Route = Route;              
                                                                            
            enemy.transform.position = SpawnPoint.position;
            enemy.GetComponent<EnemyMovement>().InitSpeed(monsterType);
        }
        else if (monsterType == EMonsterType.Flower)
        {
            var enemy = ObjectPools.Instance.GetPooledObject("Flower");        
            enemy.GetComponent<EnemyMovement>().Route = Route;              
                                                                           
            enemy.transform.position = SpawnPoint.position;
            enemy.GetComponent<EnemyMovement>().InitSpeed(monsterType);
        }
        else if (monsterType == EMonsterType.Fish)
        {
            var enemy = ObjectPools.Instance.GetPooledObject("Fish");        
            enemy.GetComponent<EnemyMovement>().Route = Route;              
                                                                            
            enemy.transform.position = SpawnPoint.position;
            enemy.GetComponent<EnemyMovement>().InitSpeed(monsterType);
        }
        else if (monsterType == EMonsterType.Slime)
        {
            var enemy = ObjectPools.Instance.GetPooledObject("Slime");        
            enemy.GetComponent<EnemyMovement>().Route = Route;              
                                                                            
            enemy.transform.position = SpawnPoint.position;
            enemy.GetComponent<EnemyMovement>().InitSpeed(monsterType);
        }
        else if (monsterType == EMonsterType.Pirate)
        {
            var enemy = ObjectPools.Instance.GetPooledObject("Pirate");        
            enemy.GetComponent<EnemyMovement>().Route = Route;              
                                                                            
            enemy.transform.position = SpawnPoint.position;
            enemy.GetComponent<EnemyMovement>().InitSpeed(monsterType);
        }
        else if (monsterType == EMonsterType.Spider)
        {
            var enemy = ObjectPools.Instance.GetPooledObject("Spider");
            enemy.GetComponent<EnemyMovement>().Route = Route;

            enemy.transform.position = SpawnPoint.position;
            enemy.GetComponent<EnemyMovement>().InitSpeed(monsterType);
        }
        else if (monsterType == EMonsterType.Bear)
        {
            var enemy = ObjectPools.Instance.GetPooledObject("Bear");
            enemy.GetComponent<EnemyMovement>().Route = Route;

            enemy.transform.position = SpawnPoint.position;
            enemy.GetComponent<EnemyMovement>().InitSpeed(monsterType);
        }
        else
        {
            var enemy = ObjectPools.Instance.GetPooledObject("BasicEnemy");
            enemy.GetComponent<EnemyMovement>().Route = Route;
            enemy.transform.position = SpawnPoint.position;
            enemy.GetComponent<EnemyMovement>().InitSpeed(monsterType);
        }
        
        
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
