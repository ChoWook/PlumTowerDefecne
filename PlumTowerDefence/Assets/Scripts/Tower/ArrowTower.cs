using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            List<float> plus = TowerUpgradeAmount.instance._ArrowTowerStat.AttackPlusModifier;

            float sum = 0f;

            for (int i = 0; i <plus.Count; i++)
            {
                sum += plus[i];
            }

            return (BaseAttackStat + sum + AttackBuffAmount);

        }
        
    }

    public override float SpeedStat
    {
        get
        {
            List<float> plus = TowerUpgradeAmount.instance._ArrowTowerStat.SpeedPlusModifier;

            float sum = 0f;

            for (int i = 0; i < plus.Count; i++)
            {
                sum += plus[i];
            }

            return (BaseSpeedStat + sum + SpeedBuffAmount);

        }
    }

    // ���� ������ ����?

    // �� �� �־��ֱ�


    // ȭ�� ������ �־��ֱ�
}
