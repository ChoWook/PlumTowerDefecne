using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTIncreaseXP : MonoBehaviour
{
    public void XPIncrease()
    {
        GameManager.instance.xp += 100;
    }
}
