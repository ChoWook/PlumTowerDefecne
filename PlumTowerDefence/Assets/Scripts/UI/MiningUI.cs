using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MiningUI : MonoBehaviour
{
    [SerializeField] Button[] PickaxeBtns;

    [SerializeField] TextMeshProUGUI ResourceNameText;

    Resource _Resource;

    private void Awake()
    {
        for (EPickaxeType PickaxeType = EPickaxeType.Wood; PickaxeType <= EPickaxeType.Black; PickaxeType++)
        {
            EPickaxeType tmp = PickaxeType;

            PickaxeBtns[(int)PickaxeType - 1].onClick.AddListener(() => OnPickaxeSelectBtnClicK(tmp));
        }
    }

    public void SetResource(Resource Sender)
    {
        _Resource = Sender;

        UpdateInfo();
    }

    public void OnCancelBtnClick()
    {
        gameObject.SetActive(false);
    }

    public void OnPickaxeSelectBtnClicK(EPickaxeType Type)
    {
        if(Tables.Pickaxe.Get(Type)._Price > GameManager.instance.money)
        {
            return;
        }

        GameManager.instance.money -= Tables.Pickaxe.Get(Type)._Price;

        _Resource.SetPickaxae(Type);

        _Resource.MiningResource();

        gameObject.SetActive(false);
    }

    public void UpdateInfo()
    {
        for(EPickaxeType PickaxeType = EPickaxeType.Wood; PickaxeType <= EPickaxeType.Black; PickaxeType++)
        {
            PickaxeBtns[(int)PickaxeType - 1].transform.GetComponentInChildren<TextMeshProUGUI>().text
                = $"{Tables.Pickaxe.Get(PickaxeType)._Korean}({Tables.Pickaxe.Get(PickaxeType)._Price})";
        }
        
        ResourceNameText.text = Tables.MapGimmickResource.Get(_Resource.ResourceType)._Korean;
    }
}
