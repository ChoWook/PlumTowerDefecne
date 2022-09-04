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


    private int[] currentLevel = new int[8];
        
    float Enforced = 1.0f;          // 강화특성

    public EMonsterType monsterType;

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
        ShieldOn = true;
    }
    
    public void SetStat()
    {
        CurrentHP = MaxHP;
        CurrentShield = MaxShield;
        //Debug.Log("CurrentHP: " + CurrentHP + " CurrentShield: " + CurrentShield + " Armor: " + Armor + " Speed: " + Speed);
    }

    IEnumerator IE_PlayDeadAnimation()
    {
        GetComponent<BaseAniContoller>().DeadAnimation();
        yield return new WaitForSeconds(1);
        ObjectPools.Instance.ReleaseObjectToPool(gameObject);
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
            KillEnemy();
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

    PropertyType MyProperty;
    Speciality1Type MySpeciality1;
    Speciality2Type MySpeciality2;

    public enum PropertyType                                      // 속성은 맵이나 타워에도 적용
    {
        물, 흙, 불, 전기
    }

    enum Speciality1Type
    {
        없음, 큰, 거대한, 튼튼한, 단단한, 풍부한, 신속한, 쾌속의
    }

    enum Speciality2Type
    {
        없음, 강화된, 분열의, 분열된, 은밀한, 부활의, 부활한,
        생성의, 이끄는, 저주하는
    }

    public int LevelType;

    void ApplyPropertyType()
    {
        // MyProperty 결정방식 필요
        // 웨이브 정보?
        // 완전 랜덤? 계수?

        switch (MyProperty)
        {
            case PropertyType.물:
                // 기존보다 체력이 50% 많으며, 방어막이 35% 적다
                MaxHP += BaseHP * 0.50f;
                MaxShield -= BaseShield * 0.35f;
                break;
            case PropertyType.흙:
                // 기존보다 체력과 방어막이 25%씩 증가하며, 속도가 40% 감소한다.
                MaxHP += BaseHP * 0.25f;
                MaxShield += BaseShield * 0.25f;
                Speed -= BaseSpeed * 0.40f;
                break;
            case PropertyType.불:
                // 기존보다 체력이 50% 적으며, 방어막이 40% 많으며, 속도가 10% 증가한다.
                MaxHP -= BaseHP * 0.50f;
                MaxShield += BaseShield * 0.40f;
                Speed += BaseSpeed * 0.10f;
                break;
            case PropertyType.전기:
                // 체력과 방어막이 25%씩 감소하며, 속도가 40% 증가한다.
                MaxHP -= BaseHP * 0.25f;
                MaxShield -= BaseShield * 0.25f;
                Speed += BaseSpeed * 0.40f;
                break;
        }
    }

    void ApplySpeciality1Type()
    {
        // MySpeciality1 결정방식 필요
        // 웨이브 정보?
        // 완전 랜덤? 계수?

        switch (MySpeciality1)
        {
            case Speciality1Type.큰:                         //체력 5% 증가
                MaxHP += BaseHP * 0.05f * Enforced;
                break;
            case Speciality1Type.거대한:                     // 체력 10% 증가
                MaxHP += BaseHP * 0.10f * Enforced;
                break;
            case Speciality1Type.튼튼한:                     //방어력 5% 증가
                Armor += BaseArmor * 0.05f * Enforced;
                break;
            case Speciality1Type.단단한:                     //방어력 10% 증가
                Armor += BaseArmor * 0.10f * Enforced;
                break;
            case Speciality1Type.풍부한:                     // 방어막 5% 증가
                MaxShield += BaseShield * 0.05f * Enforced;
                break;
            case Speciality1Type.신속한:                     //속도 10% 증가
                Speed += BaseSpeed * 0.10f * Enforced;
                break;
            case Speciality1Type.쾌속의:                     // 속도 20% 증가
                Speed += BaseSpeed * 0.20f * Enforced;
                break;
            default:
                break;
        }
    }

    void ApplySpeciality2Type()
    {

        //if(게임매니저 웨이브 == 웨이브 값)
        //{
        // 일반몬스터일 경우 0 or 1 랜덤 부여
        // 서브보스일 경우 1 ~ 4 랜덤 부여
        // 보스일 경우 1~9 랜덤 부여
        //}


        switch (MySpeciality2)
        {
            case Speciality2Type.강화된:
                Enforced = 1.2f;
                break;
            case Speciality2Type.분열의:   // 소환 로직 필요
                break;
            case Speciality2Type.분열된:
                break;
            case Speciality2Type.은밀한:   // 타워정보필요
                break;
            case Speciality2Type.부활의:   // 소환 로직 필요
                break;
            case Speciality2Type.부활한:
                break;
            case Speciality2Type.생성의:   // 소환 로직 필요
                break;
            case Speciality2Type.이끄는:   // 칸 정보 필요(맵)
                                        // 버프 로직 필요
                break;
            case Speciality2Type.저주하는: // 맵, UI 정보 필요
                break;
            default:
                break;
        }

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

    public void EnemyLevelUp()        // waveNumber
    {                                   //체력, 방어구 10% 증가
        int enemySpawnCount = EnemySpawner.EnemySpawnCounts[monsterType];
        //Debug.Log("enemySpawnCount: " + enemySpawnCount);
        int id = (int)monsterType;

        int countLevel = enemySpawnCount / 2 + 1;
        if(countLevel >= 5)
        {
            currentLevel[id - 1] = 5;
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
        if (Input.GetKeyDown(KeyCode.A))
        {
            DealDamageForSeconds(130);
        }
    }

    public void DealDamageForSeconds(float damage)
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
    }

    public void InitStat()
    {
        GetStat();
        EnemyLevelUp();
        if(hasSpecial == true)
        {
            AddSpeciality();
            Debug.Log("SpecialMonsterSpawned");
        }
        SetStat();
        transform.tag = "Enemy";
        IsAlive = true;
    }
   
}
