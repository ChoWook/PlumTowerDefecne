using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteInEditMode]
public class Tile : MonoBehaviour
{
    #region Serialize Field
    [SerializeField] Material[] LandTileMaterals;               // 평지 타일 마테리얼

    [SerializeField] Material[] AttackRouteTileMaterials;       // 공격로 타일 마테리얼 

    [SerializeField] MeshRenderer PlaneMeshRenderer;            // 타일 마테리얼을 바꾸기 위한 매시 랜더러

    [SerializeField] GameObject _ObjectOnTile;                  // 타일 위에 있는 오브젝트
    #endregion

    #region Public Field

    public Pos _GroundPos = new();                              // 그라운드 상에서의 위치

    public Pos _MapPos = new();                                 // 맵에서의 위치

    public int WaypointIndex = -1;

    public int WaypointRoute = -1;

    public bool IsFixedObstacle = false;                        // 장애물 설치를 위한 변수, 장애물 모양이 결정되면 true

    public bool IsSelectedAttackRoute = false;                  // 웨이포인트 설정을 위해, 이미 선택된 공격로인지

    public Ground ParentGround;

    #endregion

    #region Private Feild

    ETileType _TileType;

    int RandomLand = 0;

    int RandomAttackRoute = 0;

    #endregion

    #region Unity Function
    public void OnEnable()
    {
        RandomLand = Random.Range(0, LandTileMaterals.Length);

        RandomAttackRoute = Random.Range(0, AttackRouteTileMaterials.Length);
    }

    #endregion

    #region Get
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

    public bool CheckTileType(ETileType Sender)
    {
        return _TileType == Sender;
    }

    public GameObject GetObjectOnTile()
    {
        if (_ObjectOnTile != null && _ObjectOnTile.activeSelf)
        {
            return _ObjectOnTile;
        }

        return null;
    }

    bool CheckBuildAvailableTile()
    {
        if (_TileType == ETileType.Land && (_ObjectOnTile == null || !_ObjectOnTile.activeSelf))
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

    public bool CheckObjectOnTileWithSize(int size = 1)
    {
        /*
        if (size == 1)
        {
            return CheckBuildAvailableTile();
        }
        */
        if (size > 1)
        {
            if (CheckAroundTile(Map.Instance._Direction[EDirection.R]) == false) return false;
            if (CheckAroundTile(Map.Instance._Direction[EDirection.D]) == false) return false;
            if (CheckAroundTile(Map.Instance._Direction[EDirection.DR]) == false) return false;

            if (size == 3)
            {
                if (CheckAroundTile(Map.Instance._Direction[EDirection.UL]) == false) return false;
                if (CheckAroundTile(Map.Instance._Direction[EDirection.U]) == false) return false;
                if (CheckAroundTile(Map.Instance._Direction[EDirection.UR]) == false) return false;
                if (CheckAroundTile(Map.Instance._Direction[EDirection.L]) == false) return false;
                if (CheckAroundTile(Map.Instance._Direction[EDirection.DL]) == false) return false;
            }
        }
        
        // 사이즈가 1보다 클 때도 마우스가 올라가 있는 타일을 검사해야 함
        return CheckBuildAvailableTile();
    }

    #endregion

    #region Set
    public void SetTileType(ETileType Sender)
    {
        _TileType = Sender;

        UpdateTileMateral();
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
            Map.Instance.GetTileInMap(_MapPos.SumPos(Map.Instance._Direction[EDirection.R])).SetObjectOnTile(go);
            Map.Instance.GetTileInMap(_MapPos.SumPos(Map.Instance._Direction[EDirection.DR])).SetObjectOnTile(go);
            Map.Instance.GetTileInMap(_MapPos.SumPos(Map.Instance._Direction[EDirection.D])).SetObjectOnTile(go);


            // size == 3일 때 왼쪽 위, 위, 오른쪽 위, 왼쪽, 왼쪽 아래
            if (size == 3)
            {
                Map.Instance.GetTileInMap(_MapPos.SumPos(Map.Instance._Direction[EDirection.UL])).SetObjectOnTile(go);
                Map.Instance.GetTileInMap(_MapPos.SumPos(Map.Instance._Direction[EDirection.U])).SetObjectOnTile(go);
                Map.Instance.GetTileInMap(_MapPos.SumPos(Map.Instance._Direction[EDirection.UR])).SetObjectOnTile(go);
                Map.Instance.GetTileInMap(_MapPos.SumPos(Map.Instance._Direction[EDirection.L])).SetObjectOnTile(go);
                Map.Instance.GetTileInMap(_MapPos.SumPos(Map.Instance._Direction[EDirection.DL])).SetObjectOnTile(go);
            }
        }
    }
    #endregion
}
