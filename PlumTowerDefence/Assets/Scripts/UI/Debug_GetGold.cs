using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debug_GetGold : MonoBehaviour
{
    public void GetGold()
    {
        GameManager.instance.Money += 10000;
    }
}
