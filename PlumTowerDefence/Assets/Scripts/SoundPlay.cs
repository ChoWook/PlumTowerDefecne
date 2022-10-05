using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundPlay : MonoBehaviour
{
    public static bool IsOn = true;

    AudioSource Source;

    float BaseVolume;

    private void Awake()
    {
        Source = GetComponent<AudioSource>();

        Source.loop = false;

        BaseVolume = Source.volume;
    }

    private void OnEnable()
    {
        SetVolume((IsOn) ? 1 : 0);
    }

    public void Play()
    {
        if (IsOn)
        {
            Source.Play();
        }
    }

    public void Stop()
    {
        Source.Stop();
    }

    public void SetVolume(float Sender)
    {
        if(Sender > 1)
        {
            Sender = 1;
        }

        Source.volume = Sender * BaseVolume;
    }
}
