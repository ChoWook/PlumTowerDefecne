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

    public delegate void CallBack();

    private CallBack _stageClearCallBack;

    public void AddStageClearCallBack(CallBack stageClearCallBack)
    {
        _stageClearCallBack += stageClearCallBack;
    }

    private CallBack _levelChangeCallBack;

    public void AddLevelChangeCallBack(CallBack levelChangeCallBack)
    {
        _levelChangeCallBack += levelChangeCallBack;
    }

    private CallBack _xpChangeCallBack;

    public void AddXpChangeCallBack(CallBack xpChangeCallBack)
    {
        _xpChangeCallBack += xpChangeCallBack;
    }

    private CallBack _hpChangeCallBack;

    public void AddHpChangeCallBack(CallBack hpChangeCallBack)
    {
        _hpChangeCallBack += hpChangeCallBack;
    }

    private CallBack _moneyChangeCallBack;

    public void AddMoneyChangeCallBack(CallBack moneyChangeCallBack)
    {
        _moneyChangeCallBack += moneyChangeCallBack;
    }

    private CallBack _gameOverCallBack;

    public void AddGameOverCallBack(CallBack gameOverCallBack)
    {
        _gameOverCallBack += gameOverCallBack;
    }
    
    
    
    private int _level = 0;
    public int level
    {
        get { return _level; }
        set
        {
            _level = value;
            _levelChangeCallBack?.Invoke();
        }
    }
    private int _xp = 0;
    public int xp
    {
        get { return _xp; }
        set
        {
            _xp = value;
            _xpChangeCallBack?.Invoke();
        }
    }
    private int _maxHp = 10;
    public int maxHp
    {
        get { return _maxHp; }
        private set { _maxHp = value; }
    }
    private int _currentHp = 10;
    public int currentHp
    {
        get { return _currentHp; }
        set
        {
            _currentHp = value;
            _hpChangeCallBack?.Invoke();
            if (currentHp <= 0)
            {
                _gameOverCallBack?.Invoke();
            }
        }
    }
    private int _money = 100;
    public int money
    {
        get { return _money; }
        set
        {
            _money = value;
            _moneyChangeCallBack?.Invoke();
        }
    }

    private bool _isPlayingGame = false;

    public bool isPlayingGame
    {
        get { return _isPlayingGame; }
        set { _isPlayingGame = value; }
    }

    private bool _isPausing = false;

    public bool isPausing
    {
        get { return _isPausing; }
        set { _isPausing = value; }
    }

    private int _currentEnemyNumber;
    public int currentEnemyNumber
    { 
        get { return _currentEnemyNumber; } 
        set 
        { 
            _currentEnemyNumber = value; 
            if(_currentEnemyNumber == 0)
            {
                isPlayingGame = false;                 //Bool flase 만들어서 게임 끝을 알림
                xp += level;      //level만큼 xp를 얻음
                _stageClearCallBack?.Invoke();
            }
        } 
    }

    private bool _isClickedTower = false;

    public bool isClickedTower
    {
        get { return _isClickedTower; }
        set { _isClickedTower = value; }
    }


    // 타워 무료 쿠폰
    private List<int> _TowerCoupon;

    public void InitCoupon()
    {
        _TowerCoupon = new List<int>();
        // TODO 타워 ENUM 생기면 바꿔야 함
        for (int i = 0; i <= 10; i++)
        {
            _TowerCoupon.Add(0);
        }
    }

    public void AddCoupon(int idx)
    {
        _TowerCoupon[idx]++;
    }

    public void RemoveCoupon(int idx)
    {
        if (HasCoupon(idx))
        {
            _TowerCoupon[idx]--;
        }
        else
        {
            Debug.LogWarning("RemoveCoupon Error");
        }
    }

    public bool HasCoupon(int idx)
    {
        if (_TowerCoupon[idx] == 0)
            return false;
        else
            return true;
    }
}
