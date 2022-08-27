using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteInEditMode]
public class Tile : MonoBehaviour
{
    [SerializeField] TextMeshPro PosText;

    [SerializeField] GameObject HiddenBody;

    public ETileType TileType;

    public int PosX;
    public int PosY;

    public GameObject ObjectOnTile;

    public bool IsFixedObstacle = false;            // 장애물 설치를 위한 변수, 장애물 모양이 결정되면 true


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

        PosText.color = NewColor;
    }
#endif

    public Vector2 CalculateDistance(Tile another)
    {
        return new Vector2(another.PosX - PosX, another.PosY - PosY);
    }
}
