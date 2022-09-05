using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // 기본 Enemy 스탯, 속성, 특성 Class만들기

    protected float BaseHP;               
    float MaxHP;
    public float CurrentHP;
    protected float BaseShield;           
    float MaxShield;

    public float CurrentShield;
    bool ShieldOn = true;
    public float Armor;
    protected float BaseArmor;            
    public float BaseSpeed;               
    public float Speed;                     
    public bool IsAlive = true;
    public bool hasSpecial;
    public int CurrentElement;
    public bool IsBoss;
    public bool IsSubBoss;
    private Vector3 scaleChange;



    private int[] currentLevel = new int[8];
    float[] ElementArr = new float[5];
        
    float Enforced = 1.0f;          // 강화특성

    public EMonsterType monsterType;
    public EPropertyType propertyType;

    Animator animator;  

    private void Awake()
    {
        for(int i = 0; i < 8; i++)
        {
            currentLevel[i] = 1;
        }
    }

    public void GetStat()
    {
        int id = Tables.Monster.Get((int)monsterType)._ID;
        BaseHP = Tables.Monster.Get(id)._Hp;
        BaseShield = Tables.Monster.Get(id)._Sheild;
        BaseArmor = Tables.Monster.Get(id)._Armor;
        BaseSpeed = Tables.Monster.Get(id)._Speed;
        MaxHP = BaseHP;
        MaxShield = BaseShield;
        Speed = BaseSpeed;
        Armor = BaseArmor;
    }
    
    public void SetStat()
    {
        CurrentHP = MaxHP;
        CurrentShield = MaxShield;
        ShieldOn = true;

        //Debug.Log("CurrentHP: " + CurrentHP + " CurrentShield: " + CurrentShield + " Armor: " + Armor + " Speed: " + Speed);
    }

    IEnumerator IE_PlayDeadAnimation()
    {
        GetComponent<BaseAniContoller>().DeadAnimation();
        if(propertyType == EPropertyType.Resurrect)
        {
            propertyType = EPropertyType.Resurrected;

        }
        else if(propertyType == EPropertyType.Divisive)
        {
            propertyType = EPropertyType.Divided;
        }
        else
        {
            yield return new WaitForSeconds(1);

            ObjectPools.Instance.ReleaseObjectToPool(gameObject);
        }
    }
    IEnumerator IE_Resurrection()
    {
        SetStat();
        transform.tag = "Untagged";
        GetComponent<EnemyMovement>().MoveSpeed *= 0.2f;
        StartCoroutine(IE_PlayDeadAnimation());
        if(propertyType == EPropertyType.Resurrected)
        {
            yield return new WaitForSeconds(1f);
        }
        else if(propertyType == EPropertyType.Divided)
        {
            yield return null;
        }
        GetComponent<EnemyMovement>().MoveSpeed *= 5f;

        GetComponent<BaseAniContoller>().InitAnimation();
        transform.tag = "Enemy";

    }
    void Resurrect()
    {
        StartCoroutine(IE_Resurrection());
    }

    public void TakeDamage(float damage, EAttackSpecialization type)
    {
        float hpSpecial = 1.0f;
        float shieldSpecial = 1.0f;
        float penetrationOn = 1.0f;
        switch (type)
        {
            case EAttackSpecialization.Health:
                hpSpecial = 1.2f;
                break;
            case EAttackSpecialization.Shield:
                shieldSpecial = 1.2f;
                break;
            case EAttackSpecialization.Defense:
                penetrationOn = 0;
                break;
        }
        if (ShieldOn == true)                                      // 실드가 있는 경우
        {
            float DamageProtect = CurrentShield;
            CurrentShield -= damage * 0.01f * (100 - Armor * penetrationOn) * 0.9f * shieldSpecial; // 현재 실드를 깐다

            if (CurrentShield <= 0)                               // 방어구가 다 까지면 
            {
                ShieldOn = false;                                 // 방어구 제거
                damage -= DamageProtect;                          // 데미지 경감
                CurrentHP -= damage * 0.01f * (100 - Armor * penetrationOn) * hpSpecial;
                CurrentShield = 0;
            }
        }
        else                                                      // 실드가 없는 경우
        {
            CurrentHP -= damage * 0.01f * (100 - Armor * penetrationOn) * hpSpecial;
        }
        //Debug.Log("Shield: " + CurrentShield + "HP: " + CurrentHP);

        if (CurrentHP <= 0)
        {
           if(propertyType == EPropertyType.Resurrect)
            {
                Resurrect(); 
            }
           else if(propertyType == EPropertyType.Divisive)
            {
                Resurrect();
                                
                MaxHP *= 0.3f;
                MaxShield *= 0.2f;
                SetStat();

                var enemy = ObjectPools.Instance.GetPooledObject(monsterType.ToString());
                var emove = enemy.GetComponent<EnemyMovement>();
                emove.Route = gameObject.GetComponent<EnemyMovement>().Route;
                emove.WaypointIndex = gameObject.GetComponent<EnemyMovement>().WaypointIndex;
                enemy.transform.position = gameObject.transform.position;
                var getComponent = enemy.GetComponent<Enemy>();
                getComponent.monsterType = monsterType;
                getComponent.hasSpecial = true;
                getComponent.InitStat();
                enemy.GetComponent<BaseAniContoller>().InitAnimation();
                emove.InitSpeed(monsterType);
                getComponent.propertyType = EPropertyType.Divided;
                getComponent.MaxHP = MaxHP;
                getComponent.MaxShield = MaxShield;
                getComponent.SetStat();

                GameManager.instance.currentEnemyNumber++;

                float currentSize = enemy.transform.localScale.x;
                scaleChange = new Vector3(currentSize, currentSize, currentSize);
                transform.localScale = scaleChange;

                GetComponent<EnemyMovement>().MoveSpeed *= 0.9f;

            }
           else
            {
                KillEnemy();
            }

            // else
                // 부활/ 분열 추가 
        }
    }

    void KillEnemy()
    {
        transform.tag = "Untagged";
        if(IsAlive == true)
        {
            IsAlive = false;
            GameManager.instance.currentEnemyNumber--;
        }
        GetComponent<EnemyMovement>().MoveSpeed = 0;
        //Debug.Log("Killed Enemy");
        StartCoroutine(IE_PlayDeadAnimation());                                         
    }

    public void AddSpeciality()
    {
        int id = Tables.Monster.Get((int)monsterType)._ID;
        int specialityType;
        if(Random.Range(0, 2) == 0)
        {
            specialityType = Tables.Monster.Get(id)._Speciality_1;
        }
        else
            specialityType= Tables.Monster.Get(id)._Speciality_2;

        var changeStat = Tables.MonsterSpeciality.Get(specialityType)._Amount/100;
        var monsterStat = Tables.MonsterSpeciality.Get(specialityType)._ChangeStat;

        switch (monsterStat)
        {
            case EMonsterStat.Hp:
                MaxHP +=  BaseHP * changeStat;
                break;
            case EMonsterStat.Armor:
                Armor += BaseArmor * changeStat;
                break;
            case EMonsterStat.Shield:
                MaxShield += BaseShield * changeStat;
                break;
            case EMonsterStat.Speed:
                Speed += BaseSpeed * changeStat;
                break;
        }
    }

    public void AddElementType()
    {
        int waveNum = GameManager.instance.level;                         // 커밋할때 바꾸기
        //int waveNum = GetComponent<EnemySpawner>().WaveNumber;              //
  

        ElementArr[0] = Tables.Monster.Get((int)monsterType)._None;
        ElementArr[1] = Tables.Monster.Get((int)monsterType)._Water;
        ElementArr[2] = Tables.Monster.Get((int)monsterType)._Ground;
        ElementArr[3] = Tables.Monster.Get((int)monsterType)._Fire;
        ElementArr[4] = Tables.Monster.Get((int)monsterType)._Electric;

        if (waveNum <= 5)
        {
            CurrentElement = 0;
            return;
        }
        else if(waveNum > 40)
        {
            switch (monsterType)
            {
                case EMonsterType.Bet:
                    CurrentElement = 0;
                    break;
                case EMonsterType.Mushroom:
                    CurrentElement = 0;
                    break;
                case EMonsterType.Flower:
                    CurrentElement = 0;
                    break;
                case EMonsterType.Fish:
                    CurrentElement = 1;
                    break;
                case EMonsterType.Slime:
                    CurrentElement = Random.Range(0,5);
                    break;
                case EMonsterType.Pirate:
                    CurrentElement = 1;
                    break;
                case EMonsterType.Spider:
                    CurrentElement = 2;
                    break;
                case EMonsterType.Bear:
                    CurrentElement = 4;
                    break;
                default:
                    CurrentElement = 0;
                    break;
            }
        }
        else
        {
            CurrentElement = Choose(ElementArr);
        }

        switch (CurrentElement)
        {
            case 0:
                break;
            case 1:
                // 기존보다 체력이 50% 많으며, 방어막이 35% 적다
                MaxHP += BaseHP * 0.50f;
                MaxShield -= BaseShield * 0.35f;
                break;
            case 2:
                // 기존보다 체력과 방어막이 25%씩 증가하며, 속도가 40% 감소한다.
                MaxHP += BaseHP * 0.25f;
                MaxShield += BaseShield * 0.25f;
                Speed -= BaseSpeed * 0.40f;
                break;
            case 3:
                // 기존보다 체력이 50% 적으며, 방어막이 40% 많으며, 속도가 10% 증가한다.
                MaxHP -= BaseHP * 0.50f;
                MaxShield += BaseShield * 0.40f;
                Speed += BaseSpeed * 0.10f;
                break;
            case 4:
                // 체력과 방어막이 25%씩 감소하며, 속도가 40% 증가한다.
                MaxHP -= BaseHP * 0.25f;
                MaxShield -= BaseShield * 0.25f;
                Speed += BaseSpeed * 0.40f;
                break;
        }

    }

    public void AddProperty()
    {
        int waveNum = GameManager.instance.level;

        if(waveNum <= 6)
        {
            return;
        }

        if (IsBoss == true)
        {
            propertyType = Tables.MonsterProperty.Get(6)._PropertyType;
        }
        else if (IsSubBoss == true)
        {
            propertyType = Tables.MonsterProperty.Get(3)._PropertyType;
        }
        else
            propertyType = Tables.MonsterProperty.Get(1)._PropertyType;


    }

    public void AddSubBoss()
    {
        MaxHP += BaseHP * Tables.MonsterClass.Get(2)._Hp / 100;
        MaxShield += BaseShield * Tables.MonsterClass.Get(2)._Sheild / 100;
        float size = Tables.MonsterClass.Get(2)._Size / 100;
        float currentSize = transform.localScale.x;
        scaleChange = new Vector3(currentSize * size, currentSize * size, currentSize * size);
        transform.localScale += scaleChange;
        propertyType = Tables.MonsterProperty.Get(3)._PropertyType;
    }
    public void AddBoss()
    {
        MaxHP += BaseHP * Tables.MonsterClass.Get(3)._Hp / 100;
        MaxShield += BaseShield * Tables.MonsterClass.Get(3)._Sheild / 100;
        float size = Tables.MonsterClass.Get(3)._Size / 100;
        float currentSize = transform.localScale.x;
        scaleChange = new Vector3(currentSize * size, currentSize * size, currentSize * size);
        transform.localScale += scaleChange;
        propertyType = Tables.MonsterProperty.Get(6)._PropertyType;


    }

    public void EnemyLevelUp()        
    {                                   
        int enemySpawnCount = EnemySpawner.EnemySpawnCounts[monsterType];
        //Debug.Log("enemySpawnCount: " + enemySpawnCount);
        int id = (int)monsterType;

        int countLevel = enemySpawnCount / 2 + 1;
        if(countLevel >= 5)
        {
            currentLevel[id - 1] = 5;
        }
        else if(countLevel >= 6)
        {
            currentLevel[id - 1] = 10;
        }
        else
            currentLevel[id - 1] = countLevel;


        //Debug.Log(monsterType + " CurrentLevel: " + currentLevel[id - 1]);

        MaxHP += BaseHP * Tables.MonsterLevel.Get(currentLevel[id - 1])._Hp * currentLevel[id-1] / 100;
        MaxShield += BaseShield * Tables.MonsterLevel.Get(currentLevel[id -1])._Sheild * currentLevel[id - 1] / 100;
        Armor += BaseArmor * Tables.MonsterLevel.Get(currentLevel[id -1])._Armor * currentLevel[id - 1] / 100;
    }

    

    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.A))
        {
            DealDamageForSeconds(130);
        }*/
    }

   /* public void DealDamageForSeconds(float damage)
    {
        StartCoroutine(DealDamage(damage));
    }

    public IEnumerator DealDamage(float damage)
    {
        int time = 1;
        Debug.Log("ok");
        while (true)
        {
            //TakeDamage(damage);
            yield return new WaitForSeconds(time);
        }
    }*/

    public void InitStat()
    {
        GetStat();
        EnemyLevelUp();
        AddElementType();
        if(hasSpecial == true)
        {
            AddSpeciality();
            Debug.Log("SpecialMonsterSpawned");
        }
        if(IsSubBoss == true)
        {
            AddSubBoss();
        }
        if(IsBoss == true)
        {
            AddBoss();
        }        

        SetStat();
        transform.tag = "Enemy";
        IsAlive = true;
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

}
