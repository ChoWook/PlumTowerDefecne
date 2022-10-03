using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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

        public List<float> PoisonAttackMultiModifier = new List<float>();
        public List<float> PoisonDurationPlusModifier = new List<float>();

        public List<float> BurnAttackMultiModifier = new List<float>();
        public List<float> BurnDurationPlusModifier = new List<float>();

        public void ResetList()
        {
            AttackPlusModifier = new List<float>();
            SpeedPlusModifier = new List<float>();
            
            PoisonAttackMultiModifier = new List<float>();
            PoisonDurationPlusModifier = new List<float>();
            
            BurnAttackMultiModifier = new List<float>();
            BurnDurationPlusModifier = new List<float>();
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
        public List<float> RangePlusModifier = new List<float>();
        public List<float> SlowMultiModifier = new List<float>();

        public void ResetList()
        {
            AbilityPlusModifier = new List<float>();
            RangePlusModifier = new List<float>();
            SlowMultiModifier = new List<float>();
        }
    }

    #endregion

    #region FlameTower

    public FlameTowerStat _FlameTowerStat = new FlameTowerStat();

    [System.Serializable]
    public class FlameTowerStat
    {
        public List<float> AttackPlusModifier = new List<float>();
        public List<float> SpeedPlusModifier = new List<float>();
        public List<float> RangePlusModifier = new List<float>();
        public List<float> SlowMultiModifier = new List<float>();
        public List<float> AngleplusModifier = new List<float>();

        public void ResetList()
        {
            AttackPlusModifier = new List<float>();
            SpeedPlusModifier = new List<float>();
            RangePlusModifier = new List<float>();
            SlowMultiModifier = new List<float>();
            AngleplusModifier = new List<float>();
        }
    }

    #endregion

    #region AttackBuffTower

    public AttackBuffTowerStat _AttackBuffTowerStat = new AttackBuffTowerStat();

    public class AttackBuffTowerStat
    {
        public List<float> AbilityPlusModifier = new List<float>();
        public List<float> RangePlusModifier = new List<float>();

        public void ResetList()
        {
            AbilityPlusModifier = new List<float>();
            RangePlusModifier = new List<float>();
        }
    }

    #endregion

    #region SpeedBuffTower

    public SpeedBuffTowerStat _SpeedBuffTowerStat = new SpeedBuffTowerStat();

    public class SpeedBuffTowerStat
    {
        public List<float> AbilityPlusModifier = new List<float>();
        public List<float> RangePlusModifier = new List<float>();

        public void ResetList()
        {
            AbilityPlusModifier = new List<float>();
            RangePlusModifier = new List<float>();
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
        public List<float> SlowMultiModifier = new List<float>();

        public void ResetList()
        {
            AttackPlusModifier = new List<float>();
            AbilityPlusModifier = new List<float>();
            SlowMultiModifier = new List<float>();
        }
    }

    #endregion

    #region Wall

    public WallStat _WallStat = new WallStat();

    public class WallStat
    {
        public List<float> HpPlusModifier = new List<float>();

        public void ResetList()
        {
            HpPlusModifier = new List<float>();
        }
    }

    #endregion

    #region Bomb

    public BombStat _BombStat = new BombStat();

    public class BombStat
    {
        public List<float> AttackPlusModifier = new List<float>();
        public List<float> RangePlusModifier = new List<float>();

        public void ResetList()
        {
            AttackPlusModifier = new List<float>();
            RangePlusModifier = new List<float>();
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

