using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null; //싱글톤 기법

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            Tables.Load(); //게임매니져가 생성될 당시 한번만 테이블을 로드
        }
        else
        {
            if (instance != this)
                Destroy(this.gameObject); //이미 존재한다면 새로 생상된 오브젝트를 제거
        }
    }

    public delegate void CallBack();

    private CallBack _stageClearCallBack;

    public void AddStageClearCallBack(CallBack stageClearCallBack)
    {
        _stageClearCallBack += stageClearCallBack;
    }

    public void RemoveStageClearCallBack()
    {
        _stageClearCallBack = null;
    }

    private CallBack _levelChangeCallBack;

    public void AddLevelChangeCallBack(CallBack levelChangeCallBack)
    {
        _levelChangeCallBack += levelChangeCallBack;
    }

    public void RemoveLevelChangeCallBack()
    {
        _levelChangeCallBack = null;
    }

    private CallBack _xpChangeCallBack;

    public void AddXpChangeCallBack(CallBack xpChangeCallBack)
    {
        _xpChangeCallBack += xpChangeCallBack;
    }

    public void RemoveXpChangeCallBack()
    {
        _xpChangeCallBack = null;
    }

    private CallBack _hpChangeCallBack;

    public void AddHpChangeCallBack(CallBack hpChangeCallBack)
    {
        _hpChangeCallBack += hpChangeCallBack;
    }

    public void RemoveHpChangeCallBack()
    {
        _hpChangeCallBack = null;
    }

    private CallBack _moneyChangeCallBack;

    public void AddMoneyChangeCallBack(CallBack moneyChangeCallBack)
    {
        _moneyChangeCallBack += moneyChangeCallBack;
    }

    public void RemoveMoneyChangeCallBack()
    {
        _moneyChangeCallBack = null;
    }

    private CallBack _gameOverCallBack;

    public void AddGameOverCallBack(CallBack gameOverCallBack)
    {
        _gameOverCallBack += gameOverCallBack;
    }

    public void RemoveGameOverCallBack()
    {
        _gameOverCallBack = null;
    }

    private CallBack _OnGetCouponCallBack;

    public void AddGetCouponCallBack(CallBack OnGetCouponCallBack)
    {
        _OnGetCouponCallBack += OnGetCouponCallBack;
    }

    public void RemoveGetCouponCallBack()
    {
        _OnGetCouponCallBack = null;
    }

    public void CallBackClear()
    {
        RemoveStageClearCallBack();
        RemoveLevelChangeCallBack();
        RemoveXpChangeCallBack();
        RemoveHpChangeCallBack();
        RemoveMoneyChangeCallBack();
        RemoveGameOverCallBack();
        RemoveGetCouponCallBack();
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

    private int _xp = 0; //현재 xp

    public int xp
    {
        get { return _xp; }
        set
        {
            _xp = value;
            _xpChangeCallBack?.Invoke();
        }
    }

    private int _totalxp = 0;       //전체 xp 양

    public int totalxp
    {
        get
        {
            return _totalxp;
        }
        set
        {
            int tmp = _totalxp;
            _totalxp = value;
            remainxp += _totalxp - tmp;
            JsonManager.instance.SaveData.totalXP = _totalxp;
        }
    }

    private int _remainxp = 0;       //사용하고 남은 xp 양

    public int remainxp
    {
        get { return _remainxp; }
        set
        {
            _remainxp = value;
            JsonManager.instance.SaveData.remainXP = _remainxp;
        }
    }

    private int _maxHp = 10;

    public int maxHp
    {
        get { return _maxHp; }
        set
        {
            _maxHp = value;
            currentHp = maxHp;
        }
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
                totalxp += xp;
            }
        }
    }

    private int _money;

    public int money
    {
        get { return _money; }
        set
        {
            _money = value;
            _moneyChangeCallBack?.Invoke();
        }
    }

    private int _number_Of_Upgrade;

    public int number_Of_Upgrade
    {
        get { return _number_Of_Upgrade; }
        set { _number_Of_Upgrade = value; }
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
            if (_currentEnemyNumber == 0)
            {
                isPlayingGame = false; //Bool flase 만들어서 게임 끝을 알림
                xp += level; //level만큼 xp를 얻음
                _stageClearCallBack?.Invoke();
            }
        }
    }

    private int _isSettingTarget = 0; //타겟팅을 정하는 중인가,  0=X, 1=updateTowerUI, 2=GroundTower, 3=AllTower

    public int isSettingTarget
    {
        get { return _isSettingTarget; }
        set { _isSettingTarget = value; }
    }

    private bool _isClickedTower = false; //tower를 누른 상태(타워ui가 활성화된 상태)인가

    public bool isClickedTower
    {
        get { return _isClickedTower; }
        set { _isClickedTower = value; }
    }

    //사거리 계산 단위

    private float _unitTileSize;

    public float unitTileSize
    {
        get { return _unitTileSize; }
        set { _unitTileSize = value; }
    }

    //장애물 파괴 할인율
    private float _discountObstacle = 0f;
    public float discountObstacle
    {
        get { return _discountObstacle; }
        set { _discountObstacle = value; }
    }

    private float _increaseTowerCoupon = 0f;

    public float increaseTowerCoupon
    {
        get { return _increaseTowerCoupon; }
        set { _increaseTowerCoupon = value; }
    }


// 타워 무료 쿠폰
    private Dictionary<ETowerName ,int> _TowerCoupon;

    public void InitCoupon()
    {
        _TowerCoupon = new Dictionary<ETowerName, int>();
        // TODO 타워 ENUM 생기면 바꿔야 함
        for (ETowerName name = ETowerName.Arrow; name <= ETowerName.Bomb; name++)
        {
            _TowerCoupon.Add(name, 0);
        }
    }

    public void AddCoupon(ETowerName name)
    {
        _TowerCoupon[name]++;
        _OnGetCouponCallBack?.Invoke();
        _moneyChangeCallBack?.Invoke();
    }

    public void RemoveCoupon(ETowerName name)
    {
        if (HasCoupon(name))
        {
            _TowerCoupon[name]--;
            _OnGetCouponCallBack?.Invoke();
        }
        else
        {
            Debug.LogWarning("RemoveCoupon Error");
        }
    }

    public bool HasCoupon(ETowerName name)
    {
        if (_TowerCoupon[name] <= 0)
            return false;
        else
            return true;
    }

    public int GetCoupon(ETowerName name)
    {
        return _TowerCoupon[name];
    }

    public void InitGame()
    {
        money = Tables.GlobalSystem.Get("User_Money")._Value;
        maxHp = Tables.GlobalSystem.Get("User_Hp")._Value;
        currentHp = maxHp;
        xp = 0;
        level = 0;
        discountObstacle = 0f;
        increaseTowerCoupon = 0f;
        number_Of_Upgrade = Tables.GlobalSystem.Get("Number_Of_Upgrade")._Value;
        
        isPausing = false;
        isClickedTower = false;
        isPlayingGame = false;
        isSettingTarget = 0;

        InitCoupon();
    }



}
