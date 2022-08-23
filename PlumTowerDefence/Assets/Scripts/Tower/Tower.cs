using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    /*
     �� ��
    1. Ÿ�� ��ü Ư��
     - �Ӽ�
     - 
     
    2. Ÿ�� ����
     
     */

    // ���� : �Ӽ�, ���ݷ�, ��Ÿ�, ũ��, Ÿ�� ����, ���׷��̵� �ܰ�, ���� ����(�⺻, ����, ����� ��), �̵� ����


    // Ÿ�� ����

    private int TowerID;                            // Ÿ�� ID (������ ���̺�)
    public string TowerName;                        // Ÿ�� �̸�
    private int TypeID;                             // �Ӽ� ID (���������̺�)
    private int SizeID;                             // Ÿ�� ũ�� (���������̺�)
    private int AttackStat ;                        // ���ݷ� ����(���������̺�)
    private int SpeedStat ;                         // ���� �ӵ� ����(���������̺�)
    public float range = 15f;                            // ���� ��Ÿ�

    // �� ���� (Ÿ�� ����)

    private Transform target;

    public string enemyTag = "Enemy";

    // todo �� ���� ���� ����



    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0, 0.5f); // 0.5�� ���� �ݺ��ϱ�
    }

    // Ÿ�� ������Ʈ
    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag); // Enemy  �±׷� �� ã��
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position); // ������ �Ÿ� ���ϱ�
            if(distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            return;
        }
        else
        {
            target = null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }


}
