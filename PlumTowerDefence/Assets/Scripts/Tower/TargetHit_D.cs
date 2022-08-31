using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
public class TargetHit : MonoBehaviour
{
    public int damage;
    public GameObject target;
    public float scaleDownSpeed;

    private void OnTriggerEnter(Collider other)
    {
        if (target == other.gameObject)
        {
            other.GetComponent<HealthScript>().DealDamage(damage, transform.position);
            gameObject.AddComponent<ScaleDownDestroy>().scaleDownSpeed = scaleDownSpeed;

            //Removes particle system after the duration on the particle system
            if(transform.GetChild(0).GetComponent<ParticleSystem>())
            {
                ParticleSystem parts = transform.GetChild(0).GetComponent<ParticleSystem>();
                float totalDuration = parts.main.duration;

                Destroy(parts, totalDuration);
            }

            //Stop the projectile from moving forward when it has hit a target
            Destroy(GetComponent<MoveForward>());
        }
    }
}
*/
