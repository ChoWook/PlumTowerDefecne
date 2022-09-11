using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LaneBuff : MonoBehaviour, IPointerClickHandler
{
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
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        UIManager.instance.ShowLaneBuffUI(this);
    }
}
