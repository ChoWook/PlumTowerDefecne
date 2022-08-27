using System.Collections.Generic;
using UnityEngine;

public class MemoryPool
{
    // 메모리 풀로 관리되는 오브젝트 정보를 저장
    private class PoolItem
    {
        public bool isActive;               // "gameObject의 활성화/비활성화 정보
        public GameObject gameObject;       // 화면에 보이는 실제 게임 오브젝트
    }

    private int increaseCount = 6;          // 오브젝트가 부족할 때 Instantiate()로 추가 생성되는 오브젝트 개수
    private int maxCount;                   // 현재 리스트에 등록되어 있는 오브젝트 개수
    private int activeCount;                // 현재 게임에 사용되고 있는(활성화) 오브젝트 개수
    
    private GameObject poolObject;          // 오브젝트 풀링에서 관리하는 게임 오브젝트 프리팹
    private List<PoolItem> poolItemList;    // 관리되는 모든 오브젝트를 저장하는 리스트

    public MemoryPool(GameObject poolObject)// 메모리풀 생성자 --> 변수 초기화 + instantiate 함수 호출
    {
        maxCount = 0;
        activeCount = 0;
        this.poolObject = poolObject;

        poolItemList = new List<PoolItem>();

        InstatiateObjects();                // 최초 5개의 발사체 미리 생성
    }


    // increaseCount 단위로 오브젝트 생성
    public void InstatiateObjects()
    {
        maxCount += increaseCount;

        for (int i = 0; i < increaseCount; ++i)
        {
            PoolItem poolItem = new PoolItem();

            poolItem.isActive = false;
            poolItem.gameObject = GameObject.Instantiate(poolObject);   // 오브젝트 생성
            poolItem.gameObject.SetActive(false);

            poolItemList.Add(poolItem);                                 // 해당 정보를 poolItem 리스트에 저장
        }

    }

    // 현재 관리중인(활성/비활성) 모든 오브젝트를 삭제

    public void DestroyObjects()
    {
        if (poolItemList == null) return;

        int count = poolItemList.Count;

        for (int i = 0; i < count; ++i)
        {
            GameObject.Destroy(poolItemList[i].gameObject);     // 씬이 바뀌거나 게임이 종료될 때 한번만 호출 
        }

        poolItemList.Clear();
    }

    // poolItemList에 저장되어 있는 오브젝트를 활성화 해서 사용
    // 현재 모든 오브젝트가 사용중이면 InstantiateObjects()로 추가 생성

    public GameObject ActivatePoolItem()
    {
        if (poolItemList == null) return null;

        // 현재 생성해서 관리하는 모든 오브젝트 개수와 현재 활성화 상태인 오브젝트 개수 비교
        // 모든 오브젝트가 활성화 상태이면 새로운 오브젝트 필요
        if (maxCount == activeCount)
        {
            InstatiateObjects();
        }

        int count = poolItemList.Count;

        for (int i = 0; i < count; ++i)
        {
            // 리스트 탐색 --> 현재 비활성화 오브젝트를 찾아 활성화시키고 사용
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

    // 현재 사용이 완료된 오브젝트를 비활성 상태로 설정

    public void DeactivatePoolItem(GameObject removeObject)
    {
        if (poolItemList == null || removeObject == null) return;

        int count = poolItemList.Count;
        for (int i = 0; i < count; ++i)              // 리스트 내에서
        {
            PoolItem poolItem = poolItemList[i];

            if (poolItem.gameObject == removeObject) // removeObject와 동일한 요소를 찾는다
            {
                activeCount--;

                poolItem.isActive = false;
                poolItem.gameObject.SetActive(false);

                return;
            }
        }
    }

    // 게임에 사용중인 모든 오브젝트를 비활성화 상태로 설정

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