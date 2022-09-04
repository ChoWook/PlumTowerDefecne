using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerButtonGenerate : MonoBehaviour
{
    /// <summary>
    /// 타워버튼 생성을 관리할 스크립트
    /// </summary>
    /// 

    GameObject SelectedTower;

    GameObject SelectedTowerAvailable;

    GameObject SelectedTowerDisabled;

    private int tower_num = 13; //데이터베이스에서 받아야 함

    public int fontSize = 16;

    int TowerSize;

    Ray ray;

    RaycastHit[] hits;

    ETowerName SelectedTowerName = ETowerName.Arrow;

    private void Awake()
    {
        for (ETowerName TName = ETowerName.Arrow; TName <= ETowerName.Bomb; TName++)
        {
            // enum에는 있고 csv에 없는 타워는 버튼 생성 X
            if (Tables.Tower.Get(TName) == null) continue;

            // csv에는 있지만 프리팹 구현이 안되 오브젝트풀에 존재하지 않는 타워는 버튼 생성X
            var _t = ObjectPools.Instance.GetPooledObject($"Disabled_{TName}Tower");
            if (_t == null)
            {
                continue;
            }
            ObjectPools.Instance.ReleaseObjectToPool(_t);

            GameObject obj = ObjectPools.Instance.GetPooledObject("TowerButton");

            TowerBtnItem item = obj.GetComponent<TowerBtnItem>();
            
            item.SetTowerName(TName);
            
            obj.transform.SetParent(transform.GetChild(0));
            obj.transform.localScale = new Vector3(1f, 1f, 1f);

            ETowerName tmp = TName;
            obj.GetComponent<Button>().onClick.AddListener(() => OnBuildTowerBtnClick(tmp));
        }
    }

    public void OnBuildTowerBtnClick(ETowerName TName)
    {
        if(SelectedTower != null)
        {
            ObjectPools.Instance.ReleaseObjectToPool(SelectedTower);

            SelectedTower = null;
        }


        // 돈이 적으면 리턴
        //if(GameManager.instance.money < .price)
        //{
        //    return;
        //}


        /*
        if (SelectedTower == null)
        {
            // 구현이 안된 프리팹에 대해서는 리턴
            return ;
        }
        */

        SelectedTowerName = TName;

        StartCoroutine(IE_FallowingMouse(TName));
    }

    IEnumerator IE_FallowingMouse(ETowerName TName)
    {
        WaitForFixedUpdate wf = new WaitForFixedUpdate();

        Map.Instance.ShowAllGridLine();

        // 버튼 밑에 타일이 있어서 바로 설치되는 것을 방지
        yield return wf;

        // 타워 크기 조정을 위해 임시로 타워 하나 가져옴

        SelectedTower = ObjectPools.Instance.GetPooledObject($"Disabled_{TName}Tower");

        TowerSize = Tables.Tower.Get(SelectedTowerName)._Size;

        SelectedTower.transform.localScale = new Vector3(0.2f * TowerSize, 0.2f * TowerSize, 0.2f * TowerSize);

        SelectedTowerAvailable = SelectedTower.transform.Find("Available").gameObject;

        SelectedTowerDisabled = SelectedTower.transform.Find("Disabled").gameObject;

        ChangeSelectedTowerMaterial(true);

        while (true)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            hits = Physics.RaycastAll(ray, 1000);

            foreach (var hit in hits)
            {
                if (hit.collider.CompareTag("Tile"))
                {
                    Tile tile = hit.collider.transform.parent.GetComponent<Tile>();

                    // 마우스 따라다니는 오브젝트 위치 고정
                    if (TowerSize == 2)
                    {
                        float half = GameManager.instance.unitTileSize / 2;
                        SelectedTower.transform.position = new Vector3(tile.transform.position.x + half, tile.transform.position.y, tile.transform.position.z - half);
                    }
                    else
                    {
                        SelectedTower.transform.position = tile.transform.position;
                    }

                    // 타워를 짓지 못하는 곳은 오브젝트가 빨간색으로 변해야 함
                    //if(true)
                    if (tile.CheckObjectOnTileWithSize(TowerSize))
                    {
                        ChangeSelectedTowerMaterial(true);
                    }
                    else
                    {
                        ChangeSelectedTowerMaterial(false);
                    }

                    break;
                }
                else if (hit.collider.CompareTag("Ground"))
                {
                    SelectedTower.transform.position = hit.point;

                    ChangeSelectedTowerMaterial(false);
                }
            }

            yield return wf;
        }
        
    }

    private void Update()
    {
        if (hits == null || SelectedTower == null)
        {
            return;
        }

        if (Input.GetMouseButtonUp(0))
        {
            bool IsAvailableTile = false;

            Tile tile = null;

            foreach (var hit in hits)
            {
                if (hit.collider.CompareTag("Tile"))
                {
                    tile = hit.collider.transform.parent.GetComponent<Tile>();

                    //if (tile.CheckTileType(ETileType.Land) && tile.GetObjectOnTile() == null)
                    if(tile.CheckObjectOnTileWithSize(TowerSize))
                    {
                        IsAvailableTile = true;
                    }

                    break;
                }
            }
            
            // 타워가 설치 가능한 타일이 아니면 리턴
            if(IsAvailableTile == false)
            {
                return;
            }

            Map.Instance.HideAllGridLine();

            StopAllCoroutines();

            ObjectPools.Instance.ReleaseObjectToPool(SelectedTower);

            SelectedTower = ObjectPools.Instance.GetPooledObject($"{SelectedTowerName}Tower");

            // 크기가 2일때는 우하향쪽으로 생성되어야 함
            if (TowerSize == 2)
            {
                float half = GameManager.instance.unitTileSize / 2;
                SelectedTower.transform.position = new Vector3(tile.transform.position.x + half, tile.transform.position.y, tile.transform.position.z - half);
            }
            else
            {
                SelectedTower.transform.position = tile.transform.position;
            }

            Tower t = SelectedTower.GetComponent<Tower>();

            t.belowTile = tile;

            SelectedTower.transform.localScale = new Vector3(0.2f * TowerSize, 0.2f * TowerSize, 0.2f * TowerSize);

            // 타워의 사이즈가 매개변수로 들어가야 함
            tile.SetObjectOnTile(SelectedTower.gameObject, TowerSize);

            SelectedTower = null;
        }
    }

    public void ChangeSelectedTowerMaterial(bool Available)
    {
        if (SelectedTower != null && SelectedTower.gameObject.activeSelf)
        {
            SelectedTowerAvailable?.SetActive(Available);

            SelectedTowerDisabled?.SetActive(!Available);
        }
    }
}
