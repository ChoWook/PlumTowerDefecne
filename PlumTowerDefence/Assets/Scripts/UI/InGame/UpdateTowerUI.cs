using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdateTowerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI TowerName;
    [SerializeField] private TextMeshProUGUI TowerLevel;
    [SerializeField] private TextMeshProUGUI TowerDamage;
    [SerializeField] private TextMeshProUGUI TowerFireRate;
    [SerializeField] private TextMeshProUGUI TowerPriority;
    [SerializeField] private TextMeshProUGUI TowerUpgrade;
    [SerializeField] private TextMeshProUGUI TowerMove;
    [SerializeField] private TextMeshProUGUI TowerDemolish;
    private Tower _tower;

    GameObject DisabledTower;

    GameObject SelectedTowerAvailable;

    GameObject SelectedTowerDisabled;

    int TowerSize;

    Ray ray;

    RaycastHit[] hits;

    [SerializeField] private Texture2D targetCursor;

    private void Update()
    {
        if (GameManager.instance.isClickedTower && Input.GetKeyDown(KeyCode.A))
        {
            SetTarget();
        }

        if (hits == null || DisabledTower == null)
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
                    if (_tower.TowerName == ETowerName.Bomb || _tower.TowerName == ETowerName.Wall)
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
            if (IsAvailableTile == false)
            {
                return;
            }

            // 돈도 부족하고 쿠폰도 없으면 리턴
            if (_tower.MovePrice > GameManager.instance.money)
            {
                return;
            }


            GameManager.instance.money -= _tower.MovePrice;

            ResetMoveTower();

            _tower.MoveTower(tile);
        }

        // 오른쪽 마우스 클릭 시 타워 설치 취소
        if (Input.GetMouseButtonUp(1))
        {
            if (DisabledTower == null)
            {
                return;
            }

            ResetMoveTower();
        }
    }

    public void SetTower(Tower tower)
    {
        _tower = tower;

        _tower.IsSelected(true);

        UpdateTowerInfo();
    }

    private void UpdateTowerInfo()
    {
        TowerName.text = _tower.TowerName.ToString();

        TowerLevel.text = string.Format(Tables.StringUI.Get(TowerLevel.gameObject.name)._Korean, _tower.UpgradeCount);
        TowerDamage.text = string.Format(Tables.StringUI.Get(TowerDamage.gameObject.name)._Korean, _tower.AttackStat);
        TowerFireRate.text =
            string.Format(Tables.StringUI.Get(TowerFireRate.gameObject.name)._Korean, _tower.SpeedStat);
        TowerPriority.text =
            string.Format(Tables.StringUI.Get(TowerPriority.gameObject.name)._Korean, _tower.AttackPriorityID);
        TowerUpgrade.text =
            string.Format(Tables.StringUI.Get(TowerUpgrade.gameObject.name)._Korean, _tower.UpgradePrice);
        TowerMove.text = string.Format(Tables.StringUI.Get(TowerMove.gameObject.name)._Korean, _tower.MovePrice);
        TowerDemolish.text =
            string.Format(Tables.StringUI.Get(TowerDemolish.gameObject.name)._Korean, _tower.SellPrice);

    }

    public void SetTarget()
    {
        ResetMoveTower();

        ChangeMouseCursor();

        GameManager.instance.isSettingTarget = 1;
    }

    private void ChangeMouseCursor()
    {
        Cursor.SetCursor(targetCursor, Vector2.zero, CursorMode.ForceSoftware);
    }

    public void AttackEnemy(GameObject obj)
    {
        _tower.SetTarget(obj);
        
        GameManager.instance.isSettingTarget = 0;
    }

    public void ClearTower()
    {
        _tower?.IsSelected(false);

        ResetMoveTower();
    }

    public void OnUpgradeBtnClick()
    {
        if(_tower == null)
        {
            return;
        }

        _tower.UpgradeTower();

        UpdateTowerInfo();
    }

    void ResetMoveTower()
    {
        Map.Instance.HideAllGridLine();

        StopAllCoroutines();

        if(DisabledTower == null)
        {
            return;
        }

        ObjectPools.Instance.ReleaseObjectToPool(DisabledTower);

        DisabledTower = null;
    }

    public void OnMoveBtnClick()
    {
        // 돈이 부족하면 리턴
        if (_tower.MovePrice > GameManager.instance.money)
        {
            return;
        }

        StartCoroutine(nameof(IE_FallowingMouse), _tower.TowerName);
    }

    IEnumerator IE_FallowingMouse(ETowerName TName)
    {
        WaitForFixedUpdate wf = new WaitForFixedUpdate();

        Map.Instance.ShowAllGridLine();

        // 버튼 밑에 타일이 있어서 바로 설치되는 것을 방지
        yield return wf;

        // 타워 크기 조정을 위해 임시로 타워 하나 가져옴

        DisabledTower = ObjectPools.Instance.GetPooledObject($"Disabled_{TName}Tower");

        TowerSize = Tables.Tower.Get(TName)._Size;

        DisabledTower.transform.localScale = new Vector3(0.2f * TowerSize, 0.2f * TowerSize, 0.2f * TowerSize);

        SelectedTowerAvailable = DisabledTower.transform.Find("Available").gameObject;

        SelectedTowerDisabled = DisabledTower.transform.Find("Disabled").gameObject;

        ChangeSelectedTowerMaterial(true);

        while (true)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            hits = Physics.RaycastAll(ray, 1000);

            foreach (var hit in hits)
            {
                if (DisabledTower == null)
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
                        DisabledTower.transform.position = new Vector3(tile.transform.position.x + half, tile.transform.position.y, tile.transform.position.z - half);
                    }
                    else
                    {
                        DisabledTower.transform.position = tile.transform.position;
                    }

                    // 타워를 짓지 못하는 곳은 오브젝트가 빨간색으로 변해야 함
                    // 공격로에 설치하는 애들
                    if (TName == ETowerName.Bomb || TName == ETowerName.Wall)
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
                    DisabledTower.transform.position = hit.point;

                    ChangeSelectedTowerMaterial(false);
                }
            }

            yield return wf;
        }

    }

    void ChangeSelectedTowerMaterial(bool Available)
    {
        if (DisabledTower != null && DisabledTower.gameObject.activeSelf)
        {
            SelectedTowerAvailable?.SetActive(Available);

            SelectedTowerDisabled?.SetActive(!Available);
        }
    }

    public void OnSellBtnClick()
    {
        if (_tower == null)
        {
            return;
        }

        _tower.SellTower();

        gameObject.SetActive(false);
    }
}
