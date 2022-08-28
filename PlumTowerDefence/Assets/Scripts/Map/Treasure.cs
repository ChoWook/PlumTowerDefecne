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

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        StartCoroutine(IE_CapOpenAnimation());

        // TODO 상자를 열었을 때 행할 행동 만들어야 함
    }

    IEnumerator IE_CapOpenAnimation()
    {
        CloseCap.DOLocalRotateQuaternion(Quaternion.Euler(OpenRotation), OpenTime).SetEase(Ease.OutBounce);

        yield return OpenTime * 1.5;

        //ObjectPools.Instance.ReleaseObjectToPool(gameObject);
    }
}
