using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class Treasure : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Transform CloseCap;

    [SerializeField] Vector3 OpenRotation;

    [SerializeField] float OpenTime;

    int RewardMoney = 0;

    Quaternion CloseRotation;

    float RewardSecondProb = 30;

    float RewardThirdProb = 10;

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        StartCoroutine(IE_CapOpenAnimation());
    }

    IEnumerator IE_CapOpenAnimation()
    {
        CloseRotation = CloseCap.rotation;

        CloseCap.DOLocalRotateQuaternion(Quaternion.Euler(OpenRotation), OpenTime).SetEase(Ease.OutBounce);

        yield return new WaitForSeconds(OpenTime * 1.5f);

        GetReward();

        ObjectPools.Instance.ReleaseObjectToPool(gameObject);

        CloseCap.rotation = CloseRotation;
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

        if(Random.Range(0, 100) < RewardSecondProb)
        {
            // °¡Àå ½Ñ È­»ìÅ¸¿ö ÄíÆù
            GameManager.instance.AddCoupon(ETowerName.Arrow);
        }

        if (Random.Range(0, 100) < RewardSecondProb)
        {
            // 2¹øÂ°·Î ½Ñ Å¸¿öÀÏ °Í°°Àº ÄíÆù
            GameManager.instance.AddCoupon(ETowerName.Gatling);
        }
    }
}
