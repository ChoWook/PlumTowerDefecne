using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTower : Tower
{
    public int _TowerId = 1;

    private void Awake()
    {
        Setstat(_TowerId);  
    }


    
}
