using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JsonManager : MonoBehaviour
{
    public static JsonManager instance = null;

    public List<int> upgradedCard;

    public List<int> usingList;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            upgradedCard = new List<int>();
            ClearUpgrade();
            WriteJson();
        }
    }

    public void LoadJson()
    {
        if (!File.Exists(Application.dataPath + "/UpgradeJson.json"))
        {
            File.WriteAllText(Application.dataPath + "/UpgradeJson.json", JsonUtility.ToJson(upgradedCard));
        }
        else
        {
            string str = File.ReadAllText(Application.dataPath + "/UpgradeJson.json");

            upgradedCard = JsonUtility.FromJson<List<int>>(str);
        }
    }

    public void WriteJson()
    {
        File.WriteAllText(Application.dataPath + "/UpgradeJson.json", JsonUtility.ToJson(upgradedCard));
    }

    public void BuyUpgrade(int id)
    {
        upgradedCard.Add(id);
    }

    public void ClearUpgrade()
    {
        upgradedCard.Clear();

        for (int i = 1; i <= 3; i++)
        {
            for (int j = 1; j <= Tables.UpgradeButton.Get(i)._CategoryNum; j++)
            {
                for (int k = 1; k <= Tables.UpgradeCategory.Get(i * 10000 + j * 100)._CardNum; k++)
                {
                    if (Tables.UpgradeCard.Get(i * 10000 + j * 100 + k)._XpCost == 0)
                    {
                        upgradedCard.Add(i * 10000 + j * 100 + k);
                        Debug.Log("Add " + (i * 10000 + j * 100 + k));
                    }
                }
            }
        }
    }
}
