using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    /*
    // 고려할 것
    1. 다중 풀링
    2. 타워를 종류별로 나눠 관리? 아니면 Tower하나로 묶어서 풀링?
    3. 발사체는 타워마다 다른데 어떻게 관리해야하나?
    4. Pool을 여러 개 만들어야 하나?
    5. Dictionary 관리는 어떻게 하는 게 좋을까?
     */
   
    public static ObjectPool Instance;

    [SerializeField]
    private GameObject poolingObjectPrefab;

    private Queue<Tower> poolingObjectQueue = new Queue<Tower> ();

    private void Awake()
    {
        Instance = this;

        Initialize(10);
    }

    private Tower CreateNewObject()
    {
        var newObj = Instantiate(poolingObjectPrefab, transform).GetComponent<Tower>();
        newObj.gameObject.SetActive(false);
        return newObj;
    }

    //초기 생성 함수
    private void Initialize(int count)
    {
        for (int i = 0; i < count; i++)
        {
            poolingObjectQueue.Enqueue (CreateNewObject());
        }
    }

    // 생성된 타워 받아오기
    public static Tower GetObject()
    {
        if(Instance.poolingObjectQueue.Count > 0)
        {
            var obj = Instance.poolingObjectQueue.Dequeue();
            obj.transform.SetParent(null);
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            var newobj = Instance.CreateNewObject();
            newobj.transform.SetParent(null);
            newobj.gameObject.SetActive(true);
            return newobj;
        }
    }


    // 다 사용한 타워 리턴하기(판매)
    public static void ReturnObject(Tower tower)
    {
        tower.gameObject.SetActive(false);                  //비활성화
        tower.transform.SetParent(Instance.transform);      //ObjectPool 아래로 옮기고
        Instance.poolingObjectQueue.Enqueue(tower);         //Queue에 넣어줌
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
