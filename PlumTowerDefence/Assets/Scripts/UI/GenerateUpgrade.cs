using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateUpgrade : MonoBehaviour
{
    // Start is called before the first frame update
    public int x;       //test목적의 변수
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))        //test spawn Upgrade
        {
            ObjectPools.Instance.GetPooledObject("UpgradeSelect");
        }
    }
}
