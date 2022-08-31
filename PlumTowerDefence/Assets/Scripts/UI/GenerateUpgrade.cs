using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateUpgrade : MonoBehaviour
{
    /// <summary>
    /// 메인메뉴의 강화탭 에서 강화 카드를 생성하는 스크립트
    /// 현재는 테스트 목적으로 사용되고 있음
    /// </summary>
    
    public int x;       //test목적의 변수
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))        //test spawn Upgrade
        {
            GameObject obj = ObjectPools.Instance.GetPooledObject("UpgradeSelect");
            obj.transform.SetParent(gameObject.transform);
            obj.transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
