using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject towerPrefab; // todo list : TileWall 추가하기 

    public void SpawnTower(Transform tileTransform)
    {
        // 선택한 타일의 위치에 타워 건설
        Instantiate(towerPrefab, tileTransform.position, Quaternion.identity);
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
