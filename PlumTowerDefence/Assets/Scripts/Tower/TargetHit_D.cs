using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
public class TargetHit : MonoBehaviour
{
    public int damage;
    public GameObject target;
    public float scaleDownSpeed;

    private void OnTriggerEnter(Collider other) // 불릿 적에 닿으면 사라지기!
    {
        if (target == other.gameObject)
        {
            other.GetComponent<HealthScript>().DealDamage(damage, transform.position); // 데미지 넣기
            gameObject.AddComponent<ScaleDownDestroy>().scaleDownSpeed = scaleDownSpeed; // ?

            // Removes particle system after the duration on the particle system (Particle system을 재장전 시간동안 멈추기)
            if(transform.GetChild(0).GetComponent<ParticleSystem>()) // particle 을 재장전 시간 동안 없앤다?
            {
                ParticleSystem parts = transform.GetChild(0).GetComponent<ParticleSystem>();
                float totalDuration = parts.main.duration;

                Destroy(parts, totalDuration);
            }

            // Stop the projectile from moving forward when it has hit a target (앞으로 나아가는 모션을 끝낸다.)
            Destroy(GetComponent<MoveForward>()); 
        }
    }
}
*/
