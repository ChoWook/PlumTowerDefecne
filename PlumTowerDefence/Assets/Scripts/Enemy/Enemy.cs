using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // �⺻ Enemy ����, �Ӽ�, Ư�� Class�����

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
        
    float Enforced = 1.0f;          // ��ȭƯ��

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
        if (ShieldOn == true)                                      // �ǵ尡 �ִ� ���
        {
            float DamageProtect = CurrentShield;
            CurrentShield -= damage * 0.01f * (100 - Armor * penetrationOn) * 0.9f * shieldSpecial; // ���� �ǵ带 ���

            if (CurrentShield <= 0)                               // ���� �� ������ 
            {
                ShieldOn = false;                                 // �� ����
                damage -= DamageProtect;                          // ������ �氨
                CurrentHP -= damage * 0.01f * (100 - Armor * penetrationOn) * hpSpecial;
                CurrentShield = 0;
            }
        }
        else                                                      // �ǵ尡 ���� ���
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

    public enum PropertyType                                      // �Ӽ��� ���̳� Ÿ������ ����
    {
        ��, ��, ��, ����
    }

    enum Speciality1Type
    {
        ����, ū, �Ŵ���, ưư��, �ܴ���, ǳ����, �ż���, �����
    }

    enum Speciality2Type
    {
        ����, ��ȭ��, �п���, �п���, ������, ��Ȱ��, ��Ȱ��,
        ������, �̲���, �����ϴ�
    }

    public int LevelType;

    void ApplyPropertyType()
    {
        // MyProperty ������� �ʿ�
        // ���̺� ����?
        // ���� ����? ���?

        switch (MyProperty)
        {
            case PropertyType.��:
                // �������� ü���� 50% ������, ���� 35% ����
                MaxHP += BaseHP * 0.50f;
                MaxShield -= BaseShield * 0.35f;
                break;
            case PropertyType.��:
                // �������� ü�°� ���� 25%�� �����ϸ�, �ӵ��� 40% �����Ѵ�.
                MaxHP += BaseHP * 0.25f;
                MaxShield += BaseShield * 0.25f;
                Speed -= BaseSpeed * 0.40f;
                break;
            case PropertyType.��:
                // �������� ü���� 50% ������, ���� 40% ������, �ӵ��� 10% �����Ѵ�.
                MaxHP -= BaseHP * 0.50f;
                MaxShield += BaseShield * 0.40f;
                Speed += BaseSpeed * 0.10f;
                break;
            case PropertyType.����:
                // ü�°� ���� 25%�� �����ϸ�, �ӵ��� 40% �����Ѵ�.
                MaxHP -= BaseHP * 0.25f;
                MaxShield -= BaseShield * 0.25f;
                Speed += BaseSpeed * 0.40f;
                break;
        }
    }

    void ApplySpeciality1Type()
    {
        // MySpeciality1 ������� �ʿ�
        // ���̺� ����?
        // ���� ����? ���?

        switch (MySpeciality1)
        {
            case Speciality1Type.ū:                         //ü�� 5% ����
                MaxHP += BaseHP * 0.05f * Enforced;
                break;
            case Speciality1Type.�Ŵ���:                     // ü�� 10% ����
                MaxHP += BaseHP * 0.10f * Enforced;
                break;
            case Speciality1Type.ưư��:                     //���� 5% ����
                Armor += BaseArmor * 0.05f * Enforced;
                break;
            case Speciality1Type.�ܴ���:                     //���� 10% ����
                Armor += BaseArmor * 0.10f * Enforced;
                break;
            case Speciality1Type.ǳ����:                     // �� 5% ����
                MaxShield += BaseShield * 0.05f * Enforced;
                break;
            case Speciality1Type.�ż���:                     //�ӵ� 10% ����
                Speed += BaseSpeed * 0.10f * Enforced;
                break;
            case Speciality1Type.�����:                     // �ӵ� 20% ����
                Speed += BaseSpeed * 0.20f * Enforced;
                break;
            default:
                break;
        }
    }

    void ApplySpeciality2Type()
    {

        //if(���ӸŴ��� ���̺� == ���̺� ��)
        //{
        // �Ϲݸ����� ��� 0 or 1 ���� �ο�
        // ���꺸���� ��� 1 ~ 4 ���� �ο�
        // ������ ��� 1~9 ���� �ο�
        //}


        switch (MySpeciality2)
        {
            case Speciality2Type.��ȭ��:
                Enforced = 1.2f;
                break;
            case Speciality2Type.�п���:   // ��ȯ ���� �ʿ�
                break;
            case Speciality2Type.�п���:
                break;
            case Speciality2Type.������:   // Ÿ�������ʿ�
                break;
            case Speciality2Type.��Ȱ��:   // ��ȯ ���� �ʿ�
                break;
            case Speciality2Type.��Ȱ��:
                break;
            case Speciality2Type.������:   // ��ȯ ���� �ʿ�
                break;
            case Speciality2Type.�̲���:   // ĭ ���� �ʿ�(��)
                                        // ���� ���� �ʿ�
                break;
            case Speciality2Type.�����ϴ�: // ��, UI ���� �ʿ�
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
    {                                   //ü��, �� 10% ����
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
