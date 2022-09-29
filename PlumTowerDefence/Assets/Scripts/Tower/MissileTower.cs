using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MissileTower : Tower
{
    private void Awake()
    {
        Setstat(ETowerName.Missile);
    }

    
    public override float AttackStat
    {
        get
        {
            List<float> list = TowerUpgradeAmount.instance._MissileTowerStat.AttackPlusModifier;

            float sum = 0f;

            for (int i = 0; i < list.Count; i++)
            {
                sum += list[i];
            }

            for (int i = 0; i < AttackBuffTowers.Count; i++)
            {
                sum += AttackBuffTowers.ElementAt(i).Value;
            }

            return (BaseAttackStat + sum);
        }
    }

    public override float SpeedStat
    {
        get
        {
            List<float> list = TowerUpgradeAmount.instance._MissileTowerStat.SpeedPlusModifier;


            float sum = 0f;

            for (int i = 0; i < list.Count; i++)
            {
                sum += list[i];
            }

            for(int i = 0; i< SpeedPlusModifier.Count; i++)
            {
                sum += SpeedPlusModifier[i];
            }

            for (int i = 0; i < SpeedBuffTowers.Count; i++)
            {
                sum += SpeedBuffTowers.ElementAt(i).Value;
            }

            return (BaseSpeedStat + sum);
        }
    }

    

}
