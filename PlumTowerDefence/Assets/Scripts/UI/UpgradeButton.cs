using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class UpgradeButton : MonoBehaviour
{
    [SerializeField] private GameObject _panel;

    public Ease Ease;

    public void MoveMainMenu()
    {
        _panel.transform.DOLocalMoveX(0, 1).SetEase(Ease);
    }

    public void ResetXP()
    {
        Debug.Log("XP Reset");
    }
}
