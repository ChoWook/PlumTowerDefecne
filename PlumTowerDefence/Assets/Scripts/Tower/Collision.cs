using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    // 적 들어오면 추가
    public void OnTriggerEnter(Collider other)
    {
        var obj = other.gameObject;
        if(obj != null && obj.tag == "Enemy")
        {
            this.GetComponentInParent<Tower>().EnemyLIst.Add(obj);
        }
        
    }

    // 적 나가면 삭제
    public void OnTriggerExit(Collider other)
    {
        var obj = other.gameObject;
        if (obj != null && obj.tag == "Enemy" )
        {
            this.GetComponentInParent<Tower>().EnemyLIst.Remove(obj);
        }
    }


}
