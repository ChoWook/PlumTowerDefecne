using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using TMPro;
using UnityEngine.Rendering;
using Random = UnityEngine.Random;

public class Treasure : IObjectOnTile, IPointerClickHandler
{
    [SerializeField] Transform CloseCap;

    [SerializeField] Vector3 OpenRotation;

    [SerializeField] float OpenTime;

    int RewardMoney = 0;

    Quaternion CloseRotation;

    float RewardSecondProb = 30;

    float RewardThirdProb = 10;

    private static Camera mainCamera;
    private static Camera UICamera;

    private static bool CameraLoad = false;

    bool TreasureOpening = false;

    private void Awake()
    {
        if (!CameraLoad)
        {
            mainCamera = Camera.main;
            UICamera = GameObject.Find("UICam").GetComponent<Camera>();
            CameraLoad = true;
        }
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        if(TreasureOpening == false)
        {
            TreasureOpening = true;

            StartCoroutine(nameof(IE_CapOpenAnimation));
        }
    }

    IEnumerator IE_CapOpenAnimation()
    {
        CloseRotation = CloseCap.rotation;

        CloseCap.DOLocalRotateQuaternion(Quaternion.Euler(OpenRotation), OpenTime).SetEase(Ease.OutBounce);

        GetReward();
        
        yield return new WaitForSeconds(OpenTime * 1.5f);

        ObjectPools.Instance.ReleaseObjectToPool(gameObject);

        CloseCap.rotation = CloseRotation;

        TreasureOpening = false;
    }

    void GetReward()
    {
        
        if(GameManager.instance == null)
        {
            return;
        }

        RewardMoney = (int)(GameManager.instance.level
            * Tables.MapGimmickResource.Get(EResourceType.Magnetite)._MiningMoney
            * 0.8f);

        GameManager.instance.money += RewardMoney;
        
        GameObject obj = ObjectPools.Instance.GetPooledObject("BonusText");

        var text = obj.GetComponent<BonusText>();

        text.SetPosition(transform.position);

        text.AddText(string.Format(Tables.StringUI.Get("Treasure_Money")._Korean, RewardMoney));

        RewardSecondProb *= 1 + GameManager.instance.increaseTowerCoupon;
        RewardThirdProb *= 1 + GameManager.instance.increaseTowerCoupon;
        
        if(Random.Range(0, 100) < RewardSecondProb)
        {
            // °¡Àå ½Ñ È­»ìÅ¸¿ö ÄíÆù
            text.AddText("\n" + string.Format(Tables.StringUI.Get("Treasure_Low_Cost_Tower")._Korean,
                Tables.Tower.Get(ETowerName.Arrow)._Korean));

            GameManager.instance.AddCoupon(ETowerName.Arrow);
        }

        if (Random.Range(0, 100) < RewardSecondProb)
        {
            // 2¹øÂ°·Î ½Ñ Å¸¿öÀÏ °Í°°Àº ÄíÆù
            text.AddText("\n" + string.Format(Tables.StringUI.Get("Treasure_Second_Low_Cost_Tower")._Korean,
                Tables.Tower.Get(ETowerName.Hourglass)._Korean));

            GameManager.instance.AddCoupon(ETowerName.Hourglass);
        }
        
    }
}
