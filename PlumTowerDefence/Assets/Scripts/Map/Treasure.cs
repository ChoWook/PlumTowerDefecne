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

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        StartCoroutine(IE_CapOpenAnimation());
    }

    IEnumerator IE_CapOpenAnimation()
    {
        CloseCap.DOLocalRotateQuaternion(Quaternion.Euler(OpenRotation), OpenTime).SetEase(Ease.OutBounce);

        yield return new WaitForSeconds(OpenTime * 1.5f);

        GetReward();

        ObjectPools.Instance.ReleaseObjectToPool(gameObject);
    }

    void GetReward()
    {
        RewardMoney = (int)(GameManager.instance.level
            * Tables.MapGimmickResource.Get(EResourceType.Magnetite)._MiningMoney
            * 0.8f);

        GameManager.instance.money += RewardMoney;

    }
}
