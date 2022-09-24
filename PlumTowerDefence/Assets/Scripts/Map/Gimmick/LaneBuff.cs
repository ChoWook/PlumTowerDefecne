using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LaneBuff : IObjectOnTile, IPointerClickHandler
{
    [SerializeField] GameObject BuffEffect;

    [SerializeField] GameObject DeBuffEffect;

    public ELaneBuffType Type;


    private void OnEnable()
    {
        InitLaneBuff();
    }

    void InitLaneBuff()
    {
        SetType((ELaneBuffType)Random.Range(1, 29));
    }

    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();

        if (enemy == null)
        {
            return;
        }

         enemy.TakeBuff(Type);
    }

    public void SetType(ELaneBuffType Sender)
    {
        Type = Sender;

        if (Tables.MonsterLaneBuff.Get(Type)._IsBuff)
        {
            BuffEffect.SetActive(true);
            DeBuffEffect.SetActive(false);
        }
        else
        {
            BuffEffect.SetActive(false);
            DeBuffEffect.SetActive(true);
        }
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        UIManager.instance.ShowLaneBuffUI(this);
    }
}
