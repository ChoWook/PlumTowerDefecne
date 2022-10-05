using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using RTS_Cam;

public class Option : MonoBehaviour
{
    /// <summary>
    /// option창을 담당하는 스크립트
    /// </summary>
    /// 

    [SerializeField] Toggle EnvironmentToggle;

    [SerializeField] Toggle MoveScreenMouseToggle;

    [SerializeField] Toggle MoveScreenKeyboardToggle;

    AudioSource BGMSource;

    RTS_Camera Cam;

    public static bool EnvironmentChecked = true;

    public static bool MoveScreenMouseChecked = true;

    public static bool MoveScreenKeyboardChecked = false;

    private void AWake()
    {
        InitToggle();
    }

    void InitToggle()
    {
        EnvironmentToggle.isOn = EnvironmentChecked;

        MoveScreenMouseToggle.isOn = MoveScreenMouseChecked;

        MoveScreenKeyboardToggle.isOn = MoveScreenKeyboardChecked;
    }

    public void OnShowEnvironmentToggleValueChanged(bool Checked)
    {
        EnvironmentChecked = Checked;

        if (Checked)
        {
            Map.Instance?.ShowTreeEnvironmnet();
        }
        else
        {
            Map.Instance?.HideTreeEnvironmnet();
        }
    }

    public void OnShowMonterUIToggleValueChanged(bool Checked)
    {

    }

    public void OnMusicSoundToggleValueChanged(bool Checked)
    {
        BGMSource = Camera.main.GetComponent<AudioSource>();

        if (Checked)
        {
            BGMSource?.Play();
        }
        else
        {
            BGMSource?.Pause();
        }
    }

    public void OnEffectSoundToggleValueChanged(bool Checked)
    {
        var sounds = FindObjectsOfType<SoundPlay>();

        SoundPlay.IsOn = Checked;

        for (int i = 0; i < sounds.Length; i++)
        {
            sounds[i].SetVolume((Checked)? 1 : 0);
        }
    }

    public void OnMoveScreenMouseToggleValueChanged(bool Checked)
    {
        MoveScreenMouseChecked = Checked;

        Cam = Camera.main.GetComponent<RTS_Camera>();

        if (Cam != null)
        {
            Cam.useScreenEdgeInput = Checked;
        }
    }

    public void OnMoveScreenKeyboardToggleValueChanged(bool Checked)
    {
        MoveScreenKeyboardChecked = Checked;

        Cam = Camera.main.GetComponent<RTS_Camera>();

        if (Cam != null)
        {
            Cam.useKeyboardInput = Checked;
        }
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
