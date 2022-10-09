using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MiningUI : MonoBehaviour
{
    [SerializeField] Button[] PickaxeBtns;

    [SerializeField] TextMeshProUGUI ResourceNameText;

    [SerializeField] TextMeshProUGUI MiningDesText;

    Resource _Resource;

    private void Awake()
    {
        for (EPickaxeType PickaxeType = EPickaxeType.Wood; PickaxeType <= EPickaxeType.Black; PickaxeType++)
        {
            EPickaxeType tmp = PickaxeType;

            PickaxeBtns[(int)PickaxeType - 1].onClick.AddListener(() => OnPickaxeSelectBtnClicK(tmp));
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            OnPickaxeSelectBtnClicK(EPickaxeType.Wood);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            OnPickaxeSelectBtnClicK(EPickaxeType.Blue);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            OnPickaxeSelectBtnClicK(EPickaxeType.Red);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            OnPickaxeSelectBtnClicK(EPickaxeType.Black);
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
        if(Tables.Pickaxe.Get(Type)._Price > GameManager.instance.Money)
        {
            return;
        }

        GameManager.instance.Money -= Tables.Pickaxe.Get(Type)._Price;

        _Resource.SetPickaxae(Type);

        _Resource.MiningResource();

        gameObject.SetActive(false);
    }

    public void UpdateInfo()
    {
        for(EPickaxeType PickaxeType = EPickaxeType.Wood; PickaxeType <= EPickaxeType.Black; PickaxeType++)
        {
            PickaxeBtns[(int)PickaxeType - 1].transform.GetComponentInChildren<TextMeshProUGUI>().text
                = $"{Tables.Pickaxe.Get(PickaxeType)._Korean}({Tables.Pickaxe.Get(PickaxeType)._Price})(<color=red>{(int)PickaxeType}</color>)";
        }
        
        ResourceNameText.text = Tables.MapGimmickResource.Get(_Resource.ResourceType)._Korean;

        // TODO String UI에 추가 필요
        MiningDesText.text = "사용할 곡괭이를 선택하세요";
    }
}
