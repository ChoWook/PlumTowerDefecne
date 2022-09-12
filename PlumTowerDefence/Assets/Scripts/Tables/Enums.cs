using System.Collections;
using System.Collections.Generic;
using UnityEngine;


#region Map
public enum ETileType
{
    Land = 1,
    AttackRoute = 2,
    House = 3,
}

public enum EGroundType
{
    TR = 1,
    TL = 2,
    TU = 3,
    TD = 4,
    UR = 5,
    RD = 6,
    DL = 7,
    UL = 8,
    UD = 9,
    LR = 10,
    RDL = 11,
    UDL = 12,
    URL = 13,
    URD = 14,
}

public enum EMapGimmickType
{
    LaneBuff = 1,
    Obstacle = 2,
    Resource = 3,
    Treasure = 4,
}

public enum EResourceType
{
    Magnetite = 1,
    Crystal = 2,
    Gold = 3,
    Diamond = 4,
}
public enum EPickaxeType
{
    Wood = 1,
    Blue = 2,
    Red = 3,
    Black = 4,
}

#endregion

#region Monster
public enum EMonsterType
{
    Bet = 1,
    Mushroom = 2,
    Flower = 3,
    Fish = 4,
    Slime = 5,
    Pirate = 6,
    Spider = 7,
    Bear = 8,
}

public enum EMonsterClass
{
    Normal = 1,
    SubBoss = 2,
    Boss = 3,
}

public enum EMonsterStat
{
    NULL = 1,
    Hp = 2,
    Armor = 3,
    Shield = 4,
    Speed = 5,
    Damage = 6,
}

public enum EPropertyType
{
    None = 1,
    Reinforced = 2,
    Divisive = 3,
    Divided = 4,
    Private = 5,
    Resurrect = 6,
    Resurrected = 7,
    Generative = 8,
    Leading = 9,
    Cursing = 10,

}

public enum ESpecialityType
{
    None = 1,
    Big = 2,
    Hug = 3,
    Sturdy = 4,
    Hard = 5,
    Abudant = 6,
    Fast = 7,
    Rapid = 8,
}

public enum EElementalType
{
    None = 0,
    Water = 1,
    Ground = 2,
    Fire = 3,
    Electric = 4,
}

public enum ELaneBuffType
{
    AllHealHp = 1,
    AllDealHp = 2,
    AllHealShield = 3,
    AllDealShield = 4,
    AllBuffArmor = 5,
    AllNurfArmor = 6,
    WaterHealHp = 7,
    GroundHealHp = 8,
    FireHealHp = 9,
    ElectricHealHp = 10,
    WaterDealHp = 11,
    GroundDealHp = 12,
    FireDealHp = 13,
    ElectricDealHp = 14,
    WaterBuffArmor = 15,
    GroundBuffArmor = 16,
    FireBuffArmor = 17,
    ElectricBuffArmor = 18,
    WaterNurfArmor = 19,
    GroundNurfArmor = 20,
    FireNurfArmor = 21,
    ElectricNurfArmor = 22,
    ArrowBuff = 23,
    SlowBuff = 24,
    PoisonBuff = 25,
    FlameBuff = 26,
    LazerBuff = 27,
    MissileBuff = 28,
}

#endregion

#region Tower
public enum ETowerType
{
    Attack = 1,
    AttackProjectile = 2,
    Buff = 3,
    Debuff = 4,
    Mining = 5,
    Lane = 6,
}

public enum EAttackSpecialization
{
    Default = 1,
    Health = 2,
    Shield = 3,
    Defense = 4,
}

public enum EUpgradeStat
{
    Attack = 1,
    Ability = 2,
    Speed = 3,
}
public enum ETowerName
{
    Arrow = 1,
    Hourglass = 2,
    Poison = 3,
    Flame = 4,
    AttackBuff = 5,
    SpeedBuff = 6,
    Laser = 7,
    Missile = 8,
    Electric = 9,
    Gatling = 10,
    Cannon = 11,
    Wall = 12,
    Bomb = 13,
}

public enum EInteraction
{
    Targeting = 1,
    Upgrade = 2,
    Move = 3,
    Sell = 4,
    Health_P = 5,
    Shield_P = 6,
    Defense_P = 7,

}

#endregion

#region UI

public enum ECategoryType
{
    Tower = 1,
    Resource = 2,
    Passive = 3,
}

#endregion
