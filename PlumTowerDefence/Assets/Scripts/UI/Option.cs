using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Option : MonoBehaviour
{
    /// <summary>
    /// option창을 담당하는 스크립트
    /// </summary>
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
            CloseOption();
    }

    void CloseOption()
    {
        ObjectPools.Instance.ReleaseObjectToPool(gameObject);
    }
}
