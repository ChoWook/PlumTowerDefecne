using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSound : MonoBehaviour, IPointerEnterHandler
{
    public GameObject Canvas;

    private void OnEnable()
    {
        if(Canvas == null)
            Canvas = GameObject.Find("UICanvas");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Canvas.GetComponent<SoundPlay>().Play();
    }
}
