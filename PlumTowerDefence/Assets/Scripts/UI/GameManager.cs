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

    private static int level = 0;
    private static int xp = 0;
    private static int maxHp = 10;
    private static int currentHp = 10;
    private static int money = 100;

    [SerializeField] private GameObject expandButton;
    [SerializeField] private GameObject startButton;

    public void ExpandArea()    //확장하기 버튼을 누르면 호출
    {
        //영토 확장
        expandButton.SetActive(false);
        startButton.SetActive(true);
    }
    
    public void PlayGame()      //게임 시작시 호출
    {
        level++;
        //게임시작
        //if(적을 다 죽이면 || currentHp<=0)
        //게임종료
    }
    
    public static int GetLevel()
    {
        return level;
    }
    public static int GetXp()
    {
        return xp;
    }
    public static int GetMaxHp()
    {
        return maxHp;
    }
    public static int GetCurrentHp()
    {
        return currentHp;
    }
    public static int GetMoney()
    {
        return money;
    }
}
