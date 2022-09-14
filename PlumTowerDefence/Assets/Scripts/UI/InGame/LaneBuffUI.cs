using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LaneBuffUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI LaneBuffNameText;

    [SerializeField] TextMeshProUGUI LaneBuffDescriptionText;

    [SerializeField] TextMeshProUGUI LaneBuffEffectText;


    LaneBuff _LaneBuff;


    public void SetLaneBuff(LaneBuff Sender)
    {
        _LaneBuff = Sender;

        UpdateInfo();
    }

    void UpdateInfo()
    {
        var buff = Tables.MonsterLaneBuff.Get(_LaneBuff.Type);

        // TODO StringUI에 추가 필요
        if (buff._IsBuff)
        {
            LaneBuffNameText.text = "몬스터 버프";
        }
        else
        {
            LaneBuffNameText.text = "몬스터 디버프";
        }
        

        LaneBuffDescriptionText.text = buff._Korean;

        if(buff._Amount > 0)
        {
            LaneBuffEffectText.text = $"{buff._StatType.ToString()} +{buff._Amount}";
        }
        else
        {
            LaneBuffEffectText.text = $"{buff._StatType.ToString()} {buff._Amount}";
        }
        
    }
}
