using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteInEditMode]
public class Tile : MonoBehaviour
{
    [SerializeField] TextMeshPro PosText;

    [SerializeField] GameObject HiddenBody;

    [SerializeField] Material[] LandTileMaterals;

    [SerializeField] Material[] AttackRouteTileMaterials;

    [SerializeField] MeshRenderer PlaneMeshRenderer;

    public ETileType TileType;
    
    public int PosX;
    public int PosY;

    public GameObject ObjectOnTile;

    public bool IsFixedObstacle = false;            // 장애물 설치를 위한 변수, 장애물 모양이 결정되면 true

    int RandomLand = 0;

    int RandomAttackRoute = 0;


#if UNITY_EDITOR
    private void Update()
    {
        PosText.text = string.Format("({0}, {1})\n{2}", PosX, PosY, (int)TileType);

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

    public void OnEnable()
    {
        RandomLand = Random.Range(0, LandTileMaterals.Length);

        RandomAttackRoute = Random.Range(0, AttackRouteTileMaterials.Length);
    }


    public Vector2 CalculateDistance(Tile another)
    {
        return new Vector2(another.PosX - PosX, another.PosY - PosY);
    }

    public bool IsResourceOnTile()
    {
        if(ObjectOnTile == null)
        {
            return false;
        }

        var res = ObjectOnTile.GetComponent<Resource>();

        if (res == null)
        {
            return false;
        }

        return true;
    }

    void UpdateTileMateral()
    {
        if(TileType == ETileType.Land)
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
}
