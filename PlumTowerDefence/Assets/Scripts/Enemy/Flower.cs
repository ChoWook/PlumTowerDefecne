using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : Enemy
{
    void GetStat()
    {
        BaseHP = Tables.Monster.Get(3)._Hp;
        BaseShield = Tables.Monster.Get(3)._Sheild;
        BaseArmor = Tables.Monster.Get(3)._Armor;
        BaseSpeed = Tables.Monster.Get(3)._Speed;
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
