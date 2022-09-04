using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBuffTower : Tower
{
    private void Awake()
    {
        Setstat(ETowerName.SpeedBuff);
    }

    public override void Shoot()
    {
        
    }
}
