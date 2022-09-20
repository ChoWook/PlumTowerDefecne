using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAniContoller : MonoBehaviour
{
    public abstract void DeadAnimation();

    public abstract void InitAnimation();

    public bool Isrevived;
    public bool Isdivided;
}
