using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform SpawnPoint;

    public int WaveNumber;                  // 게임 매니저한테 받을수도
    public int Route = 0;
    public int WaypointIndex = 5;
    int EnemyTypeNum;
    int EnemyNumber = 1;
    int SpawnEnemyNumber;
    float[] EnemyArr;
    static int remainder;
    bool enemySpecial;
    bool bossEnemy;
    bool subbossEnemy;


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


    // 맵 확장을 누른 뒤

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space))     // if(맵 확장 버튼이 눌리면)으로 대체
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

        WaveNumber = GameManager.instance.Level;                              // 커밋할때 바꿔주기
        Debug.Log("WaveNumber: " + WaveNumber);


        EnemyNumber = Tables.MonsterAmount.Get(WaveNumber)._TotalAmount;
        Debug.Log("Total Enemy Number: " + EnemyNumber);
        GameManager.instance.CurrentEnemyNumber = EnemyNumber;
        EnemyTypeNum = Tables.MonsterAmount.Get(WaveNumber)._Monster.Count;
        EnemyArr = new float[EnemyTypeNum];

        int EachTotalSpawn = 0;

        for (int i = 0; i < EnemyTypeNum; i++)
        {
           int EachEnemyAmount = Tables.MonsterAmount.Get(WaveNumber)._Monster[i]._Amount;
            GetSpawnNumber(EachEnemyAmount);

            EnemyArr[i] = SpawnEnemyNumber;
            EachTotalSpawn += SpawnEnemyNumber;
        }
        //Debug.Log("Total EnemyNum: " + EnemyNumber);
        int spawnEnemyNum = 0;
        //Debug.Log("EachTotalSpawn: " + EachTotalSpawn);
        int specialityAmount = Tables.MonsterSpecialityAmount.Get(WaveNumber)._Amount;
        GetSpawnNumber(specialityAmount);
        int specialEnemyNum = SpawnEnemyNumber;
        //Debug.Log("나눠진 특성 가진 몬스터 수: " + SpawnEnemyNumber);
        while (EachTotalSpawn > 0)
        {
            if (EachTotalSpawn <= specialEnemyNum)
            {
                enemySpecial = true;

            }
            if(EachTotalSpawn == 2 && ((WaveNumber % 3) == 0 || (WaveNumber % 5)==0))
            {
                subbossEnemy = true;
            }
            if(EachTotalSpawn == 1 && (WaveNumber % 5) == 0)
            {
                subbossEnemy = false;
                bossEnemy = true;
            }

       
            int randEnemy = Choose(EnemyArr);
            int id = Tables.MonsterAmount.Get(WaveNumber)._Monster[randEnemy]._ID;
            SpawnEnemy(Tables.Monster.Get(id)._Type);
            EnemyArr[randEnemy]--;
            EachTotalSpawn--;
            spawnEnemyNum++;
            //Debug.Log(Tables.Monster.Get(id)._Type + " Spawn Enemy: " + spawnEnemyNum);
            yield return ws;
        }
        ObjectPools.Instance.ReleaseObjectToPool(gameObject);                           // 커밋할때 바꿔주기

    }

    public void UpdateEnemySpawnCounts()
    {
        WaveNumber = GameManager.instance.Level;
        EnemyTypeNum = Tables.MonsterAmount.Get(WaveNumber)._Monster.Count;

        for (int i = 0; i < EnemyTypeNum; i++)
        {
            int id = Tables.MonsterAmount.Get(WaveNumber)._Monster[i]._ID;
            EnemySpawnCounts[Tables.Monster.Get(id)._Type] += 1;
        }
    }


    public void SpawnEnemy(EMonsterType monsterType)
    {
        var enemy = ObjectPools.Instance.GetPooledObject(monsterType.ToString());        // GameObject
        var emove = enemy.GetComponent<EnemyMovement>();
        emove.Route = Route;
        emove.WaypointIndex = WaypointIndex;
        enemy.transform.position = SpawnPoint.position;

        enemy.GetComponent<Enemy>().monsterType = monsterType;

        if(enemySpecial == true)
        {
            enemy.GetComponent<Enemy>().hasSpecial = true;
        }
        else
            enemy.GetComponent<Enemy>().hasSpecial = false;

        if(bossEnemy == true)
        {
            enemy.GetComponent<Enemy>().IsBoss = true;
        }
        else
        {
            enemy.GetComponent<Enemy>().IsBoss = false;
        }
        if(subbossEnemy == true)
        {
            enemy.GetComponent<Enemy>().IsSubBoss = true;
        }
        else
        {
            enemy.GetComponent <Enemy>().IsSubBoss = false;
        }


        enemy.GetComponent<Enemy>().InitStat();

        enemy.GetComponent<BaseAniContoller>().InitAnimation();


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


    void GetSpawnNumber(int eachEnemyAmount)
    {

        int RouteCount = Map.Instance.CurAttackRouteCnt;                  // 커밋할때 바꾸기
        //int RouteCount = 1;

        SpawnEnemyNumber = eachEnemyAmount / RouteCount;
        remainder = eachEnemyAmount % RouteCount;
        //Debug.Log("RouteCount: " + RouteCount);

        switch (remainder)
        {
            case 0: 
                break;
            case 1: 
                if(Route == 0)
                {
                    SpawnEnemyNumber++;
                }
                break;
            case 2:
                if(Route == 0)
                {
                    SpawnEnemyNumber++;
                }
                else if(Route == 1)
                {
                    SpawnEnemyNumber++;
                }
                break;
        }

    }
    

}
