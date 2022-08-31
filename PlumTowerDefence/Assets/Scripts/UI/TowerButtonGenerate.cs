using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerButtonGenerate : MonoBehaviour
{
    /// <summary>
    /// 타워버튼 생성을 관리할 스크립트
    /// </summary>
    /// 

    GameObject SelectedTower;

    private int tower_num = 12; //데이터베이스에서 받아야 함

    Ray ray;

    RaycastHit[] hits;

    private void Awake()
    {
        for (int i = 0; i < tower_num; i++)
        {
            GameObject obj = ObjectPools.Instance.GetPooledObject("TowerButton");
            obj.transform.SetParent(transform);
            obj.transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    public void OnBuildTowerBtnClick()
    {
        if(SelectedTower != null)
        {
            return;
        }
        StartCoroutine(IE_FallowingMouse());
    }

    IEnumerator IE_FallowingMouse()
    {
        WaitForFixedUpdate wf = new WaitForFixedUpdate();

        Map.Instance.ShowAllGridLine();

        yield return wf;

        SelectedTower = ObjectPools.Instance.GetPooledObject("Disabled_ArrowTower");

        SelectedTower.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);

        while (true)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            hits = Physics.RaycastAll(ray, 1000);

            foreach (var hit in hits)
            {
                if (hit.collider.CompareTag("Tile"))
                {
                    SelectedTower.transform.position = hit.collider.transform.position;

                    break;
                }
                else if (hit.collider.CompareTag("Ground"))
                {
                    SelectedTower.transform.position = hit.point;
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

                    if (tile.TileType == ETileType.Land && tile.ObjectOnTile == null)
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

            SelectedTower = ObjectPools.Instance.GetPooledObject("ArrowTower");

            SelectedTower.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);

            SelectedTower.transform.position = tile.transform.position;

            tile.ObjectOnTile = SelectedTower;

            SelectedTower = null;
        }
    }
}
