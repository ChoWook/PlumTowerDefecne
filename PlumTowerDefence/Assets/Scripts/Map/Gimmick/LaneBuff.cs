using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneBuff : MonoBehaviour
{
    ELaneBuffType Type;

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
}
