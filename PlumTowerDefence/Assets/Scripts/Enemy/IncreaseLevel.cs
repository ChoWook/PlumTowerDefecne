using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseLevel : MonoBehaviour
{
    public void IncreaseLevel1()
    {
        GameManager.instance.level++;
    }

}
