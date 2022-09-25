using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveData
{
    public List<int> upgradedCard;
    public int totalXP;
    public int remainXP;
}

public class JsonManager : MonoBehaviour
{
    public static JsonManager instance = null;

    public List<int> usingList;
    private SaveData saveData = new SaveData();

    public SaveData SaveData
    {
        set { saveData = value;}
        get { return saveData; }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            SaveData.upgradedCard = new List<int>();
            ClearUpgrade();
            
            LoadJson();
        }
    }

    public void LoadJson()
    {
        if (!File.Exists(Application.dataPath + "/UpgradeJson.json"))
        {
            File.WriteAllText(Application.dataPath + "/UpgradeJson.json", JsonUtility.ToJson(SaveData));
        }
        else
        {
            string str = File.ReadAllText(Application.dataPath + "/UpgradeJson.json");

            SaveData = JsonUtility.FromJson<SaveData>(str);

            GameManager.instance.TotalXP = SaveData.totalXP;
            GameManager.instance.RemainXP = SaveData.remainXP;
        }
    }

    public void WriteJson()
    {
        File.WriteAllText(Application.dataPath + "/UpgradeJson.json", JsonUtility.ToJson(SaveData));
    }

    public void BuyUpgrade(int id)
    {
        SaveData.upgradedCard.Add(id);
    }

    public void ClearUpgrade()
    {
        SaveData.upgradedCard.Clear();

        for (int i = 1; i <= 3; i++)
        {
            for (int j = 1; j <= Tables.UpgradeButton.Get(i)._CategoryNum; j++)
            {
                for (int k = 1; k <= Tables.UpgradeCategory.Get(i * 10000 + j * 100)._CardNum; k++)
                {
                    if (Tables.UpgradeCard.Get(i * 10000 + j * 100 + k)._XpCost == 0)
                    {
                        SaveData.upgradedCard.Add(i * 10000 + j * 100 + k);
                        Debug.Log("Add " + (i * 10000 + j * 100 + k));
                    }
                }
            }
        }

        SaveData.totalXP = 0;
        SaveData.remainXP = 0;
    }
}
