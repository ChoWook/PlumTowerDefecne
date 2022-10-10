using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeadSound : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<SoundPlay>().Play();
    }
}
