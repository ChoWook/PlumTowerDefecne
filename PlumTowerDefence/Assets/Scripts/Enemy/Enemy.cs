using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // �⺻ Enemy ����, �Ӽ�, Ư�� Class�����

    //[SerializeField] --> ���� ����
    float OriginalHP; // ���������̺��� ��������
    float MaxHP;
    float CurrentHP;

    ModifiableValue HP;

    float OriginalShield;// ���������̺��� ��������
    float MaxShield; 
    float CurrentShield;
    bool ShieldOn = true;
    float Armor; 
    float OriginalArmor;// ���������̺��� ��������
    public float OriginalSpeed; // ���������̺��� ��������
    public float Speed; // EnemyMovement ���� ����

    float Enforced = 1.0f; // ��ȭƯ��



    public void TakeDamage(float damage)
    {
        
        if(ShieldOn == true) // �ǵ尡 �ִ� ���
        {
            float DamageProtect = CurrentShield;
            CurrentShield -= damage * 0.01f * (100 - Armor)*0.9f; // ���� �ǵ带 ���
            
            if (CurrentShield <= 0)// ���� �� ������ 
            {
                ShieldOn = false; // �� ����
                damage -= DamageProtect; // ������ �氨
                CurrentHP -= damage * 0.01f * (100 - Armor);
            }
        }
        else // �ǵ尡 ���� ���
        {
            CurrentHP -= damage * 0.01f * (100 - Armor);
        }
        
        if(CurrentHP <= 0)
        {
            KillEnemy();
        }
    }

    public void KillEnemy()
    {
        Destroy(gameObject);
    }

    PropertyType MyProperty;
    Speciality1Type MySpeciality1;
    Speciality2Type MySpeciality2;

    public enum PropertyType // �Ӽ��� ���̳� Ÿ������ ����
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
                MaxHP += OriginalHP * 0.50f;
                MaxShield -= OriginalShield * 0.35f;
                break;
            case PropertyType.��:
                // �������� ü�°� ���� 25%�� �����ϸ�, �ӵ��� 40% �����Ѵ�.
                MaxHP += OriginalHP * 0.25f;
                MaxShield += OriginalShield * 0.25f;
                Speed -= OriginalSpeed * 0.40f;
                break;
            case PropertyType.�� :
                // �������� ü���� 50% ������, ���� 40% ������, �ӵ��� 10% �����Ѵ�.
                MaxHP -= OriginalHP * 0.50f;
                MaxShield += OriginalShield * 0.40f;
                Speed += OriginalSpeed * 0.10f;
                break;
            case PropertyType.����:
                // ü�°� ���� 25%�� �����ϸ�, �ӵ��� 40% �����Ѵ�.
                MaxHP -= OriginalHP * 0.25f;
                MaxShield -= OriginalShield * 0.25f;
                Speed += OriginalSpeed * 0.40f;
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
            case Speciality1Type.ū: //ü�� 5% ����
                MaxHP += OriginalHP * 0.05f* Enforced;
                break;
            case Speciality1Type.�Ŵ���: // ü�� 10% ����
                MaxHP += OriginalHP * 0.10f * Enforced;
                break;
            case Speciality1Type.ưư��: //���� 5% ����
                Armor += OriginalArmor * 0.05f * Enforced;
                break;
            case Speciality1Type.�ܴ���: //���� 10% ����
                Armor += OriginalArmor * 0.10f * Enforced;
                break;
            case Speciality1Type.ǳ����: // �� 5% ����
                MaxShield += OriginalShield * 0.05f * Enforced;
                break;
            case Speciality1Type.�ż���: //�ӵ� 10% ����
                Speed += OriginalSpeed * 0.10f * Enforced;
                break;
            case Speciality1Type.�����: // �ӵ� 20% ����
                Speed += OriginalSpeed * 0.20f * Enforced;
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
            case Speciality2Type.�п���: // ��ȯ ���� �ʿ�
                break;
            case Speciality2Type.�п���:
                break;
            case Speciality2Type.������: // Ÿ�������ʿ�
                break;
            case Speciality2Type.��Ȱ��: // ��ȯ ���� �ʿ�
                break;
            case Speciality2Type.��Ȱ��: 
                break;
            case Speciality2Type.������: // ��ȯ ���� �ʿ�
                break;
            case Speciality2Type.�̲���: // ĭ ���� �ʿ�(��)
                                         // ���� ���� �ʿ�
                break;
            case Speciality2Type.�����ϴ�: // ��, UI ���� �ʿ�
                break;
            default :
                break;
        }

    }

    void EnemyLevelUp(int level) // CurrentLevel �μ��� �ޱ�
    { //ü��, �� 10% ����
        MaxHP += OriginalHP * 0.10f; // �տ��� �����ϱ� + ���� Ư��, �Ӽ��� �տ���?
        MaxShield += OriginalShield * 0.10f;

        if (level > 3) // lv 4 �̻���� ���µ� ����
        {
            Armor += OriginalArmor * 0.1f;
        }
    }



    private void Start()
    {
        
    }

    private void Update()
    {
        //if(WaveStart){
        // 
        //}
        MaxHP = OriginalHP;
        MaxShield = OriginalShield;
        CurrentHP = MaxHP;

        HP.BaseValue = 3;

        CurrentShield = MaxShield;
        Armor = OriginalArmor;
        Speed = OriginalSpeed;
        ApplyPropertyType();
        ApplySpeciality2Type();
        ApplySpeciality1Type();

        //if(Ư�� ���̺� ���� �޼�){
        // EnemyLevelUp(���緹��) //UI ��� �ʿ�
        //}
    }

}
