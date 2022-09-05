using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricTower : Tower
{
    private void Awake()
    {
        Setstat(ETowerName.Electric);
    }


    public override void Shoot()
    {
        // bulletprefab 정하면 생성
    }
}
