using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdateTowerButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI cost;

    [HideInInspector] public int index;

    private void OnEnable()
    {
        Debug.Log("ChangeText" + index);
        
    }

    public void ChangeText()
    {
        Debug.Log("In" + index);
        title.text = Tables.Tower.Get(index + 1)._Korean;
        Debug.Log("title" + Tables.Tower.Get(index + 1)._Korean);
        Debug.Log("cost" + Tables.Tower.Get(index + 1)._Price);
        cost.text = Tables.Tower.Get(index + 1)._Price.ToString();
    }
}
