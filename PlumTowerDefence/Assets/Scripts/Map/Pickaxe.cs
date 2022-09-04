using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickaxe : MonoBehaviour
{
    [SerializeField] Material[] Materials;

    [SerializeField] MeshRenderer _MeshRenderer;

    EPickaxeType _Type;

    public void SetPickaxeType(EPickaxeType Sender)
    {
        _Type = Sender;

        UpdateMaterial();
    }

    public void UpdateMaterial()
    {
        _MeshRenderer.material = Materials[(int)(_Type) - 1];
    }
}
