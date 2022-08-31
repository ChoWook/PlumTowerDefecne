using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CategoryButton : MonoBehaviour
{
    /// <summary>
    /// 각 카테고리 안에 들어가는 스크립트
    /// 현제 버튼이 가지는 정보를 포함하고 있음
    /// </summary>
    [HideInInspector] public CategoryType categoryType;

    [HideInInspector] public int id;
    

    private void OnEnable()
    {
        
    }

    void ChangeText()
    {
        
    }
}
