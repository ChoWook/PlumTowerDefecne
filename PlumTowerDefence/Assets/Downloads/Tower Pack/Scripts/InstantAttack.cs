using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InstantAttack : MonoBehaviour
{
    public TowerScript towerScript;
    private GameObject closestEnemy;
    private Transform[] spawnpointChildren;
    private float timer = 1;
    private float startTime;
    private TowerClass towerClass;

    void Start()
    {
        startTime = timer;
        if(towerScript.lookTowardsObj != null)
        {
            towerScript.lookTowardsObj.rotationSpeed = towerScript.rotationSpeed;
        }

        towerClass = new TowerClass();
    }

    void Update()
    {
        towerClass.AnimatorExists(towerScript.controller, false);

        if(closestEnemy == null)
        {
            closestEnemy = towerClass.CheckForEnemies(transform.position, towerScript.attackRange, towerScript.lookTowardsObj);
        }

        if (closestEnemy != null)
        {
            var distance = Vector3.Distance(transform.position, closestEnemy.transform.position);
            if (distance > towerScript.attackRange)
            {
                if(towerScript.lookTowardsObj != null)
                {
                    towerScript.lookTowardsObj.ga = null;
                }

                closestEnemy = null;
            }
            else if (!towerScript.isStationary)
            {
                var closestEnemyPosition = closestEnemy.transform.position;
                var lookPos = towerScript.flipRotation ? closestEnemyPosition - transform.position : transform.position - closestEnemyPosition;
                var rot = Quaternion.LookRotation(lookPos);
                transform.rotation = Quaternion.Slerp(transform.rotation, rot, towerScript.rotationSpeed * Time.deltaTime);
            }
        }

        if (closestEnemy != null)
        {
            spawnpointChildren = towerScript.projectileSpawnPoint.GetComponentsInChildren<Transform>();

            if (timer <= 0)
            {
                ActivateProjectile();
                timer = startTime;
            }
            else
            {
                timer -= Time.deltaTime * towerScript.attackSpeed;
            }
        }
    }

    private void ActivateProjectile()
    {
        GameObject projectile = Instantiate(towerScript.projectile, towerScript.projectileSpawnPoint);
        projectile.GetComponentInChildren<ScaleOverTime>().scaleTo = towerScript.projectileSize;
        projectile.GetComponentInChildren<ScaleOverTime>().scaleUpSpeed = towerScript.scaleUpSpeed;
        projectile.GetComponentInChildren<MoveForward>().speed = towerScript.projectileVelocity;
        projectile.GetComponentInChildren<MoveForward>().target = closestEnemy;
        projectile.GetComponentInChildren<UnParentTimer>().enabled = true;
        projectile.GetComponentInChildren<UnParentTimer>().enabled = true;

        if (projectile.GetComponentInChildren<TargetHit>())
        {
            projectile.GetComponentInChildren<TargetHit>().damage = towerScript.attackDamage;
            projectile.GetComponentInChildren<TargetHit>().target = closestEnemy;
            projectile.GetComponentInChildren<TargetHit>().scaleDownSpeed = towerScript.shrinkSpeed;
        }

        if (projectile.GetComponentInChildren<ExplosionScript>())
        {
            projectile.GetComponentInChildren<ExplosionScript>().damage = towerScript.attackDamage;
            projectile.GetComponentInChildren<ExplosionScript>().target = closestEnemy;
            projectile.GetComponentInChildren<ExplosionScript>().explosionRadius = towerScript.explosionRadius;
            projectile.GetComponentInChildren<ExplosionScript>().scaleDownSpeed = towerScript.shrinkSpeed;
        }

        if (towerScript.shootParticle != null)
        {
            GameObject ps = Instantiate(towerScript.shootParticle, towerScript.psSpawnPoint);
            ParticleSystem parts = ps.GetComponent<ParticleSystem>();
            float totalDuration = parts.main.duration;
            Destroy(ps, totalDuration);
        }

        towerClass.AnimatorExists(towerScript.controller, true);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(
            Vector3.zero,
            Vector3.up
        );
        Gizmos.DrawWireSphere(transform.position, towerScript.attackRange);
    }
}
