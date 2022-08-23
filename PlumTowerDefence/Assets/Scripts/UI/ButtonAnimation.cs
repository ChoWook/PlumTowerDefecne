using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAnimation : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;  //0번 : idle , 1번 : onMouse
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnMouseEnter()
    {
        spriteRenderer.sprite = sprites[1];
    }

    private void OnMouseExit()
    {
        spriteRenderer.sprite = sprites[0];
    }
}
