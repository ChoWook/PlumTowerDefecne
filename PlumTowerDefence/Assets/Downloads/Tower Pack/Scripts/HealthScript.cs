using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    public int health;
    public GameObject blood;
    private int startHealth;

    void Start()
    {
        startHealth = health;
    }

    public void GiveFullHealth()
    {
        health = startHealth;
    }

    public void DealDamage(int damage, Vector3 projectileLoc)
    {
        var diff = projectileLoc - transform.position;
        if(diff != Vector3.zero)
        {
            var lookRotation = Quaternion.LookRotation(diff);

            if (blood != null)
            {
                GameObject ps = Instantiate(blood, transform.position, lookRotation);
                ParticleSystem parts = ps.GetComponent<ParticleSystem>();
                float totalDuration = parts.main.duration;
                Destroy(ps, totalDuration);
            }
        }

        health -= damage;
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
