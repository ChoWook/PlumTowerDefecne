using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerUpgradeAmount : MonoBehaviour
{
    public static TowerUpgradeAmount instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    #region ArrowTower

    public ArrowTowerStat _ArrowTowerStat = new ArrowTowerStat();

    public class ArrowTowerStat
    {
        public float attackDamage = 0f;
        public float attackSpeed = 0f;

        public bool isPoison = false;
        public float poisonDamageRate = 0f;
        public float poisonDuration = 0f;

        public bool isBurn = false;
        public float burnDamageRate = 0f;
        public float burnDuration = 0f;

        public float BurnDuration
        {
            get => burnDuration;
            set => burnDuration = value;
        }

        public float BurnDamageRate
        {
            get => burnDamageRate;
            set => burnDamageRate = value;
        }

        public bool IsBurn
        {
            get => isBurn;
            set => isBurn = value;
        }

        public float PoisonDuration
        {
            get => poisonDuration;
            set => poisonDuration = value;
        }

        public float PoisonDamageRate
        {
            get => poisonDamageRate;
            set => poisonDamageRate = value;
        }

        public bool IsPoison
        {
            get => isPoison;
            set => isPoison = value;
        }

        public float AttackSpeed
        {
            get => attackSpeed;
            set => attackSpeed = value;
        }

        public float AttackDamage
        {
            get => attackDamage;
            set => attackDamage = value;
        }
    }

    #endregion

    #region HourglassTower

    public HourglassTowerStat _HourglassTowerStat = new HourglassTowerStat();

    public class HourglassTowerStat
    {
        public float slowRate = 0f;
    }

    #endregion

    #region PoisonTower

    public PoisonTowerStat _PoisonTowerStat = new PoisonTowerStat();

    public class PoisonTowerStat
    {
        public float attackRange = 0f;
        public float abilityDamage = 0f;
        public float slowRate = 0f;

        public float additionalAbilityDamage = 0f;
    }

    #endregion

    #region FlameTower

    public FlameTowerStat _FlameTowerStat = new FlameTowerStat();

    public class FlameTowerStat
    {
        public float attackDamage = 0f;
        public float attackRange = 0f;
        public float slowRate = 0f;
        public float centerAngle = 60f;
        
        public float additionalAttackSpeed = 0f;
    }

    #endregion

    #region AttackBuffTower

    public AttackBuffTowerStat _AttackBuffTowerStat = new AttackBuffTowerStat();

    public class AttackBuffTowerStat
    {
        public float attackRange = 0f;
        public float abilityDamage = 0f;

        public float additionalAbilityDamage = 0f;
        public float additionalAttackRange = 0f;
    }

    #endregion

    #region SpeedBuffTower

    public SpeedBuffTowerStat _SpeedBuffTowerStat = new SpeedBuffTowerStat();

    public class SpeedBuffTowerStat
    {
        public float attackRange = 0f;
        public float abilityDamage = 0f;
        
        public float additionalAbilityDamage = 0f;
        public float additionalAttackRange = 0f;
    }

    #endregion

    #region LaserTower

    public LaserTowerStat _LaserTowerStat = new LaserTowerStat();

    public class LaserTowerStat
    {
        public float attackSpeed = 0f;
        public float attackDamage = 0f;
    }

    #endregion

    #region MissileTower

    public MissileTowerStat _MissileTowerStat = new MissileTowerStat();

    public class MissileTowerStat
    {
        public float attackDamage = 0f;
        public float attackSpeed = 0f;

        public float additionalAttackSpeed = 0f;
        public float additionalAttackDamage = 0f;
    }

    #endregion

    #region ElectricTower

    public ElectricTowerStat _ElectricTowerStat = new ElectricTowerStat();

    public class ElectricTowerStat
    {
        public float attackDamage = 0f;
        public float abilityDamage = 0f;
        public float slowRate = 0f;

        public float additionalAttackDmage = 0f;
        public float additionalAbilityDamage = 0f;
    }

    #endregion

    #region Wall

    public WallStat _WallStat = new WallStat();

    public class WallStat
    {
        public float hp = 0f;
    }

    #endregion

    #region Bomb

    public BombStat _BombStat = new BombStat();

    public class BombStat
    {
        public float attackDamage = 0f;
        public int range = 3;
    }

    #endregion

    #region GatlingTower

    public GatlingTowerStat _GatlingTowerStat = new GatlingTowerStat();

    public class GatlingTowerStat
    {
        public float attackSpeed = 0f;
        public float attackDamage = 0f;

        public float additionalAttackSpeed = 0f;
        public float additionalAttackDamage = 0f;
    }

    #endregion

    #region CannonTower

    public CannonTowerStat _CannonTowerStat = new CannonTowerStat();

    public class CannonTowerStat
    {
        public float attackDamage = 0f;
        public float attackSpeed = 0f;

        public float additionalAttackDamage = 0f;
        public float additionalAttackSpeed = 0f;
    }

    #endregion

    #region Magnetite

    public MagnetiteStat _MagnetiteStat = new MagnetiteStat();

    public class MagnetiteStat
    {
        public float moneyRate = 0f;
    }

    #endregion

    #region Crystal

    public CrystalStat _CrystalStat = new CrystalStat();

    public class CrystalStat
    {
        public float moneyRate = 0f;
    }

    #endregion

    #region Gold

    public GoldStat _GoldStat = new GoldStat();

    public class GoldStat
    {
        public float moneyRate = 0f;
    }

    #endregion

    #region Diamond

    public DiamondStat _DiamondStat = new DiamondStat();

    public class DiamondStat
    {
        public float moneyRate = 0f;
    }

    #endregion
}

