using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Tables;

public class TowerSizeController : MonoBehaviour
{
    [SerializeField] GameObject MarkSizePrefab;                                               // Size ǥ�� ������Ʈ
    [SerializeField] GameObject MarkRangePrefab;
    [SerializeField] GameObject MarkSelectedPrefab;

    [SerializeField] ETowerName Name = ETowerName.Arrow;

    int Size = 0;
    float Range = 0;

    private void Awake()
    {
        Tables.Tower tower = Tables.Tower.Get(Name);

        Size = tower._Size;
        Range = tower._Range;
    }

    private void OnEnable()
    {
        UpdateRange();
        
    }

    public void IsSelected(bool sender)
    {

        //��Ÿ�, Ÿ�� ������ ǥ�� Ȱ��ȭ
        if (sender)
        {
            UpdateRange();
        }

        MarkSelectedPrefab?.SetActive(sender);
        MarkRangePrefab?.SetActive(sender);

    }

    private void UpdateRange()
    {
        Range = Tables.Tower.Get(Name)._Range;

        switch (Name)
        {
            case ETowerName.Poison:
                Range += PoisonTower.UpgradeRange;
                break;
            case ETowerName.Flame:
                Range += FlameTower.UpgradeRange;
                break;
            case ETowerName.AttackBuff:
                Range += AttackBuffTower.UpgradeRange;
                break;
            case ETowerName.SpeedBuff:
                Range += SpeedBuffTower.UpgradeRange;
                break;
        }

        float RealRange = Range * 5 * GameManager.instance.UnitTileSize / Size * 2;

        // Debug.Log("Range :" + Range + " / Real")

        float RealSize = 5 * GameManager.instance.UnitTileSize;


        if (MarkRangePrefab != null)
        {
            MarkRangePrefab.transform.localScale = new Vector3(RealRange, 0.05f, RealRange);
        }

        if (MarkSelectedPrefab != null)
        {
            MarkSelectedPrefab.transform.localScale = new Vector3(RealSize, 0.05f, RealSize);
        }

        if (MarkSizePrefab != null)
        {
            MarkSizePrefab.transform.localScale = new Vector3(RealSize, 0.05f, RealSize);
        }
    }
}
