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
        CreateBtn();
    }

    void CreateBtn()
    {
        for (ETowerName TName = ETowerName.Arrow; TName <= ETowerName.Bomb; TName++)
        {
            // enum에는 있고 csv에 없는 타워는 버튼 생성 X
            if (Tables.Tower.Get(TName) == null)
            {
                Debug.Log("CSV is not contain key : " + TName.ToString());
                continue;
            }

            // csv에는 있지만 프리팹 구현이 안되 오브젝트풀에 존재하지 않는 타워는 버튼 생성X
            var _t = ObjectPools.Instance.GetPooledObject($"Disabled_{TName}Tower");
            if (_t == null)
            {
                Debug.Log("Prefabs is not contain key : " + TName.ToString());
                continue;
            }
            ObjectPools.Instance.ReleaseObjectToPool(_t);

            GameObject obj = ObjectPools.Instance.GetPooledObject("TowerButton");

            TowerBtnItem item = obj.GetComponent<TowerBtnItem>();

            GameManager.instance.InitCoupon();

            item.SetTowerName(TName);
            item.SetTowerImage();

            obj.transform.SetParent(transform.GetChild(0));
            obj.transform.localScale = new Vector3(1f, 1f, 1f);


            // 버튼 클릭 리스너 설정
            Button btn = obj.GetComponent<Button>();

            ETowerName tmp = TName;                                         // Ref 값으로 들어가는걸 막기 위한 tmp 변수 생성

            btn.onClick.AddListener(() => OnBuildTowerBtnClick(tmp));

            GameManager.instance.AddMoneyChangeCallBack(() =>
            {
                // 구매할 수 없는 버튼일 때
                if (Tables.Tower.Get(item._Name)._Price > GameManager.instance.money && !GameManager.instance.HasCoupon(tmp))
                {
                    btn.interactable = false;
                }
                else
                {
                    btn.interactable = true;
                }
            });
        }
    }

    public void OnBuildTowerBtnClick(ETowerName TName)
    {
        // 해당 버튼의 타워 가격보다 돈이 적고 쿠폰도 없으면 리턴
        if(Tables.Tower.Get(TName)._Price > GameManager.instance.money && !GameManager.instance.HasCoupon(TName))
        {
            return;
        }


        // 이미 선택한 타워가 있다면 바꿔야 함
        if(SelectedTower != null)
        {
            ObjectPools.Instance.ReleaseObjectToPool(SelectedTower);

            SelectedTower = null;
        }

        UIManager.instance?.UIClear();

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
                if (SelectedTower == null)
                {
                    continue;
                }

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
                    // 공격로에 설치하는 애들
                    if(TName == ETowerName.Bomb || TName == ETowerName.Wall)
                    {
                        ChangeSelectedTowerMaterial(tile.CheckObjectOnAttackRoute());
                    }
                    // 평지에 설치하는 애들
                    else
                    {
                        ChangeSelectedTowerMaterial(tile.CheckObjectOnLandWithSize(TowerSize));
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
                    // 공격로에 설치하는 애들이면
                    if (SelectedTowerName == ETowerName.Bomb || SelectedTowerName == ETowerName.Wall)
                    {
                        IsAvailableTile = tile.CheckObjectOnAttackRoute();
                    }
                    // 평지에 설치하는 애들이면
                    else
                    {
                        IsAvailableTile = tile.CheckObjectOnLandWithSize(TowerSize);
                    }
                    break;
                }
            }
            
            // 타워가 설치 가능한 타일이 아니면 리턴
            if(IsAvailableTile == false)
            {
                return;
            }

            // 돈도 부족하고 쿠폰도 없으면 리턴
            if(Tables.Tower.Get(SelectedTowerName)._Price > GameManager.instance.money && !GameManager.instance.HasCoupon(SelectedTowerName))
            {
                return;
            }


            // 돈보다 쿠폰 먼저 사용
            if (GameManager.instance.HasCoupon(SelectedTowerName))
            {
                GameManager.instance.RemoveCoupon(SelectedTowerName);
            }
            else
            {
                GameManager.instance.money -= Tables.Tower.Get(SelectedTowerName)._Price;
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

            // 타워 사거리 표시 안하기
            t.IsSelected(false);

            // 타워의 사이즈가 매개변수로 들어가야 함
            tile.SetObjectOnTile(SelectedTower.gameObject, TowerSize);

            SelectedTower = null;
        }

        // 오른쪽 마우스 클릭 시 타워 설치 취소
        if (Input.GetMouseButtonUp(1))
        {
            if(SelectedTower == null)
            {
                return;
            }

            Map.Instance.HideAllGridLine();

            StopAllCoroutines();

            ObjectPools.Instance.ReleaseObjectToPool(SelectedTower);
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
