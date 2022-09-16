using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationUpgrade : MonoBehaviour
{
    public static ApplicationUpgrade instance = null;
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void ApplicationPassiveUpgrade(int id)
    {
        if (id / 100 == 301)    //HP증가
        {
            GameManager.instance.maxHp += (int)Tables.UpgradePassiveStat.Get(id)._Increase_Passive_Value;
        }

        if (id / 100 == 302)    //증강체 갯수 증가
        {
            GameManager.instance.number_Of_Upgrade += 1;
        }

        if (id / 100 == 303)    //장애물제거 비용 감소
        {
            
        }

        if (id / 100 == 304)    //타워 출현 확률 증가
        {
            
        }
    }
}
