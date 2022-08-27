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


    // Ÿ�� ���� (�ӽ÷� ȭ��Ÿ�� ���� ����)

    [SerializeField]
    private int TowerID = 0;                            // Ÿ�� ID (������ ���̺�)


    public string TowerName = "ȭ��Ÿ��";                        // Ÿ�� �̸�
    private int AttackPropertyID = 0;                   // ���� �Ӽ�(���������̺�)
    private int TypeID = 0;                             // �Ӽ� ID (���������̺�)
    private int SizeID = 0;                             // Ÿ�� ũ�� (���������̺�)
    private int AttackStat = 25;                       // ���ݷ� ����(���������̺�)
    private int SpeedStat = 4;                         // ���� �ӵ� ����(���������̺�)
    private int AbilityStat;                        // Ư�� �ɷ� ����(���������̺�)
    public int Range = 3;                       // ���� ��Ÿ�
    private int UpgradePrice = 40;                       // ���׷��̵� ����(���������̺�)
    private int Price = 100;                              // ���� ����(���������̺�)
    private double Damage;                             // ������ 
    private int SellPrice;                             // �Ǹ� ����


    // �� ���� (Ÿ�� ����)

    public Transform Target;

    public string enemyTag = "Enemy";

    public int DefenceStat = 10;

    // todo �� ���� ���� ����



    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0, 0.5f); // 0.5�� ���� �ݺ��ϱ�
    }

    // Ÿ�� ������Ʈ (ü�� �켱, �� �켱, ���� ���� �� �켱 �߰��ϱ�)
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

        if (nearestEnemy != null && shortestDistance <= Range)
        {
            Target = nearestEnemy.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Target == null)
        {
            return;
        }
        else
        {
            Target = null;
        }
    }

    // ��Ÿ� ǥ��
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Range);
    }


    // Ÿ�� ���� �Ӽ��� ������ ���� (�� ��ȹ�� ���� �ʿ�)
    private void SetDamageWithAttackProperty()
    {
        // DefenceStat ���� ��� ������ ���� �ʿ���.

        switch(AttackPropertyID)
        {
            case 0:                                // Ÿ���� ���ݷ� x {0.01x (100 - ���� ����)} ��ŭ ��󿡰� �������� �ش�.
                Damage = AttackStat;
                break;
            case 1:                               // ü�¿� Ÿ���� ���ݷ� x 1.2 x{0.01x(100-����)} ��ŭ ��󿡰� �������� �ش�, ������ �⺻ �˰���� ����

                break;
            case 2 :

                break;

            case 3 :
                break;
                
        }    
    }



}
