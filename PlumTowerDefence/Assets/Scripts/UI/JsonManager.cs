using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JsonManager : MonoBehaviour
{
    public static JsonManager instance = null;

    public List<int> upgradedCard;

    private void Awake()
    {
        instance = this;
        upgradedCard = new List<int>();

        LoadJson();
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
    }
}
