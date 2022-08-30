using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pirate : Enemy
{
    void GetStat()
    {
        BaseHP = Tables.Monster.Get(6)._Hp;
        BaseShield = Tables.Monster.Get(6)._Sheild;
        BaseArmor = Tables.Monster.Get(6)._Armor;
        BaseSpeed = Tables.Monster.Get(6)._Speed;
        Debug.Log(BaseHP + " " + BaseShield + " " + BaseArmor + " " + BaseSpeed);

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
