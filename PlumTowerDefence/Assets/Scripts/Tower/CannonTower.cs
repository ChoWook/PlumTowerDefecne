using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonTower : Tower
{
    private void Awake()
    {
        Setstat(ETowerName.Cannon);
    }
}
