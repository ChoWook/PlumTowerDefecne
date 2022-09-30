using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySound : MonoBehaviour
{

    private AudioSource theAudio;
    [SerializeField] private AudioClip deadClip;
    [SerializeField] private AudioClip attackClip;
    // Start is called before the first frame update
    void Awake()
    {
        theAudio = GetComponent<AudioSource>();
    }

    public void DeadEnemySound()
    {
        theAudio.clip = deadClip;
        theAudio.Play();
    }

    public void AttackEnemySound()
    {
        theAudio.clip = attackClip;
        theAudio.Play();
    }
}
