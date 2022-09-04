using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonTower : Tower
{
    private void Awake()
    {
        Setstat(ETowerName.Poison);
    }

    public override void Shoot()
    {
        
    }
}
