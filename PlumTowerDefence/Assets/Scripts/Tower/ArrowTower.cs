using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ArrowTower : Tower
{



    private void Awake()
    {
        Setstat(ETowerName.Arrow);
    }

    public override float AttackStat
    {
        get
        {
            float sum = 0f;

            List<float> plus = TowerUpgradeAmount.instance._ArrowTowerStat.AttackPlusModifier;


            for (int i = 0; i < plus.Count; i++)
            {
                sum += plus[i];
            }
            
            for(int i=0; i<AttackPlusModifier.Count; i++)
            {
                sum += AttackPlusModifier[i];
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
            List<float> plus = TowerUpgradeAmount.instance._ArrowTowerStat?.SpeedPlusModifier;

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


    // 독 뎀 넣어주기


    // 화상 데미지 넣어주기
}