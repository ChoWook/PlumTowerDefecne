using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBuffTower : Tower
{
    private void Awake()
    {
        Setstat(ETowerName.AttackBuff);
    }

    public override void Shoot()
    {
        
    }

}
