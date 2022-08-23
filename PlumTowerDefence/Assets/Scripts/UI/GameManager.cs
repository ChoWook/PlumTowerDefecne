using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;      //싱글톤 기법

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            Tables.Load();                      //게임매니져가 생성될 당시 한번만 테이블을 로드
        }
        else
        {
            if(instance!=this)
                Destroy(this.gameObject);       //이미 존재한다면 새로 생상된 오브젝트를 제거
        }
    }

    private int level, xp, hp, money;

}
