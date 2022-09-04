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

    private int tower_num = 11; //데이터베이스에서 받아야 함
    private int tower_row = 7;

    public int fontSize = 16;

    Ray ray;

    RaycastHit[] hits;

    ETowerName SelectedTowerName = ETowerName.Arrow;

    private void Awake()
    {
        for (ETowerName TName = ETowerName.Arrow; TName <= ETowerName.Bomb; TName++)
        {
            GameObject obj = ObjectPools.Instance.GetPooledObject("TowerButton");
            TowerBtnItem item = obj.GetComponent<TowerBtnItem>();
            
            item.SetTowerName(TName);

            if ((int)TName <= tower_row)
            {
                obj.transform.SetParent(transform.GetChild(0));
            }
            else
            {
                obj.transform.SetParent(transform.GetChild(1));
            }
            obj.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);

            ETowerName tmp = TName;
            obj.GetComponent<Button>().onClick.AddListener(() => OnBuildTowerBtnClick(tmp));
        }
    }

    public void OnBuildTowerBtnClick(ETowerName TName)
    {
        if(SelectedTower != null)
        {
            return;
        }


        // 돈이 적으면 리턴
        //if(GameManager.instance.money < .price)
        //{
        //    return;
        //}

        SelectedTower = ObjectPools.Instance.GetPooledObject($"Disabled_{TName}Tower");

        if (SelectedTower == null)
        {
            // 구현이 안된 프리팹에 대해서는 리턴
            return ;
        }

        SelectedTowerName = TName;

        StartCoroutine(IE_FallowingMouse());
    }

    IEnumerator IE_FallowingMouse()
    {
        WaitForFixedUpdate wf = new WaitForFixedUpdate();

        Map.Instance.ShowAllGridLine();

        yield return wf;

        SelectedTower.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);

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
                    SelectedTower.transform.position = tile.transform.position;

                    // 타워를 짓지 못하는 곳은 오브젝트가 빨간색으로 변해야 함
                    if (tile.CheckTileType(ETileType.Land) && tile.GetObjectOnTile() == null)
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

                    // TODO 채굴 타워는 tile.ObjectOnTile이 자원이어야 함

                    if (tile.CheckTileType(ETileType.Land) && tile.GetObjectOnTile() == null)
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

            SelectedTower.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);

            SelectedTower.transform.position = tile.transform.position;

            SelectedTower.GetComponent<Tower>().belowTile = tile;

            // TODO 타워의 사이즈가 매개변수로 들어가야 함
            tile.SetObjectOnTile(SelectedTower);

            SelectedTower = null;
        }
    }

    public void ChangeSelectedTowerMaterial(bool Available)
    {
        if (SelectedTower != null && SelectedTower.activeSelf)
        {
            SelectedTowerAvailable?.SetActive(Available);

            SelectedTowerDisabled?.SetActive(!Available);
        }
    }
}
