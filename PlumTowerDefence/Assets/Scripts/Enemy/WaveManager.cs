using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{


    // Start is called before the first frame update
    void Awake()
    {
        Tables.Load();
    }

    // Update is called once per frame
   
    private void Update()
    {
         /*if (Input.GetKeyDown(KeyCode.A))
         {
             var enemyHit = GetComponent<Enemy>();
             Debug.Log("a pressed");
             enemyHit.DealDamageForSeconds(30);
             Debug.Log("Shield: " + enemyHit.CurrentShield);
             Debug.Log("HP: " + enemyHit.CurrentHP);
         }*/
    }
}
