using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerUpgradeAmount : ScriptableObject
{
    #region ArrowTower

    public static class ArrowTowerStat
    {
        public static float attackDamage = 0f;
        public static float attackSpeed = 0f;

        public static bool isPoison = false;
        public static float poisonDamageRate = 0f;
        public static float poisonDuration = 0f;

        public static bool isBurn = false;
        public static float burnDamageRate = 0f;
        public static float burnDuration = 0f;
    }

    #endregion

    #region HourglassTower

    public static class HourglassTowerStat
    {
        public static float slowRate = 0f;
    }

    #endregion

    #region PoisonTower

    public static class PoisonTowerStat
    {
        public static float attackRange = 0f;
        public static float abilityDamage = 0f;
        public static float slowRate = 0f;

        public static float additionalAbilityDamage = 0f;
    }

    #endregion

    #region FlameTower

    public static class FlameTowerStat
    {
        public static float attackDamage = 0f;
        public static float attackRange = 0f;
        public static float slowRate = 0f;
        public static float centerAngle = 60f;
        
        public static float additionalAttackSpeed = 0f;
    }

    #endregion

    #region AttackBuffTower

    public static class AttackBuffTowerStat
    {
        public static float attackRange = 0f;
        public static float abilityDamage = 0f;

        public static float additionalAbilityDamage = 0f;
        public static float additionalAttackRange = 0f;
    }

    #endregion

    #region SpeedBuffTower

    public static class SpeedBuffTowerStat
    {
        public static float attackRange = 0f;
        public static float abilityDamage = 0f;
        
        public static float additionalAbilityDamage = 0f;
        public static float additionalAttackRange = 0f;
    }

    #endregion

    #region LaserTower

    public static class LaserTowerStat
    {
        public static float attackSpeed = 0f;
        public static float attackDamage = 0f;
    }

    #endregion

    #region MissileTower

    public static class MissileTowerStat
    {
        public static float attackDamage = 0f;
        public static float attackSpeed = 0f;

        public static float additionalAttackSpeed = 0f;
        public static float additionalAttackDamage = 0f;
    }

    #endregion

    #region ElectricTower

    public static class ElectricTowerStat
    {
        public static float attackDamage = 0f;
        public static float abilityDamage = 0f;
        public static float slowRate = 0f;

        public static float additionalAttackDmage = 0f;
        public static float additionalAbilityDamage = 0f;
    }

    #endregion

    #region Wall

    public static class WallStat
    {
        public static float hp = 0f;
    }

    #endregion

    #region Bomb

    public static class BombStat
    {
        public static float attackDamage = 0f;
        public static int range = 3;
    }

    #endregion

    #region GatlingTower

    public static class GatlingTowerStat
    {
        public static float attackSpeed = 0f;
        public static float attackDamage = 0f;

        public static float additionalAttackSpeed = 0f;
        public static float additionalAttackDamage = 0f;
    }

    #endregion

    #region CannonTower

    public static class CannonTowerStat
    {
        public static float attackDamage = 0f;
        public static float attackSpeed = 0f;

        public static float additionalAttackDamage = 0f;
        public static float additionalAttackSpeed = 0f;
    }

    #endregion

    #region Magnetite

    public static class MagnetiteStat
    {
        public static float moneyRate = 0f;
    }

    #endregion

    #region Crystal

    public static class CrystalStat
    {
        public static float moneyRate = 0f;
    }

    #endregion

    #region Gold

    public static class GoldStat
    {
        public static float moneyRate = 0f;
    }

    #endregion

    #region Diamond

    public static class DiamondStat
    {
        public static float moneyRate = 0f;
    }

    #endregion
}

