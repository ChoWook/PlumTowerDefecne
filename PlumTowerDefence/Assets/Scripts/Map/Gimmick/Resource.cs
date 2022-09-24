using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using TMPro;

public class Resource : IObjectOnTile, IPointerClickHandler
{

    [SerializeField] GameObject[] Resources;

    [SerializeField] GameObject[] Pickaxes;

    //List<Transform> PickaxePoint = new();

    public EResourceType ResourceType = EResourceType.Magnetite;

    public Ease AnimationEase = Ease.Linear;

    float[] Probs;

    int MiningCnt;              // 자원을 몇 번 캘 수 있는지

    int MiningMoney;

    readonly float AnimationTime = 0.5f;

    float MiningTime = 5;

    readonly float MiningAngle = -100;

    private bool IsMining = false;

    #region Unity Function
#if UNITY_EDITOR
    //private void Update()
    //{
        //UpdateResourceType();
    //}
#endif

    private void OnEnable()
    {
        InitResource();

        InitPickaxe();
    }

    #endregion

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

        IsMining = false;
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
        IsMining = true;
        
        StartCoroutine(nameof(IE_MiningAnimation));

        StartCoroutine(nameof(IE_Mining));
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

            // 확장 단계에서는 자원 캐지지 말아야 함
            while (!GameManager.instance.isPlayingGame)
            {
                yield return null;
            }

            yield return ws;

            while (!GameManager.instance.isPlayingGame)
            {
                yield return null;
            }
        }
    }

    IEnumerator IE_Mining()
    {
        WaitForSeconds ws = new WaitForSeconds(MiningTime);

        for(int i = 0; i < MiningCnt; i++)
        {
            // 확장 단계에서는 자원 캐지지 말아야 함
            while (!GameManager.instance.isPlayingGame)
            {
                yield return null;
            }

            yield return ws;

            while (!GameManager.instance.isPlayingGame)
            {
                yield return null;
            }

            GameManager.instance.money += MiningMoney;

            // 증가된 돈 표시
            GameObject obj = ObjectPools.Instance.GetPooledObject("BonusText");

            var text = obj.GetComponent<BonusText>();

            text.SetPosition(transform.position);

            text.AddText(string.Format(Tables.StringUI.Get("Treasure_Money")._Korean, MiningMoney));

        }

        ObjectPools.Instance.ReleaseObjectToPool(gameObject);
    }


    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        if(!IsMining)
        {
            UIManager.instance.ShowMiningUI(this);
        }
    }

    public void SetPickaxae(EPickaxeType Sender)
    {
        for(int i = 0; i < Pickaxes.Length; i++)
        {
            Pickaxes[i].GetComponentInChildren<Pickaxe>()?.SetPickaxeType(Sender);
        }

        MiningTime = Tables.Pickaxe.Get(Sender)._MiningSpeed;
    }
}
