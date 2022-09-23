using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
    /// <summary>
    /// option창을 담당하는 스크립트
    /// </summary>
    /// 

    [SerializeField] Toggle ShowMonsterUIToggle;

    [SerializeField] Toggle MusicSoundToggle;

    [SerializeField] Toggle EffectSoundToggle;


    AudioSource BGMSource;

    private void Awake()
    {
        BGMSource = Camera.main.GetComponent<AudioSource>();
    }

    public void OnShowMonterUIToggleValueChanged(bool Checked)
    {

    }

    public void OnMusicSoundToggleValueChanged(bool Checked)
    {
        if (Checked)
        {
            BGMSource.Play();
        }
        else
        {
            BGMSource.Pause();
        }
    }

    public void OnEffectSoundToggleValueChanged(bool Checked)
    {
        
    }

    public void ShowOption()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    public void CloseOption()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
}
