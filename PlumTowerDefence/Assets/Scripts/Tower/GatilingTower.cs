using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GatilingTower : Tower
{
    private void Awake()
    {
        Setstat(ETowerName.Gatling);
    }

    
    public override float AttackStat
    {
        get
        {
            List <float> plus = TowerUpgradeAmount.instance._GatlingTowerStat.AttackPlusModifier;

            float sum = 0f;

            for (int i = 0; i < plus.Count; i++)
            {
                sum += plus[i];
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
            List<float> plus = TowerUpgradeAmount.instance._GatlingTowerStat?.SpeedPlusModifier;

            float sum = 0f;

            for (int i = 0; i < plus.Count; i++)
            {
                sum += plus[i];
            }

            for (int i = 0; i < SpeedBuffTowers.Count; i++)
            {
                sum += SpeedBuffTowers.ElementAt(i).Value;
            }

            return (BaseSpeedStat + sum);

        }
    }

    

}
