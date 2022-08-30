using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : Enemy
{

    void GetStat()
    {
        BaseHP = Tables.Monster.Get(1)._Hp;
        BaseShield = Tables.Monster.Get(1)._Sheild;
        BaseArmor = Tables.Monster.Get(1)._Armor;
        BaseSpeed = Tables.Monster.Get(1)._Speed;
        Debug.Log(BaseHP + " " + BaseShield + " " + BaseArmor + " " + BaseSpeed);

    }

    private void OnEnable()
    {
        Speed = 15;
       //GetStat();
       // SetStat();

    }
    private void Awake()
    {
        
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
