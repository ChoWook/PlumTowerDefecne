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
    }

    IEnumerator IE_CapOpenAnimation()
    {
        CloseCap.DOLocalRotateQuaternion(Quaternion.Euler(OpenRotation), OpenTime).SetEase(Ease.OutBounce);

        yield return new WaitForSeconds(OpenTime * 1.5f);

        // TODO ���ڸ� ������ �� ���� �ൿ ������ ��

        ObjectPools.Instance.ReleaseObjectToPool(gameObject);
    }
}
