using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPools : MonoBehaviour
{
    public static ObjectPools Instance;
    
    private void Start()
    {
        Instance = this;

        CreateMultiplePoolObjects();
    }

    [SerializeField] GameObject[] poolPrefabs;
    public int poolingCount;

    private Dictionary<object, List<GameObject>> pooledObjects = new Dictionary<object, List<GameObject>>();

    public void CreateMultiplePoolObjects()
    {
        for (int i = 0; i < poolPrefabs.Length; i++)
        {
            for (int j = 0; j < poolingCount; j++)
            {
                if (!pooledObjects.ContainsKey(poolPrefabs[i].name))
                {
                    List<GameObject> newList = new List<GameObject>();
                    pooledObjects.Add(poolPrefabs[i].name, newList);
                }

                GameObject newDoll = Instantiate(poolPrefabs[i], Instance.transform);
                newDoll.SetActive(false);
                pooledObjects[poolPrefabs[i].name].Add(newDoll);
            }
        }
    }

    // 오브젝트를 풀에서 가져옴
    public GameObject GetPooledObject(string _name)
    {

        if (pooledObjects.ContainsKey(_name))
        {
            for (int i = 0; i < pooledObjects[_name].Count; i++)
            {
                if (!pooledObjects[_name][i].activeSelf)
                {
                    pooledObjects[_name][i].SetActive(true);
                    return pooledObjects[_name][i];
                }
            }
		
	    // 용량이 꽉차 새로운 오브젝트를 생성할 필요가 생김
            int beforeCreateCount = pooledObjects[_name].Count;

            CreateMultiplePoolObjects();

            pooledObjects[_name][beforeCreateCount].SetActive(true);
            return pooledObjects[_name][beforeCreateCount];
        }
        else
        {
            return null;
        }
    }
    
    // 오브젝트를 해제해 풀로 되돌려 놓음
    public void ReleaseObjectToPool(GameObject go)
    {
        go.SetActive(false);
        go.transform.SetParent(Instance.transform);
        go.transform.localPosition = Vector3.zero;
    }
}