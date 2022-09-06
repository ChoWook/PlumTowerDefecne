using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using TMPro;
using UnityEngine.Rendering;
using Random = UnityEngine.Random;

public class Treasure : MonoBehaviour, IPointerClickHandler
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
        StartCoroutine(IE_CapOpenAnimation());
    }

    IEnumerator IE_CapOpenAnimation()
    {
        CloseRotation = CloseCap.rotation;

        CloseCap.DOLocalRotateQuaternion(Quaternion.Euler(OpenRotation), OpenTime).SetEase(Ease.OutBounce);

        GetReward();
        
        yield return new WaitForSeconds(OpenTime * 1.5f);

        ObjectPools.Instance.ReleaseObjectToPool(gameObject);

        CloseCap.rotation = CloseRotation;
    }

    void GetReward()
    {
        var TempPosition = mainCamera.WorldToScreenPoint(transform.position);
        var ScreenPosition = UICamera.ScreenToWorldPoint(TempPosition);
        
        if(GameManager.instance == null)
        {
            return;
        }

        RewardMoney = (int)(GameManager.instance.level
            * Tables.MapGimmickResource.Get(EResourceType.Magnetite)._MiningMoney
            * 0.8f);

        GameManager.instance.money += RewardMoney;
        
        GameObject obj = ObjectPools.Instance.GetPooledObject("TreasureText");
        obj.transform.position = ScreenPosition;
        obj.transform.SetParent(GameObject.Find("UICanvas").transform);
        obj.transform.localScale = Vector3.one;
        
        TextMeshProUGUI textMeshProUGUI = obj.GetComponent<TextMeshProUGUI>();

       textMeshProUGUI.text = string.Format(Tables.StringUI.Get("Treasure_Money")._Korean, RewardMoney);
        
        
        if(Random.Range(0, 100) < RewardSecondProb)
        {
            // °¡Àå ½Ñ È­»ìÅ¸¿ö ÄíÆù
            textMeshProUGUI.text += "\n" + string.Format(Tables.StringUI.Get("Treasure_Low_Cost_Tower")._Korean,
                Tables.Tower.Get(ETowerName.Arrow)._Korean);
            GameManager.instance.AddCoupon(ETowerName.Arrow);
        }

        if (Random.Range(0, 100) < RewardSecondProb)
        {
            // 2¹øÂ°·Î ½Ñ Å¸¿öÀÏ °Í°°Àº ÄíÆù
            textMeshProUGUI.text += "\n" + string.Format(Tables.StringUI.Get("Treasure_Secod_Low_Cost_Toer")._Korean,
                Tables.Tower.Get(ETowerName.Hourglass)._Korean);
            GameManager.instance.AddCoupon(ETowerName.Hourglass);
        }
        
        obj.transform.DOLocalMoveY(obj.transform.localPosition.y + 100, 1).OnComplete(() => ObjectPools.Instance.ReleaseObjectToPool(obj));
    }
}
