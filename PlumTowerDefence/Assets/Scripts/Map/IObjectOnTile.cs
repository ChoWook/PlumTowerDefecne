using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IObjectOnTile : MonoBehaviour
{
    private List<Tile> BelowTile = new();

    public void SetBelowTile(Tile Sender)
    {
        BelowTile.Add(Sender);
    }

    void OnDisable()
    {
        for (int i = 0; i < BelowTile.Count; i++)
        {
            BelowTile[i].RemoveObjectOnTile();
        }
        
        BelowTile.Clear();
    }
}
