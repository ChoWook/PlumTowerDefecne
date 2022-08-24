using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // 기본 Enemy 스탯, 속성, 특성 Class만들기

    //[SerializeField] --> 사용법 배우기
    float OriginalHP; // 데이터테이블에서 가져오기
    float MaxHP;
    float CurrentHP;

    ModifiableValue HP;

    float OriginalShield;// 데이터테이블에서 가져오기
    float MaxShield; 
    float CurrentShield;
    bool ShieldOn = true;
    float Armor; 
    float OriginalArmor;// 데이터테이블에서 가져오기
    public float OriginalSpeed; // 데이터테이블에서 가져오기
    public float Speed; // EnemyMovement 에서 조정

    float Enforced = 1.0f; // 강화특성



    public void TakeDamage(float damage)
    {
        
        if(ShieldOn == true) // 실드가 있는 경우
        {
            float DamageProtect = CurrentShield;
            CurrentShield -= damage * 0.01f * (100 - Armor)*0.9f; // 현재 실드를 깐다
            
            if (CurrentShield <= 0)// 방어구가 다 까지면 
            {
                ShieldOn = false; // 방어구 제거
                damage -= DamageProtect; // 데미지 경감
                CurrentHP -= damage * 0.01f * (100 - Armor);
            }
        }
        else // 실드가 없는 경우
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

    public enum PropertyType // 속성은 맵이나 타워에도 적용
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
                MaxHP += OriginalHP * 0.50f;
                MaxShield -= OriginalShield * 0.35f;
                break;
            case PropertyType.흙:
                // 기존보다 체력과 방어막이 25%씩 증가하며, 속도가 40% 감소한다.
                MaxHP += OriginalHP * 0.25f;
                MaxShield += OriginalShield * 0.25f;
                Speed -= OriginalSpeed * 0.40f;
                break;
            case PropertyType.불 :
                // 기존보다 체력이 50% 적으며, 방어막이 40% 많으며, 속도가 10% 증가한다.
                MaxHP -= OriginalHP * 0.50f;
                MaxShield += OriginalShield * 0.40f;
                Speed += OriginalSpeed * 0.10f;
                break;
            case PropertyType.전기:
                // 체력과 방어막이 25%씩 감소하며, 속도가 40% 증가한다.
                MaxHP -= OriginalHP * 0.25f;
                MaxShield -= OriginalShield * 0.25f;
                Speed += OriginalSpeed * 0.40f;
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
            case Speciality1Type.큰: //체력 5% 증가
                MaxHP += OriginalHP * 0.05f* Enforced;
                break;
            case Speciality1Type.거대한: // 체력 10% 증가
                MaxHP += OriginalHP * 0.10f * Enforced;
                break;
            case Speciality1Type.튼튼한: //방어력 5% 증가
                Armor += OriginalArmor * 0.05f * Enforced;
                break;
            case Speciality1Type.단단한: //방어력 10% 증가
                Armor += OriginalArmor * 0.10f * Enforced;
                break;
            case Speciality1Type.풍부한: // 방어막 5% 증가
                MaxShield += OriginalShield * 0.05f * Enforced;
                break;
            case Speciality1Type.신속한: //속도 10% 증가
                Speed += OriginalSpeed * 0.10f * Enforced;
                break;
            case Speciality1Type.쾌속의: // 속도 20% 증가
                Speed += OriginalSpeed * 0.20f * Enforced;
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
            case Speciality2Type.분열의: // 소환 로직 필요
                break;
            case Speciality2Type.분열된:
                break;
            case Speciality2Type.은밀한: // 타워정보필요
                break;
            case Speciality2Type.부활의: // 소환 로직 필요
                break;
            case Speciality2Type.부활한: 
                break;
            case Speciality2Type.생성의: // 소환 로직 필요
                break;
            case Speciality2Type.이끄는: // 칸 정보 필요(맵)
                                         // 버프 로직 필요
                break;
            case Speciality2Type.저주하는: // 맵, UI 정보 필요
                break;
            default :
                break;
        }

    }

    void EnemyLevelUp(int level) // CurrentLevel 인수로 받기
    { //체력, 방어구 10% 증가
        MaxHP += OriginalHP * 0.10f; // 합연산 적용하기 + 위의 특성, 속성도 합연산?
        MaxShield += OriginalShield * 0.10f;

        if (level > 3) // lv 4 이상부터 방어력도 증가
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

        //if(특정 웨이브 조건 달성){
        // EnemyLevelUp(현재레벨) //UI 요소 필요
        //}
    }

}
