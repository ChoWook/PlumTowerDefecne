using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform SpawnPoint;

    public int WaveNumber;                  // ���� �Ŵ������� ��������
    public int Route = 0;
    public int WaypointIndex = 5;
    int EnemyTypeNum;
    int EnemyNumber = 1;
    int SpawnEnemyNumber;
    float[] EnemyArr;


    public static Dictionary<EMonsterType, int> EnemySpawnCounts = new();

    static bool isLoad = false;

    private void Awake()
    {
        if(isStaticInit() == false)
        {
            for (EMonsterType monsterClass = EMonsterType.Bet; monsterClass <= EMonsterType.Bear; monsterClass++)
            {
                EnemySpawnCounts.Add(monsterClass, 0);
            }
        }
        
    }


    // �� Ȯ���� ���� ��

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space))     // if(�� Ȯ�� ��ư�� ������)���� ��ü
        {
            SpawnWave();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1)){
            WaveNumber++;
        }
    }
    
    private bool isStaticInit()
    {
        if(isLoad == false)
        {
            isLoad = true;
            return false;
        }
        else
            return true;
    }

    public void SpawnWave()
    {
        StartCoroutine(IE_SpawnWave());
    }


    IEnumerator IE_SpawnWave()
    {
        WaitForSeconds ws = new WaitForSeconds(0.5f);

        WaveNumber = GameManager.instance.level;    // Ŀ���Ҷ� �ٲ��ֱ�
        Debug.Log("WaveNumber: " + WaveNumber);


        EnemyNumber = Tables.MonsterAmount.Get(WaveNumber)._TotalAmount;
        GameManager.instance.currentEnemyNumber = EnemyNumber;
        EnemyTypeNum = Tables.MonsterAmount.Get(WaveNumber)._Monster.Count;
        EnemyArr = new float[EnemyTypeNum];



        for(int i = 0; i < EnemyTypeNum; i++)
        {
            EnemyArr[i] = Tables.MonsterAmount.Get(WaveNumber)._Monster[i]._Amount;

            int id = Tables.MonsterAmount.Get(WaveNumber)._Monster[i]._ID;
            EnemySpawnCounts[Tables.Monster.Get(id)._Type] += 1;
        }
        Debug.Log("Total EnemyNum: " + EnemyNumber);

        while (EnemyNumber > 0)
        {
            int randEnemy = Choose(EnemyArr);
            int id = Tables.MonsterAmount.Get(WaveNumber)._Monster[randEnemy]._ID;
            SpawnEnemy(Tables.Monster.Get(id)._Type);
            EnemyArr[randEnemy]--;
            EnemyNumber--;

            yield return ws;
        }
        ObjectPools.Instance.ReleaseObjectToPool(gameObject);

    }


    void SpawnEnemy(EMonsterType monsterType)
    {
        var enemy = ObjectPools.Instance.GetPooledObject(monsterType.ToString());        // GameObject
        var emove = enemy.GetComponent<EnemyMovement>();
        emove.Route = Route;
        emove.WaypointIndex = WaypointIndex;
        enemy.transform.position = SpawnPoint.position;

        enemy.GetComponent<Enemy>().monsterType = monsterType;

        enemy.GetComponent<Enemy>().InitStat();

        emove.InitSpeed(monsterType);

        
    }

   

    int Choose(float[] probs)
    {

        float total = 0;

        foreach (float elem in probs)
        {
            total += elem;
        }

        float randomPoint = Random.value * total;

        for (int i = 0; i < probs.Length; i++)
        {
            if (randomPoint < probs[i])
            {
                return i;
            }
            else
            {
                randomPoint -= probs[i];
            }
        }
        return probs.Length - 1;
    }


    int GetSpawnNumber()
    {



        return EnemyNumber;
    }
    

}
