using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Debug_RemainEnemy : MonoBehaviour
{
    private TextMeshProUGUI text;
    private void Awake()
    {
        text = gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "remainEnemy : " + GameManager.instance.CurrentEnemyNumber;
    }
}
