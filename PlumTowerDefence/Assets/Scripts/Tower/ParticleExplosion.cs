using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleExplosion : MonoBehaviour
{
    SoundPlay Source;

    AudioSource AudioSource;

    public AudioClip[] Sounds;
    // Start is called before the first frame update

    ETowerName towername;

    private void Awake()
    {
        AudioSource = GetComponent<AudioSource>();
        Source = GetComponent<SoundPlay>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fire(float time, ETowerName name)
    {
        towername = name;

        if (towername == ETowerName.Missile)
        {
            AudioSource.clip = Sounds[0];
        }
        else if (towername == ETowerName.Cannon)
        {
            AudioSource.clip = Sounds[1];
        }
        else if (towername == ETowerName.Bomb)
        {
            AudioSource.clip = Sounds[2];
        }


        Source.Play();
        Debug.Log("Sound! ");
        StartCoroutine(IE_psDelay(time));
    }

    IEnumerator IE_psDelay(float time)
    {
        WaitForSeconds ws = new(time);

        yield return ws;

        if(towername == ETowerName.Missile)
        {
            ObjectPools.Instance.ReleaseObjectToPool(gameObject);
        }
        else if (towername == ETowerName.Cannon)
        {
            gameObject.SetActive(false);
        }
        
    }
}
