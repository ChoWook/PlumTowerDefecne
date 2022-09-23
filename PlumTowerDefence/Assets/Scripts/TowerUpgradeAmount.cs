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
        public List<float> AttackPlusModifier = new List<float>();
        public List<float> SpeedPlusModifier = new List<float>();

        public bool isPoison = false;
        public float poisonDamageRate = 0f;
        public float poisonDuration = 0f;

        public bool isBurn = false;
        public float burnDamageRate = 0f;
        public float burnDuration = 0f;

        public void ResetList()
        {
            AttackPlusModifier = new List<float>();
            SpeedPlusModifier = new List<float>();
        }
    }

    #endregion

    #region HourglassTower

    public HourglassTowerStat _HourglassTowerStat = new HourglassTowerStat();

    public class HourglassTowerStat
    {
        public List<float> AbilityMultiModifier = new List<float>();

        public void ResetList()
        {
            AbilityMultiModifier = new List<float>();
        }
    }

    #endregion

    #region PoisonTower

    public PoisonTowerStat _PoisonTowerStat = new PoisonTowerStat();

    public class PoisonTowerStat
    {
        public List<float> AbilityPlusModifier = new List<float>();
        
        public float attackRange = 0f;
        public float slowRate = 0f;

        public void ResetList()
        {
            AbilityPlusModifier = new List<float>();
        }
    }

    #endregion

    #region FlameTower

    public FlameTowerStat _FlameTowerStat = new FlameTowerStat();

    public class FlameTowerStat
    {
        public List<float> AttackPlusModifier = new List<float>();
        public List<float> SpeedPlusModifier = new List<float>();
        
        public float attackRange = 0f;
        public float slowRate = 0f;
        public float centerAngle = 60f;

        public void ResetList()
        {
            AttackPlusModifier = new List<float>();
            SpeedPlusModifier = new List<float>();
        }
    }

    #endregion

    #region AttackBuffTower

    public AttackBuffTowerStat _AttackBuffTowerStat = new AttackBuffTowerStat();

    public class AttackBuffTowerStat
    {
        public List<float> AbilityPlusModifier = new List<float>();
        
        public float attackRange = 0f;

        public float additionalAttackRange = 0f;

        public void ResetList()
        {
            AbilityPlusModifier = new List<float>();
        }
    }

    #endregion

    #region SpeedBuffTower

    public SpeedBuffTowerStat _SpeedBuffTowerStat = new SpeedBuffTowerStat();

    public class SpeedBuffTowerStat
    {
        public List<float> AbilityPlusModifier = new List<float>();
        
        public float attackRange = 0f;
        
        public float additionalAttackRange = 0f;

        public void ResetList()
        {
            AbilityPlusModifier = new List<float>();
        }
    }

    #endregion

    #region LaserTower

    public LaserTowerStat _LaserTowerStat = new LaserTowerStat();

    public class LaserTowerStat
    {
        public List<float> AttackPlusModifier = new List<float>();
        public List<float> SpeedPlusModifier = new List<float>();

        public void ResetList()
        {
            AttackPlusModifier = new List<float>();
            SpeedPlusModifier = new List<float>();
        }
    }

    #endregion

    #region MissileTower

    public MissileTowerStat _MissileTowerStat = new MissileTowerStat();

    public class MissileTowerStat
    {
        public List<float> AttackPlusModifier = new List<float>();
        public List<float> SpeedPlusModifier = new List<float>();

        public void ResetList()
        {
            AttackPlusModifier = new List<float>();
            SpeedPlusModifier = new List<float>();
        }
    }

    #endregion

    #region ElectricTower

    public ElectricTowerStat _ElectricTowerStat = new ElectricTowerStat();

    public class ElectricTowerStat
    {
        public List<float> AttackPlusModifier = new List<float>();
        public List<float> AbilityPlusModifier = new List<float>();
        
        public float slowRate = 0f;

        public void ResetList()
        {
            AttackPlusModifier = new List<float>();
            AbilityPlusModifier = new List<float>();
        }
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
        public List<float> AttackPlusModifier = new List<float>();
        public int range = 3;

        public void ResetList()
        {
            AttackPlusModifier = new List<float>();
        }
    }

    #endregion

    #region GatlingTower

    public GatlingTowerStat _GatlingTowerStat = new GatlingTowerStat();

    public class GatlingTowerStat
    {
        public List<float> AttackPlusModifier = new List<float>();
        public List<float> SpeedPlusModifier = new List<float>();

        public void ResetList()
        {
            AttackPlusModifier = new List<float>();
            SpeedPlusModifier = new List<float>();
        }
    }

    #endregion

    #region CannonTower

    public CannonTowerStat _CannonTowerStat = new CannonTowerStat();

    public class CannonTowerStat
    {
        public List<float> AttackPlusModifier = new List<float>();
        public List<float> SpeedPlusModifier = new List<float>();

        public void ResetList()
        {
            AttackPlusModifier = new List<float>();
            SpeedPlusModifier = new List<float>();
        }
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

