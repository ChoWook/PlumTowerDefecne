using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // 기본 Enemy 스탯, 속성, 특성 Class만들기

    protected float BaseHP;
    public float MaxHP;
    float _CurrentHP;
    public float CurrentHP
    {
        get { return _CurrentHP; }
        set { 
            _CurrentHP = value;
            monsterUI?.HandleHp();
        }


    }

    protected float BaseShield;           
    public float MaxShield;

    float _CurrentShield;
    public float CurrentShield
    {
        get { return _CurrentShield; }
        set { 
            _CurrentShield = value;
            monsterUI?.HandleHp();
        }

    }

    bool ShieldOn = true;
    public float Armor;
    protected float BaseArmor;            
    public float BaseSpeed;               
    public float Speed;
    public float CurSpeed
    {
        get
        {
            return Speed * SlowAmount / 100;
        }
    }
    public bool IsAlive = true;
    public bool hasSpecial;
    public int CurrentElement;
    public bool IsBoss;
    public bool IsSubBoss;
    private Vector3 scaleChange;
    public bool[] IsBuffed;
    public bool IsSlowed = false;
    public bool IsPoisoned = false;
    private int dividedEnemyNum = 0;
    public int specialityType;
    private float PoisonTime;
    private float PoisonRunTime;
    private float PoisonDamage;
    private float BurnTime;
    private float BurnRunTime;
    private float BurnDamage;
    private float SlowAmount;


    private int[] currentLevel = new int[8];
    float[] ElementArr = new float[5];
        
    float Enforced = 1.0f;          // 강화특성

    public EMonsterType monsterType;
    public EPropertyType propertyType;
    public ESpecialityType SpecialityType;
    public ELaneBuffType currentBuffType;

    MonsterUI monsterUI;
    EnemyName enemyName;
    Animator animator;
    float SlowedAbilty;

    private void Awake()
    {
        for(int i = 0; i < 8; i++)
        {
            currentLevel[i] = 1;
        }
        
        monsterUI = transform.Find("Canvas").GetComponent<MonsterUI>();
        enemyName = transform.Find("Canvas").Find("HP bar").Find("Name").GetComponent<EnemyName>();

    }
    private void OnEnable()
    {
        Init();
    }

    void Init()
    {
        IsBuffed = new bool[6];
        for (int i = 0; i < 6; i++)
        {
            IsBuffed[i] = false;
        }
        IsBoss = false;
        IsSubBoss = false;
        IsAlive = true;
        IsSlowed = false;
        IsPoisoned = false;

        PoisonTime = 0f;
        PoisonRunTime = 0f;
        PoisonDamage = 0f;
        BurnTime = 0f;
        BurnRunTime = 0f;
        BurnDamage = 0f;
        SlowAmount = 0f;

        GetComponent<BaseAniContoller>().Isrevived = false;
        GetComponent<BaseAniContoller>().Isdivided = false;
        monsterUI.gameObject.SetActive(true);
    }

    private void Update()
    {
       /* if (CurrentHP <= 0)
        {
            bar.SetActive(false);
        }
        else
            bar.SetActive(true);*/
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
            enemyName.ShowName();

        }
        else if(propertyType == EPropertyType.Divisive)
        {
            propertyType = EPropertyType.Divided;
            enemyName.ShowName();
        }
        else
        {
            yield return new WaitForSeconds(1);

            ObjectPools.Instance.ReleaseObjectToPool(gameObject);
        }
    }
    IEnumerator IE_Resurrection()
    {
        IsAlive = false;
        float currentSpeed = Speed;
        SetStat();
        transform.tag = "Untagged";
        Speed = 0;
        StartCoroutine(IE_PlayDeadAnimation());
        if(propertyType == EPropertyType.Resurrected)
        {
            GetComponent<BaseAniContoller>().Isrevived = true;
            yield return new WaitForSeconds(1f);
            transform.tag = "Enemy";
            //GetComponent<EnemyMovement>().MoveSpeed = Speed;
            IsAlive = true;
            GetComponent<BaseAniContoller>().InitAnimation();
            Speed = currentSpeed;
        }
        else if(propertyType == EPropertyType.Divided)
        {
            yield return null;
            GetComponent<BaseAniContoller>().Isdivided = true;
            IsAlive = true;
            transform.tag = "Enemy";
            //GetComponent<EnemyMovement>().MoveSpeed = Speed;

            GetComponent<BaseAniContoller>().InitAnimation();
            Speed = currentSpeed;
        }


    }
    void Resurrect()
    {
        StartCoroutine(IE_Resurrection());
    }

    public void TakeDamage(float damage, EAttackSpecialization type, ETowerName towerName)
    {
        
        switch (towerName)
        {
            case ETowerName.Arrow:
                if (IsBuffed[0])
                {
                    float buffedBonus = Tables.MonsterLaneBuff.Get((int)currentBuffType)._Amount;

                    damage += damage * buffedBonus / 100;
                    StartCoroutine(IE_BuffTimeLast(() =>
                    {
                        IsBuffed[0] = false;
                    }));
                }
                break;
            case ETowerName.Hourglass:
                if (IsBuffed[1])
                {
                    //GetComponent<EnemyMovement>().MoveSpeed += GetComponent<EnemyMovement>().MoveSpeed * buffedBonus / 100;
                    
                }
                break;
            case ETowerName.Poison:
                if (IsBuffed[2])
                {

                    float buffedBonus = Tables.MonsterLaneBuff.Get((int)currentBuffType)._Amount;

                    damage += damage * buffedBonus / 100;
                    StartCoroutine(IE_BuffTimeLast(() =>
                    {
                        IsBuffed[2] = false;
                    }));
                }
                break;
            case ETowerName.Flame:
                if (IsBuffed[3])
                {
                    float buffedBonus = Tables.MonsterLaneBuff.Get((int)currentBuffType)._Amount;

                    damage += damage * buffedBonus / 100;
                    StartCoroutine(IE_BuffTimeLast(() =>
                    {
                        IsBuffed[3] = false;
                    }));
                }
                break;
            case ETowerName.Laser:
                if (IsBuffed[4])
                {
                    float buffedBonus = Tables.MonsterLaneBuff.Get((int)currentBuffType)._Amount;

                    damage += damage * buffedBonus / 100;
                    StartCoroutine(IE_BuffTimeLast(() =>
                    {
                        IsBuffed[4] = false;
                    }));
                }
                break;
            case ETowerName.Missile:
                if (IsBuffed[5])
                {
                    float buffedBonus = Tables.MonsterLaneBuff.Get((int)currentBuffType)._Amount;

                    damage += damage * buffedBonus / 100;
                    StartCoroutine(IE_BuffTimeLast(() =>
                    {
                        IsBuffed[5] = false;
                    }));
                }
                break;

        }

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
            monsterUI.gameObject.SetActive(false);

            if (propertyType == EPropertyType.Resurrect)
            {
                Resurrect(); 
                monsterUI.gameObject.SetActive(true);
            }
           else if(propertyType == EPropertyType.Divisive)
            {
                Resurrect();
                monsterUI.gameObject.SetActive(true);
                                
                MaxHP *= 0.3f;
                MaxShield *= 0.2f;

                SpawnDivided();
            }
           else
            {
                if(IsAlive == true)
                {
                    KillEnemy();
                    IncreaseMoney();
                }
            }

        }
    }

    void TakeDamage(float damage)
    {
        if (ShieldOn == true)                                      // 실드가 있는 경우
        {
            float DamageProtect = CurrentShield;
            CurrentShield -= damage; // 현재 실드를 깐다

            if (CurrentShield <= 0)                               // 방어구가 다 까지면 
            {
                ShieldOn = false;                                 // 방어구 제거
                damage -= DamageProtect;                          // 데미지 경감
                CurrentHP -= damage;
                CurrentShield = 0;
            }
        }
        else                                                      // 실드가 없는 경우
        {
            CurrentHP -= damage;
        }
    }


    void KillEnemy()
    {
        transform.tag = "Untagged";
        if(IsAlive == true)
        {
            IsAlive = false;
            GameManager.instance.CurrentEnemyNumber--;

        }
        enemyName.HideName();
        //GetComponent<EnemyMovement>().MoveSpeed = 0;
        //Debug.Log("Killed Enemy" + "Current Enemy Num: " + GameManager.instance.currentEnemyNumber);
        StartCoroutine(IE_PlayDeadAnimation());
    }

    public void SpawnDivided()
    {
        var enemy = ObjectPools.Instance.GetPooledObject(monsterType.ToString());
        var emove = enemy.GetComponent<EnemyMovement>();
        emove.Route = gameObject.GetComponent<EnemyMovement>().Route;
        emove.WaypointIndex = gameObject.GetComponent<EnemyMovement>().WaypointIndex;
        enemy.transform.position = gameObject.transform.position;
        var getComponent = enemy.GetComponent<Enemy>();
        getComponent.monsterType = monsterType;
        getComponent.hasSpecial = true;
        getComponent.IsSubBoss = true;
        getComponent.GetStat();
        getComponent.InitSize();
        getComponent.EnemyLevelUp();
        getComponent.transform.tag = "Enemy";
        getComponent.IsAlive = true;
        getComponent.CurrentElement = CurrentElement;
        enemy.GetComponent<BaseAniContoller>().InitAnimation();
        emove.InitSpeed(monsterType);
        getComponent.propertyType = EPropertyType.Divided;
        getComponent.MaxHP = MaxHP;
        getComponent.MaxShield = MaxShield;
        getComponent.enemyName.ShowName();
        getComponent.SetStat();

        GameManager.instance.CurrentEnemyNumber++;

        float currentSize = enemy.transform.localScale.x;
        scaleChange = new Vector3(currentSize, currentSize, currentSize);
        transform.localScale = scaleChange;

        GetComponent<EnemyMovement>().MoveSpeed *= 0.9f;
        dividedEnemyNum++;
        //Debug.Log("Divided Enemy Spawned: " + dividedEnemyNum);
        //Debug.Log("Current Enemy Number" + GameManager.instance.currentEnemyNumber);
    }

    public void IncreaseMoney()
    {
        int waveNum = GameManager.instance.Level;
        int MoneyAdd = Tables.MonsterMoneyAmount.Get(waveNum)._Money;
        GameManager.instance.Money += MoneyAdd;

    }

    public void SlowEnemy(float abilty)
    {
        SlowedAbilty = abilty;
        if(IsSlowed == false)
        {
            //GetComponent<EnemyMovement>().MoveSpeed *= abilty;
            Speed *= abilty;
            IsSlowed = true;
            StartCoroutine(IE_SlowTime());
        }
        
    }

    public void PoisonEnemy(float ability)
    {
        if(IsPoisoned == false)
        {
            IsPoisoned = true;
            StartCoroutine(IE_PoisonDamage(ability));
        }
    }

    public void TakeBuff(ELaneBuffType laneBufftype)
    {
        currentBuffType = laneBufftype;
        float statChange =Tables.MonsterLaneBuff.Get((int)currentBuffType)._Amount;

        switch (currentBuffType)
        {
            case ELaneBuffType.AllHealHp:
                CurrentHP += MaxHP * statChange / 100;
                break;
            case ELaneBuffType.AllDealHp:
                CurrentHP += MaxHP * statChange / 100;
                break;
            case ELaneBuffType.AllHealShield:
                CurrentShield += MaxShield * statChange / 100;
                break;
            case ELaneBuffType.AllDealShield:
                CurrentShield += MaxShield * statChange / 100;
                break;
            case ELaneBuffType.AllBuffArmor:
                Armor += BaseArmor * statChange / 100;
                StartCoroutine(IE_BuffTimeLast(() =>
                {
                    Armor -= BaseArmor * statChange / 100;
                }));
                break;
            case ELaneBuffType.AllNurfArmor:
                Armor += BaseArmor * statChange / 100;
                StartCoroutine(IE_BuffTimeLast(() =>
                {
                    Armor -= BaseArmor * statChange / 100;
                }));
                break;
            case ELaneBuffType.WaterHealHp:
                if((EElementalType)CurrentElement == EElementalType.Water)
                {
                    CurrentHP += MaxHP * statChange / 100;
                }
                break;
            case ELaneBuffType.GroundHealHp:
                if ((EElementalType)CurrentElement == EElementalType.Ground)
                {
                    CurrentHP += MaxHP * statChange / 100;
                }
                break;
            case ELaneBuffType.FireHealHp:
                if ((EElementalType)CurrentElement == EElementalType.Fire)
                {
                    CurrentHP += MaxHP * statChange / 100;
                }
                break;
            case ELaneBuffType.ElectricHealHp:
                if ((EElementalType)CurrentElement == EElementalType.Electric)
                {
                    CurrentHP += MaxHP * statChange / 100;
                }
                break;
            case ELaneBuffType.WaterDealHp:
                if ((EElementalType)CurrentElement == EElementalType.Water)
                {
                    CurrentHP += MaxHP * statChange / 100;
                }
                break;
            case ELaneBuffType.GroundDealHp:
                if ((EElementalType)CurrentElement == EElementalType.Ground)
                {
                    CurrentHP += MaxHP * statChange / 100;
                }
                break;
            case ELaneBuffType.FireDealHp:
                if ((EElementalType)CurrentElement == EElementalType.Fire)
                {
                    CurrentHP += MaxHP * statChange / 100;
                }
                break;
            case ELaneBuffType.ElectricDealHp:
                if ((EElementalType)CurrentElement == EElementalType.Electric)
                {
                    CurrentHP += MaxHP * statChange / 100;
                }
                break;
            case ELaneBuffType.WaterBuffArmor:
                if ((EElementalType)CurrentElement == EElementalType.Water)
                {
                    Armor += BaseArmor * statChange / 100;
                    StartCoroutine(IE_BuffTimeLast(() =>
                    {
                        Armor -= BaseArmor * statChange / 100;
                    }));
                    break;
                }
                break;
            case ELaneBuffType.GroundBuffArmor:
                if ((EElementalType)CurrentElement == EElementalType.Ground)
                {
                    Armor += BaseArmor * statChange / 100;
                    StartCoroutine(IE_BuffTimeLast(() =>
                    {
                        Armor -= BaseArmor * statChange / 100;
                    }));
                    break;
                }
                break;
            case ELaneBuffType.FireBuffArmor:
                if ((EElementalType)CurrentElement == EElementalType.Fire)
                {
                    Armor += BaseArmor * statChange / 100;
                    StartCoroutine(IE_BuffTimeLast(() =>
                    {
                        Armor -= BaseArmor * statChange / 100;
                    }));
                    break;
                }
                break;
            case ELaneBuffType.ElectricBuffArmor:
                if ((EElementalType)CurrentElement == EElementalType.Electric)
                {
                    Armor += BaseArmor * statChange / 100;
                    StartCoroutine(IE_BuffTimeLast(() =>
                    {
                        Armor -= BaseArmor * statChange / 100;
                    }));
                    break;
                }
                break;
            case ELaneBuffType.WaterNurfArmor:
                if ((EElementalType)CurrentElement == EElementalType.Water)
                {
                    Armor += BaseArmor * statChange / 100;
                    StartCoroutine(IE_BuffTimeLast(() =>
                    {
                        Armor -= BaseArmor * statChange / 100;
                    }));
                    break;
                }
                break;
            case ELaneBuffType.GroundNurfArmor:
                if ((EElementalType)CurrentElement == EElementalType.Ground)
                {
                    Armor += BaseArmor * statChange / 100;
                    StartCoroutine(IE_BuffTimeLast(() =>
                    {
                        Armor -= BaseArmor * statChange / 100;
                    }));
                    break;
                }
                break;
            case ELaneBuffType.FireNurfArmor:
                if ((EElementalType)CurrentElement == EElementalType.Fire)
                {
                    Armor += BaseArmor * statChange / 100;
                    StartCoroutine(IE_BuffTimeLast(() =>
                    {
                        Armor -= BaseArmor * statChange / 100;
                    }));
                    break;
                }
                break;
            case ELaneBuffType.ElectricNurfArmor:
                if ((EElementalType)CurrentElement == EElementalType.Electric)
                {
                    Armor += BaseArmor * statChange / 100;
                    StartCoroutine(IE_BuffTimeLast(() =>
                    {
                        Armor -= BaseArmor * statChange / 100;
                    }));
                    break;
                }
                break;

            case ELaneBuffType.ArrowBuff:
                IsBuffed[0] = true;
                break;
            case ELaneBuffType.SlowBuff:
                IsBuffed[1] = true;
                break;
            case ELaneBuffType.PoisonBuff:
                IsBuffed[2] = true;
                break;
            case ELaneBuffType.LazerBuff:
                IsBuffed[3] = true;
                break;
            case ELaneBuffType.MissileBuff:
                IsBuffed[4] = true;
                break;
        }
    }

    void TakeTowerDebuff(ETowerDebuffType eTowerDebuffType, float amount, float time = -1)
    {
        switch (eTowerDebuffType)
        {
            case ETowerDebuffType.Poison:

                if(PoisonRunTime >= PoisonTime || amount >= PoisonDamage)
                {
                    PoisonTime = time;
                    PoisonRunTime = 0;

                    StopCoroutine(nameof(IE_TakePoisonDamage));

                    StartCoroutine(nameof(IE_TakePoisonDamage), amount);
                }
                
                break;
            case ETowerDebuffType.Burn:

                if (BurnRunTime >= BurnTime || amount >= BurnDamage)
                {
                    BurnTime = time;
                    BurnRunTime = 0;

                    StopCoroutine(nameof(IE_TakeBurnDamage));

                    StartCoroutine(nameof(IE_TakeBurnDamage), amount);
                }

                break;
            case ETowerDebuffType.Slow:
                if(amount > SlowAmount)
                {
                    SlowAmount = amount;
                }

                break;
        }
    }

    IEnumerator IE_TakePoisonDamage(float amount)
    {
        WaitForSeconds ws = new WaitForSeconds(1f);

        PoisonDamage = amount;

        while (PoisonRunTime < PoisonTime) 
        {
            TakeDamage(PoisonDamage);

            yield return ws;

            PoisonRunTime += Time.deltaTime;
        }
    }

    IEnumerator IE_TakeBurnDamage(float amount)
    {
        WaitForSeconds ws = new WaitForSeconds(1f);

        BurnDamage = amount;

        while (BurnRunTime < BurnTime)
        {
            TakeDamage(BurnDamage);

            yield return ws;

            BurnRunTime += Time.deltaTime;
        }
    }

    IEnumerator IE_BuffTimeLast(System.Action onEnd)
    {
        int bufftime = Tables.MonsterLaneBuff.Get((int)currentBuffType)._Time;
        yield return new WaitForSeconds(bufftime);
        onEnd();
    }
    
    IEnumerator IE_SlowTime()
    {
        float slowtime = 5f;
        yield return new WaitForSeconds(slowtime);              // 시간초기화 구현 x
        IsSlowed = false;
        //GetComponent<EnemyMovement>().MoveSpeed = Speed;
        Speed /= SlowedAbilty;
    }

    IEnumerator IE_PoisonDamage(float poisonDamage)
    {
        float poisonTime = 1f;
        while(CurrentHP > 0) { 
            TakeDamage(poisonDamage, EAttackSpecialization.Default, ETowerName.Poison);
            yield return new WaitForSeconds(poisonTime);
        }
    }


   

    public void AddSpeciality()
    {
        int id = Tables.Monster.Get((int)monsterType)._ID;
        if(Random.Range(0, 2) == 0)
        {
            specialityType = Tables.Monster.Get(id)._Speciality_1;
        }
        else
            specialityType= Tables.Monster.Get(id)._Speciality_2;

        var changeStat = Tables.MonsterSpeciality.Get(specialityType)._Amount/100;
        var monsterStat = Tables.MonsterSpeciality.Get(specialityType)._ChangeStat;
        SpecialityType = Tables.MonsterSpeciality.Get(specialityType)._SpecialityType;

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
        int waveNum = GameManager.instance.Level;                         // 커밋할때 바꾸기
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
        int waveNum = GameManager.instance.Level;

        /*if(waveNum <= 6)
        {
            return;
        }*/
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

    
    public void AddBossStat()
    {
        int id;
        if (IsBoss == true)
        {
            id = 3;
        }
        else if (IsSubBoss == true)
        {
            id = 2;
        }
        else
            id = 1;
        MaxHP += BaseHP * Tables.MonsterClass.Get(id)._Hp / 100;
        MaxShield += BaseShield * Tables.MonsterClass.Get(id)._Sheild / 100;
        float size = Tables.MonsterClass.Get(id)._Size / 100;
        float currentSize = transform.localScale.x;
        scaleChange = new Vector3(currentSize * size, currentSize * size, currentSize * size);
        transform.localScale += scaleChange;
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
        else
            currentLevel[id - 1] = countLevel;

        if(GameManager.instance.Level >= 45 && monsterType != EMonsterType.Bear)
        {
            currentLevel[id - 1] = 10;
        }


        //Debug.Log(monsterType + " CurrentLevel: " + currentLevel[id - 1]);

        MaxHP += BaseHP * Tables.MonsterLevel.Get(currentLevel[id - 1])._Hp * currentLevel[id-1] / 100;
        MaxShield += BaseShield * Tables.MonsterLevel.Get(currentLevel[id -1])._Sheild * currentLevel[id - 1] / 100;
        Armor += BaseArmor * Tables.MonsterLevel.Get(currentLevel[id -1])._Armor * currentLevel[id - 1] / 100;
    }

    void InitSize()
    {
        switch (monsterType)
        {
            case EMonsterType.Bet:
                transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
                break;
            case EMonsterType.Flower:
                transform.localScale = new Vector3(1.4f, 1.4f, 1.4f);
                break;
            case EMonsterType.Spider:
                transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
                break;
            default:
                transform.localScale = new Vector3(1f, 1f, 1f);
                break;
        }

    }

    public void InitStat()
    {
        GetStat();
        InitSize();
        EnemyLevelUp();
        AddElementType();
        if(propertyType == EPropertyType.Divided)
        {
            propertyType = EPropertyType.None;
        }
        if (hasSpecial == true)
        {
            AddSpeciality();
            //Debug.Log("SpecialMonsterSpawned");
        }
        else
        {
            SpecialityType = ESpecialityType.None;
        }
        if(IsSubBoss == true || IsBoss == true)
        {
            AddBossStat();
            AddProperty();
        }
        else
        {
            propertyType = EPropertyType.None;
        }
        
        transform.tag = "Enemy";
        IsAlive = true;
        enemyName?.ShowName();
        SetStat();
       
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
