using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPools : MonoBehaviour
{
    static ObjectPools _Instance;

    public static ObjectPools Instance
    {
        get
        {
            return _Instance;
        }

    }

    private void Start()
    {
        if (_Instance == null)
        {
            _Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            if (_Instance != this)
            {
                Destroy(this);
            }
        }
    }

    public GameObject[] poolPrefabs;
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

    // 오브젝트를 풀에서 가져옴ㄴ
    public GameObject GetPooledObject(string _name)
    {
        if (pooledObjects.ContainsKey(_name))
        {
            for (int i = 0; i < pooledObjects[_name].Count; i++)
            {
                if (!pooledObjects[_name][i].activeSelf)
                {
                    return pooledObjects[_name][i];
                }
            }
		
	    // 용량이 꽉차 새로운 오브젝트를 생성할 필요가 생김
            int beforeCreateCount = pooledObjects[_name].Count;

            CreateMultiplePoolObjects();

            return pooledObjects[_name][beforeCreateCount];
        }
        else
        {
            return null;
        }
    }
    
    // 오브젝트를 해제해 풀로 되돌려 놓음
    public void ReleaseObject(GameObject go)
    {
        go.SetActive(false);
        go.transform.parent = Instance.transform;
        go.transform.localPosition = Vector3.zero;
    }
}
