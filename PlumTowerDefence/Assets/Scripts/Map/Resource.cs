using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class Resource : MonoBehaviour, IPointerClickHandler
{
    public EResourceType ResourceType = EResourceType.Magnetite;

    [SerializeField] GameObject[] Resources;

    [SerializeField] GameObject[] Pickaxes;

    //List<Transform> PickaxePoint = new();

    public Ease AnimationEase = Ease.Linear;

    float[] Probs;

    int MiningCnt;              // 자원을 몇 번 캘 수 있는지

    int MiningMoney;

    float AnimationTime = 0.5f;

    float MiningTime = 5;

    float MiningAngle = -100;

#if UNITY_EDITOR
    private void Update()
    {
        //UpdateResourceType();
    }
#endif

    private void OnEnable()
    {
        InitResource();

        InitPickaxe();
    }

    void InitResource()
    {
        Probs = new float[Resources.Length];

        Tables.Load();

        for (int i = 0; i < Probs.Length; i++)
        {
            Probs[i] = Tables.MapGimmickResource.Get(i + 1)._Probability;
        }

        int min = Tables.GlobalSystem.Get("Mining_Num_Min")._Value;

        int max = Tables.GlobalSystem.Get("Mining_Num_Max")._Value + 1;

        MiningCnt = Random.Range(min, max);

        SetResourceType(ChooseType());

        MiningMoney = Tables.MapGimmickResource.Get(ResourceType)._MiningMoney;
    }

    void InitPickaxe()
    {
        for (int i = 0; i < Pickaxes.Length; i++)
        {
            Pickaxes[i].SetActive(false);
        }
    }

    int ChooseType()
    {
        float total = 0;

        foreach (float elem in Probs)
        {
            total += elem;
        }

        float randomPoint = Random.Range(0, total);

        for (int i = 0; i < Probs.Length; i++)
        {
            if (randomPoint < Probs[i])
            {
                return i;
            }
            else
            {
                randomPoint -= Probs[i];
            }
        }
        return Probs.Length - 1;
    }

    public void SetResourceType(int Sender)
    {
        ResourceType = (EResourceType)(Sender + 1);

        UpdateResourceType();
    }

    public void UpdateResourceType()
    {
        for(int i = 0; i < Resources.Length; i++)
        {
            if(Resources[i].name.CompareTo(ResourceType.ToString()) == 0)
            {
                Resources[i].SetActive(true);
            }
            else
            {
                Resources[i].SetActive(false);
            }
        }
    }

    public void MiningResource()
    {
        StartCoroutine(IE_MiningAnimation());

        StartCoroutine(IE_Mining());
    }

    IEnumerator IE_MiningAnimation()
    {
        WaitForSeconds ws = new WaitForSeconds(AnimationTime);

        while (true)
        {
            InitPickaxe();

            int PickaxeNum = Random.Range(0, Pickaxes.Length);

            Pickaxes[PickaxeNum].SetActive(true);

            Transform PTransfrom = Pickaxes[PickaxeNum].transform;

            PTransfrom.rotation = Quaternion.Euler(0, PTransfrom.rotation.eulerAngles.y, 0);

            PTransfrom.DORotate(new Vector3(0, PTransfrom.rotation.eulerAngles.y, MiningAngle), AnimationTime)
                .SetEase(AnimationEase);

            yield return ws;
        }
    }

    IEnumerator IE_Mining()
    {
        WaitForSeconds ws = new WaitForSeconds(MiningTime);

        for(int i = 0; i < MiningCnt; i++)
        {
            GameManager.instance.money += MiningMoney;

            // TODO 돈이 증가되는 애니메이션 +~라는 UI를 게임상에 표시해줄 필요가 있을 것 같음

            yield return ws;
        }
    }


    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        MiningResource();
    }
}
