using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public delegate void ObjectOnTileDisabledCallback();

[ExecuteInEditMode]
public class Tile : MonoBehaviour
{
    [SerializeField] TextMeshPro PosText;

    [SerializeField] GameObject HiddenBody;

    [SerializeField] Material[] LandTileMaterals;

    [SerializeField] Material[] AttackRouteTileMaterials;

    [SerializeField] MeshRenderer PlaneMeshRenderer;

    [SerializeField] GameObject _ObjectOnTile;

    public Pos _GroundPos = new();

    public Pos _MapPos = new();

    public int WaypointIndex = -1;

    public int WaypointRoute = -1;

    public bool IsFixedObstacle = false;            // 장애물 설치를 위한 변수, 장애물 모양이 결정되면 true

    public bool IsSelectedAttackRoute = false;      // 웨이포인트 설정을 위해, 이미 선택된 공격로인지

    public Ground ParentGround;

    ETileType _TileType;

    int RandomLand = 0;

    int RandomAttackRoute = 0;


    /*
#if UNITY_EDITOR
    private void Update()
    {
        PosText.text = string.Format("({0}, {1})\n{2}", _GroundPos.PosX, _GroundPos.PosY, (int)TileType);

        Color NewColor = Color.yellow;

        switch (TileType)
        {
            case ETileType.Land:
                NewColor = Color.white;
                HiddenBody.SetActive(false);
                break;

            case ETileType.AttackRoute:
                NewColor = Color.red;
                break;

            case ETileType.House:
                NewColor = Color.green;
                break;
        }

        UpdateTileMateral();

        PosText.color = NewColor;
    }
#endif
    */

    public void OnEnable()
    {
        RandomLand = Random.Range(0, LandTileMaterals.Length);

        RandomAttackRoute = Random.Range(0, AttackRouteTileMaterials.Length);
    }


    public Vector2 CalculateDistance(Pos another)
    {
        return new Vector2(another.PosX - _GroundPos.PosX, another.PosY - _GroundPos.PosY);
    }

    public bool IsResourceOnTile()
    {
        if(_ObjectOnTile == null)
        {
            return false;
        }

        var res = _ObjectOnTile.GetComponent<Resource>();

        if (res == null)
        {
            return false;
        }

        return true;
    }

    public void SetTileType(ETileType Sender)
    {
        _TileType = Sender;

        UpdateTileMateral();
    }

    public bool CheckTileType(ETileType Sender)
    {
        return _TileType == Sender;
    }


    void UpdateTileMateral()
    {
        if(_TileType == ETileType.Land || _TileType == ETileType.House)
        {
            if(LandTileMaterals.Length != 0)
            {
                PlaneMeshRenderer.material = LandTileMaterals[RandomLand];
            }
        }
        else
        {
            if (AttackRouteTileMaterials.Length != 0)
            {
                PlaneMeshRenderer.material = AttackRouteTileMaterials[RandomAttackRoute];
            }
        }
    }

    public void SetObjectOnTile(GameObject go, int size = 1)
    {
        if(size < Tables.GlobalSystem.Get("Tower_Size_Min")._Value || size > Tables.GlobalSystem.Get("Tower_Size_Max")._Value)
        {
            return;
        }

        _ObjectOnTile = go;

        //  TODO 사이즈만큼 주변 타일들도 이 오브젝트를 참조해야 함
        if(size != 1)
        {
            Map.Instance.GetTileInMap(_MapPos.SumPos(Map.Instance._Direction[Direction.R])).SetObjectOnTile(go);
            Map.Instance.GetTileInMap(_MapPos.SumPos(Map.Instance._Direction[Direction.DR])).SetObjectOnTile(go);
            Map.Instance.GetTileInMap(_MapPos.SumPos(Map.Instance._Direction[Direction.D])).SetObjectOnTile(go);


            // size == 3일 때 왼쪽 위, 위, 오른쪽 위, 왼쪽, 왼쪽 아래
            if (size == 3)
            {
                Map.Instance.GetTileInMap(_MapPos.SumPos(Map.Instance._Direction[Direction.UL])).SetObjectOnTile(go);
                Map.Instance.GetTileInMap(_MapPos.SumPos(Map.Instance._Direction[Direction.U])).SetObjectOnTile(go);
                Map.Instance.GetTileInMap(_MapPos.SumPos(Map.Instance._Direction[Direction.UR])).SetObjectOnTile(go);
                Map.Instance.GetTileInMap(_MapPos.SumPos(Map.Instance._Direction[Direction.L])).SetObjectOnTile(go);
                Map.Instance.GetTileInMap(_MapPos.SumPos(Map.Instance._Direction[Direction.DL])).SetObjectOnTile(go);
            }
        }
    }

    public GameObject GetObjectOnTile()
    {
        if (_ObjectOnTile != null && _ObjectOnTile.activeSelf)
        {
            return _ObjectOnTile;
        }

        return null;
    }

    public bool CheckObjectOnTileWithSize(int size = 1)
    {
        if (size == 1)
        {
            return CheckBuildAvailableTile();
        }

        if (CheckAroundTile(Map.Instance._Direction[Direction.R]) == false) return false;
        if (CheckAroundTile(Map.Instance._Direction[Direction.D]) == false) return false;
        if (CheckAroundTile(Map.Instance._Direction[Direction.DR]) == false) return false;

        if (size == 3)
        {
            if (CheckAroundTile(Map.Instance._Direction[Direction.UL]) == false) return false;
            if (CheckAroundTile(Map.Instance._Direction[Direction.U]) == false) return false;
            if (CheckAroundTile(Map.Instance._Direction[Direction.UR]) == false) return false;
            if (CheckAroundTile(Map.Instance._Direction[Direction.L]) == false) return false;
            if (CheckAroundTile(Map.Instance._Direction[Direction.DL]) == false) return false;
        }

        return true;
    }

    bool CheckBuildAvailableTile()
    {
        if(_TileType == ETileType.Land && (_ObjectOnTile == null || !_ObjectOnTile.activeSelf))
        {
            return true;
        }

        return false;
    }

    bool CheckAroundTile(Pos TilePos)
    {
        var tile = Map.Instance.GetTileInMap(_MapPos.SumPos(TilePos));

        if (tile == null || tile.ParentGround.gameObject.activeSelf == false)
        {
            return false;
        }

        return tile.CheckBuildAvailableTile();
    }
}
