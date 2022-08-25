using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteInEditMode]
public class Tile : MonoBehaviour
{
    [SerializeField] TextMeshPro PosText;

    public ETileType TileType;

    public int PosX;
    public int PosY;

    GameObject ObjectOnTile;

#if UNITY_EDITOR
    private void Update()
    {
        PosText.text = string.Format("({0}, {1})\n{2}", PosX, PosY, (int)TileType);

        Color NewColor = Color.yellow;

        switch (TileType)
        {
            case ETileType.Land:
                NewColor = Color.white;
                gameObject.SetActive(false);
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
}
