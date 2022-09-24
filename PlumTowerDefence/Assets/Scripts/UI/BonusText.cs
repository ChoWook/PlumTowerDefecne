using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

[RequireComponent(typeof(TextMeshProUGUI))]
public class BonusText : MonoBehaviour
{
    TextMeshProUGUI text;

    static Camera mainCamera;

    static Camera UICamera;

    static bool CameraLoad = false;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();

        mainCamera = Camera.main;
        UICamera = GameObject.Find("UICam").GetComponent<Camera>();
        CameraLoad = true;
    }

    private void OnEnable()
    {
        InitText();
    }

    public void InitText()
    {
        text.text = "";
    }

    public void AddText(string NewLine)
    {
        text.text += NewLine;
    }

    public void SetPosition(Vector3 WorldPosition)
    {
        var TempPosition = mainCamera.WorldToScreenPoint(WorldPosition);
        var ScreenPosition = UICamera.ScreenToWorldPoint(TempPosition);

        transform.position = ScreenPosition;

        transform.SetParent(GameObject.Find("UICanvas").transform);

        transform.localScale = Vector3.one;

        transform.DOLocalMoveY(transform.localPosition.y + 100, 1)
            .OnComplete(() => ObjectPools.Instance.ReleaseObjectToPool(gameObject));
    }
}
