using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // �⺻ Enemy ����, �Ӽ�, Ư�� Class�����

    protected float BaseHP;               // ���������̺��� ��������
    float MaxHP;
    public float CurrentHP;
    protected float BaseShield;           // ���������̺��� ��������
    float MaxShield;

    public float CurrentShield;
    bool ShieldOn = true;
    float Armor;
    protected float BaseArmor;            // ���������̺��� ��������
    public float BaseSpeed;     // ���������̺��� ��������
    public float Speed;             // EnemyMovement ���� ����
    public bool IsAlive = true;

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

    


    public void GetStat(EMonsterType monsterType)
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
        Debug.Log("CurrentHP: " + CurrentHP + " CurrentShield: " + CurrentShield + " Armor: " + Armor + " Speed: " + Speed);
    }

    IEnumerator IE_PlayDeadAnimation()
    {
        switch (monsterType)
        {
            case EMonsterType.Bet:
                GetComponent<BatAniController>().DeadAnimation();
                break;
            case EMonsterType.Mushroom:
                GetComponent<MushAniController>().DeadAnimation();
                break;
            case EMonsterType.Flower:
                GetComponent<FlowerAniController>().DeadAnimation();
                break;
            case EMonsterType.Slime:
                GetComponent<SlimeAniController>().DeadAnimation();
                break;
            case EMonsterType.Fish:
                GetComponent<FishAniController>().TriggerAnimation();
                break;
            case EMonsterType.Pirate:
                GetComponent<PirateAniController>().TriggerAnimation();
                break;
            case EMonsterType.Spider:
                GetComponent<SpiderAniController>().TriggerAnimation();
                break;
            case EMonsterType.Bear:
                GetComponent<BearAniController>().TriggerAnimation();
                break;
        }
        yield return new WaitForSeconds(1);
        ObjectPools.Instance.ReleaseObjectToPool(gameObject);
    }


    

    public void TakeDamage(float damage)
    {

        if (ShieldOn == true)                                      // �ǵ尡 �ִ� ���
        {
            float DamageProtect = CurrentShield;
            CurrentShield -= damage * 0.01f * (100 - Armor) * 0.9f; // ���� �ǵ带 ���

            if (CurrentShield <= 0)                               // ���� �� ������ 
            {
                ShieldOn = false;                                 // �� ����
                damage -= DamageProtect;                          // ������ �氨
                CurrentHP -= damage * 0.01f * (100 - Armor);
                CurrentShield = 0;
            }
        }
        else                                                      // �ǵ尡 ���� ���
        {
            CurrentHP -= damage * 0.01f * (100 - Armor);
        }
        Debug.Log("Shield: " + CurrentShield + "HP: " + CurrentHP);

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
        Debug.Log("Killed Enemy");
        StartCoroutine(IE_PlayDeadAnimation());                                         // ��ü�д°� ���ֱ�
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

    public void EnemyLevelUp(EMonsterType monsterType)        // waveNumber
    {                                   //ü��, �� 10% ����
        int enemySpawnCount = EnemySpawner.EnemySpawnCounts[monsterType];
        Debug.Log("enemySpawnCount: " + enemySpawnCount);
        int id = (int)monsterType;

        int countLevel = enemySpawnCount / 2 + 1;
        if(countLevel >= 5)
        {
            currentLevel[id - 1] = 5;
        }
        else
            currentLevel[id - 1] = countLevel;


        Debug.Log(monsterType + " CurrentLevel: " + currentLevel[id - 1]);

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
            TakeDamage(damage);
            yield return new WaitForSeconds(time);
        }
    }

    public void InitStat()
    {
        GetStat(monsterType);
        EnemyLevelUp(monsterType);
        SetStat();
        transform.tag = "Enemy";
    }
   
}
