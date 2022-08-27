using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    public float scaleDownSpeed;
    public float explosionRadius;
    public int damage;
    public GameObject target;
    public GameObject explosion;

    private void OnTriggerEnter(Collider other)
    {
        if (target == other.gameObject)
        {
            Collider[] nearbyTargets = Physics.OverlapSphere(transform.position, explosionRadius);
            foreach(var target in nearbyTargets)
            {
                if (target.CompareTag("enemy"))
                    target.GetComponent<HealthScript>().DealDamage(damage, transform.position);
            }

            if(explosion != null)
            {
                GameObject ps = Instantiate(explosion, transform.position, transform.rotation);
                ps.transform.localScale = new Vector3(explosionRadius, explosionRadius, explosionRadius) / 4;
                ParticleSystem parts = ps.GetComponent<ParticleSystem>();
                float totalDuration = parts.main.duration;
                Destroy(ps, totalDuration);
            }

            Destroy(GetComponent<MoveForward>());
            gameObject.AddComponent<ScaleDownDestroy>().scaleDownSpeed = scaleDownSpeed;

            Destroy(GetComponent<ExplosionScript>());
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(
            Vector3.zero,
            Vector3.up
        );

        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
