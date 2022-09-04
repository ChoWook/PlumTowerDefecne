using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
[CreateAssetMenu(menuName = "Scriptables/ObjectPoolsData")]
public class ObjectPoolsScriptable : ScriptableObject
{
    public GameObject[] ObjectPrefabs;
}
