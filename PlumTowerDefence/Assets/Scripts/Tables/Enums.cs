using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EPoolType
{
    Obtacle = 1,
    Resource = 2,

}


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

public enum EMonsterStat
{
    NULL = 1,
    Hp = 2,
    Armor = 3,
    Shield = 4,
    Speed = 5,
    Damage = 6,
}


public enum ECategoryType
{
    Tower = 1,
    Resource = 2,
    Passive = 3,
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
    Mine = 12,
    Wall = 13,
    Bomb = 14,
}