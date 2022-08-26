using System.Collections.Generic;
using UnityEngine;

public class MemoryPool
{
    // �޸� Ǯ�� �����Ǵ� ������Ʈ ������ ����
    private class PoolItem
    {
        public bool isActive;               // "gameObject�� Ȱ��ȭ/��Ȱ��ȭ ����
        public GameObject gameObject;       // ȭ�鿡 ���̴� ���� ���� ������Ʈ
    }

    private int increaseCount = 6;          // ������Ʈ�� ������ �� Instantiate()�� �߰� �����Ǵ� ������Ʈ ����
    private int maxCount;                   // ���� ����Ʈ�� ��ϵǾ� �ִ� ������Ʈ ����
    private int activeCount;                // ���� ���ӿ� ���ǰ� �ִ�(Ȱ��ȭ) ������Ʈ ����
    
    private GameObject poolObject;          // ������Ʈ Ǯ������ �����ϴ� ���� ������Ʈ ������
    private List<PoolItem> poolItemList;    // �����Ǵ� ��� ������Ʈ�� �����ϴ� ����Ʈ

    public MemoryPool(GameObject poolObject)// �޸�Ǯ ������ --> ���� �ʱ�ȭ + instantiate �Լ� ȣ��
    {
        maxCount = 0;
        activeCount = 0;
        this.poolObject = poolObject;

        poolItemList = new List<PoolItem>();

        InstatiateObjects();                // ���� 5���� �߻�ü �̸� ����
    }


    // increaseCount ������ ������Ʈ ����
    public void InstatiateObjects()
    {
        maxCount += increaseCount;

        for (int i = 0; i < increaseCount; ++i)
        {
            PoolItem poolItem = new PoolItem();

            poolItem.isActive = false;
            poolItem.gameObject = GameObject.Instantiate(poolObject);   // ������Ʈ ����
            poolItem.gameObject.SetActive(false);

            poolItemList.Add(poolItem);                                 // �ش� ������ poolItem ����Ʈ�� ����
        }

    }

    // ���� ��������(Ȱ��/��Ȱ��) ��� ������Ʈ�� ����

    public void DestroyObjects()
    {
        if (poolItemList == null) return;

        int count = poolItemList.Count;

        for (int i = 0; i < count; ++i)
        {
            GameObject.Destroy(poolItemList[i].gameObject);     // ���� �ٲ�ų� ������ ����� �� �ѹ��� ȣ�� 
        }

        poolItemList.Clear();
    }

    // poolItemList�� ����Ǿ� �ִ� ������Ʈ�� Ȱ��ȭ �ؼ� ���
    // ���� ��� ������Ʈ�� ������̸� InstantiateObjects()�� �߰� ����

    public GameObject ActivatePoolItem()
    {
        if (poolItemList == null) return null;

        // ���� �����ؼ� �����ϴ� ��� ������Ʈ ������ ���� Ȱ��ȭ ������ ������Ʈ ���� ��
        // ��� ������Ʈ�� Ȱ��ȭ �����̸� ���ο� ������Ʈ �ʿ�
        if (maxCount == activeCount)
        {
            InstatiateObjects();
        }

        int count = poolItemList.Count;

        for (int i = 0; i < count; ++i)
        {
            // ����Ʈ Ž�� --> ���� ��Ȱ��ȭ ������Ʈ�� ã�� Ȱ��ȭ��Ű�� ���
            PoolItem poolItem = poolItemList[i];
            if (poolItem.isActive == false)
            {
                activeCount++;

                poolItem.isActive = true;
                poolItem.gameObject.SetActive(true);

                return poolItem.gameObject;
            }
        }
        return null;
    }

    // ���� ����� �Ϸ�� ������Ʈ�� ��Ȱ�� ���·� ����

    public void DeactivatePoolItem(GameObject removeObject)
    {
        if (poolItemList == null || removeObject == null) return;

        int count = poolItemList.Count;
        for (int i = 0; i < count; ++i)              // ����Ʈ ������
        {
            PoolItem poolItem = poolItemList[i];

            if (poolItem.gameObject == removeObject) // removeObject�� ������ ��Ҹ� ã�´�
            {
                activeCount--;

                poolItem.isActive = false;
                poolItem.gameObject.SetActive(false);

                return;
            }
        }
    }

    // ���ӿ� ������� ��� ������Ʈ�� ��Ȱ��ȭ ���·� ����

    public void DeactivateAllPoolItems()
    {
        if (poolItemList == null) return;

        int count = poolItemList.Count;
        for (int i = 0; i < count; ++i)
        {
            PoolItem poolItem = poolItemList[i];

            if (poolItem.gameObject != null && poolItem.isActive == true)
            {
                poolItem.isActive = false;
                poolItem.gameObject.SetActive(false);
            }
        }
        activeCount = 0;
    }

}