using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{

    public string enemyTag = "Enemy";

    [SerializeField]Tower tower;

    List<GameObject> enemyList;

    private void Awake()
    {
       // tower = GetComponentInParent<Tower>();

        enemyList = tower.EnemyLIst;
        
    }



    // 적 들어오면 추가
    public void OnTriggerEnter(Collider other)
    {
        var obj = other.gameObject;
        if(obj != null && obj.CompareTag(enemyTag))
        {
            enemyList.Add(obj);
        }
        
    }

    // 적 나가면 삭제
    public void OnTriggerExit(Collider other)
    {
        var obj = other.gameObject;
        if (obj != null && obj.tag == "Enemy" )
        {
            enemyList.Remove(obj);
        }
    }


}
