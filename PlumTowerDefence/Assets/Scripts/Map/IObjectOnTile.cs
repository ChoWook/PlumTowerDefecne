using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IObjectOnTile : MonoBehaviour
{
    protected List<Tile> BelowTile = new();

    public void SetBelowTile(Tile Sender)
    {
        BelowTile.Add(Sender);
    }

    void OnDisable()
    {
        ClearObjectOnTile();
    }

    protected void ClearObjectOnTile()
    {
        for (int i = 0; i < BelowTile.Count; i++)
        {
            BelowTile[i].RemoveObjectOnTile();
        }

        BelowTile.Clear();
    }
}
