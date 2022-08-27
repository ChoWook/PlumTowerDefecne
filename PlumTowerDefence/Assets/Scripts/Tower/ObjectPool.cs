using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    /*
    // ����� ��
    1. ���� Ǯ��
    2. Ÿ���� �������� ���� ����? �ƴϸ� Tower�ϳ��� ��� Ǯ��?
    3. �߻�ü�� Ÿ������ �ٸ��� ��� �����ؾ��ϳ�?
    4. Pool�� ���� �� ������ �ϳ�?
    5. Dictionary ������ ��� �ϴ� �� ������?
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

    //�ʱ� ���� �Լ�
    private void Initialize(int count)
    {
        for (int i = 0; i < count; i++)
        {
            poolingObjectQueue.Enqueue (CreateNewObject());
        }
    }

    // ������ Ÿ�� �޾ƿ���
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


    // �� ����� Ÿ�� �����ϱ�(�Ǹ�)
    public static void ReturnObject(Tower tower)
    {
        tower.gameObject.SetActive(false);                  //��Ȱ��ȭ
        tower.transform.SetParent(Instance.transform);      //ObjectPool �Ʒ��� �ű��
        Instance.poolingObjectQueue.Enqueue(tower);         //Queue�� �־���
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
