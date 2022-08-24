using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUpgrade : MonoBehaviour
{
    [SerializeField] private GameObject[] selectButtons;

    public void SelectButton1()
    {
        selectButtons[0].SetActive(true);
        selectButtons[1].SetActive(false);
        selectButtons[2].SetActive(false);
    }
    public void SelectButton2()
    {
        selectButtons[1].SetActive(true);
        selectButtons[0].SetActive(false);
        selectButtons[2].SetActive(false);
    }
    public void SelectButton3()
    {
        selectButtons[2].SetActive(true);
        selectButtons[0].SetActive(false);
        selectButtons[1].SetActive(false);
    }
}
