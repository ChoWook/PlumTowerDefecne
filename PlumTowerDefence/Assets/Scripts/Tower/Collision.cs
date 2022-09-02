using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    // �� ������ �߰�
    public void OnTriggerEnter(Collider other)
    {
        var obj = other.gameObject;
        if(obj != null && obj.tag == "Enemy")
        {
            this.GetComponentInParent<Tower>().EnemyLIst.Add(obj);
        }
        
    }

    // �� ������ ����
    public void OnTriggerExit(Collider other)
    {
        var obj = other.gameObject;
        if (obj != null && obj.tag == "Enemy" )
        {
            this.GetComponentInParent<Tower>().EnemyLIst.Remove(obj);
        }
    }


}
