using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ElectricTower : Tower
{
    private void Awake()
    {
        Setstat(ETowerName.Electric);
    }

    
    public override float AttackStat
    {
        get
        {
            List<float> list = TowerUpgradeAmount.instance._ElectricTowerStat.AttackPlusModifier;

            float sum = 0f;

            for (int i = 0; i < list.Count; i++)
            {
                sum += list[i];
            }

            float multi = 1f;

            for (int i = 0; i < AttackMultiModifier.Count; i++)
            {
                multi += AttackMultiModifier[i];
            }

            for (int i = 0; i < AttackBuffTowers.Count; i++)
            {
                sum += AttackBuffTowers.ElementAt(i).Value;
            }


            return (BaseAttackStat + sum) * multi;
        } 
    }
    public override float AbilityStat
    {
        get
        {
            List<float> list = TowerUpgradeAmount.instance._ElectricTowerStat.AbilityPlusModifier;

            float sum = 0f;

            for (int i = 0; i < list.Count; i++)
            {
                sum += list[i];
            }

            float multi = 1f;

            for (int i = 0; i < AbilityMultiModifier.Count; i++)
            {
                multi += AbilityMultiModifier[i];
            }


            return (BaseAbilityStat + sum) * multi;
        }
    }
    

    // slowrate


    /*
    public override void Shoot()
    {
        // bulletprefab 정하면 생성

    }*/
}
