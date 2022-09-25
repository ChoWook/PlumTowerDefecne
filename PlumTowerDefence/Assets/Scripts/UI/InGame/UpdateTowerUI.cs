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
                    // ���ݷο� ��ġ�ϴ� �ֵ��̸�
                    if (_tower.TowerName == ETowerName.Bomb || _tower.TowerName == ETowerName.Wall)
                    {
                        IsAvailableTile = tile.CheckObjectOnAttackRoute();
                    }
                    // ������ ��ġ�ϴ� �ֵ��̸�
                    else
                    {
                        IsAvailableTile = tile.CheckObjectOnLandWithSize(TowerSize);
                    }
                    break;
                }
            }

            // Ÿ���� ��ġ ������ Ÿ���� �ƴϸ� ����
            if (IsAvailableTile == false)
            {
                return;
            }

            // ���� �����ϰ� ������ ������ ����
            if (_tower.MovePrice > GameManager.instance.money)
            {
                return;
            }


            GameManager.instance.money -= _tower.MovePrice;

            ResetMoveTower();

            _tower.MoveTower(tile);
        }

        // ������ ���콺 Ŭ�� �� Ÿ�� ��ġ ���
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
        // ���� �����ϸ� ����
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

        // ��ư �ؿ� Ÿ���� �־ �ٷ� ��ġ�Ǵ� ���� ����
        yield return wf;

        // Ÿ�� ũ�� ������ ���� �ӽ÷� Ÿ�� �ϳ� ������

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

                    // ���콺 ����ٴϴ� ������Ʈ ��ġ ����
                    if (TowerSize == 2)
                    {
                        float half = GameManager.instance.unitTileSize / 2;
                        DisabledTower.transform.position = new Vector3(tile.transform.position.x + half, tile.transform.position.y, tile.transform.position.z - half);
                    }
                    else
                    {
                        DisabledTower.transform.position = tile.transform.position;
                    }

                    // Ÿ���� ���� ���ϴ� ���� ������Ʈ�� ���������� ���ؾ� ��
                    // ���ݷο� ��ġ�ϴ� �ֵ�
                    if (TName == ETowerName.Bomb || TName == ETowerName.Wall)
                    {
                        ChangeSelectedTowerMaterial(tile.CheckObjectOnAttackRoute());
                    }
                    // ������ ��ġ�ϴ� �ֵ�
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
