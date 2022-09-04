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



    // �� ������ �߰�
    public void OnTriggerEnter(Collider other)
    {
        var obj = other.gameObject;
        if(obj != null && obj.CompareTag(enemyTag))
        {
            enemyList.Add(obj);
        }
        
    }

    // �� ������ ����
    public void OnTriggerExit(Collider other)
    {
        var obj = other.gameObject;
        if (obj != null && obj.tag == "Enemy" )
        {
            enemyList.Remove(obj);
        }
    }


}
