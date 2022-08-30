using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUpgrade : MonoBehaviour
{
    /// <summary>
    /// 인게임 내의 증강체를 모으는 패널이 보유하는 스크립트
    /// 증강체를 오브젝트풀에서 생성하는 함수와 부모 지정의 시간차로 인한 여러 세팅에 대한 코드를 보유중
    /// 증강체를 선택한 이후 오브젝트풀로 release하는 함수 또한 관리 하고 있음
    /// </summary>
    
    private int number_Of_Upgrade = Tables.GlobalSystem.Get("Number_Of_Upgrade")._Value;
    public void ShowInGameUpgrade()
    {
        for (int i = 0; i < number_Of_Upgrade; i++)
        {
            GameObject obj = ObjectPools.Instance.GetPooledObject("InGameUpgrade"); //증강체 생성
            
            obj.transform.SetParent(gameObject.transform);                               //부모 설정
            obj.transform.localScale = new Vector3(1f, 1f, 1f);                    //스케일 변환
            obj.GetComponent<Toggle>().group = gameObject.GetComponent<ToggleGroup>();  //라디오 버튼 설정
            obj.transform.Find("Select")?.GetComponent<Button>().onClick.AddListener(()=>SelectInGameUpgrade());//온 버튼 적용
        }
    }

    public void SelectInGameUpgrade()
    {
        int count = transform.childCount;
        for (int i = 0; i < count; i++)
        {
            ObjectPools.Instance.ReleaseObjectToPool(transform.GetChild(0).gameObject);
        }
    }
}
