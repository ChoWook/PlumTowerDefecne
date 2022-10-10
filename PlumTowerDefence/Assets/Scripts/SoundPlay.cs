using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundPlay : MonoBehaviour
{
    public static bool IsOn = true;

    public bool IsLoop = false;

    public bool Is3DSound = true;

    public bool IsPlayOnAwake = false;

    AudioSource Source;

    float BaseVolume;

    private void Awake()
    {
        Source = GetComponent<AudioSource>();

        //Source.loop = IsLoop;
        SetLoop(IsLoop);


        Source.spatialBlend = (Is3DSound)? 1 : 0;

        Source.playOnAwake = IsPlayOnAwake;

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

    public void SetLoop(bool _loop)
    {
        Source.loop = _loop;
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
