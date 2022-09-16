using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSzieController : MonoBehaviour
{
    [SerializeField] GameObject MarkSizePrefab;                                               // Size 표시 오브젝트
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
        float RealRange = Range * 5 * GameManager.instance.unitTileSize / Size;

        float RealSize = 5 * GameManager.instance.unitTileSize;


        if(MarkRangePrefab != null)
        {
            MarkRangePrefab.transform.localScale = new Vector3(RealRange, 0.05f, RealRange);
        }
        
        if(MarkSelectedPrefab != null)
        {
            MarkSelectedPrefab.transform.localScale = new Vector3(RealSize, 0.05f, RealSize);
        }

        if(MarkSizePrefab != null)
        {
            MarkSizePrefab.transform.localScale = new Vector3(RealSize, 0.05f, RealSize);
        }
    }
}
