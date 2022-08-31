using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : Enemy
{
    public void GetStat()
    {
        BaseHP = Tables.Monster.Get(4)._Hp;
        BaseShield = Tables.Monster.Get(4)._Sheild;
        BaseArmor = Tables.Monster.Get(4)._Armor;
        BaseSpeed = Tables.Monster.Get(4)._Speed;
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
