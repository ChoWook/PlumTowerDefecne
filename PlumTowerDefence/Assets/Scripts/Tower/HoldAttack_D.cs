using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    // 애니메이터 연관 




public class HoldAttack : MonoBehaviour
{
    public TowerScript towerScript;
    private GameObject closestEnemy;
    private Transform[] spawnpointChildren;
    private float timer = 1;
    private float startTime;
    private float reloadTimer;
    private bool projectileExists = false;
    private TowerClass towerClass;
    private GameObject currentProjectile;


    
    void Start()
    {
        towerClass = new TowerClass();
        startTime = timer;
        reloadTimer = towerScript.reloadDelay; //재장전 시간 -> 발사 속도
        if (towerScript.lookTowardsObj != null)
        {
            towerScript.lookTowardsObj.rotationSpeed = towerScript.rotationSpeed;
        }

        towerScript.controller = GetComponent<Animator>();  // 애니메이터
    }

    void Update()
    {
        towerClass.AnimatorExists(towerScript.controller, false); //애니메이터

        if (closestEnemy == null)
        {
            closestEnemy = towerClass.CheckForEnemies(transform.position, towerScript.attackRange, towerScript.lookTowardsObj);
        }

        if (closestEnemy != null)
        {
            var diffDistance = Vector3.Distance(transform.position, closestEnemy.transform.position);
            if (diffDistance > towerScript.attackRange)
            {
                if (towerScript.lookTowardsObj != null)
                {
                    towerScript.lookTowardsObj.ga = null;
                }

                closestEnemy = null;
            }
            else if(!towerScript.isStationary)
            {
                var closestEnemyPosition = closestEnemy.transform.position;
                var lookPos = towerScript.flipRotation ? closestEnemyPosition - transform.position : transform.position - closestEnemyPosition;
                var rot = Quaternion.LookRotation(lookPos);
                transform.rotation = Quaternion.Slerp(transform.rotation, rot, towerScript.rotationSpeed * Time.deltaTime);
            }
        }

        if (reloadTimer <= 0)
        {
            spawnpointChildren = towerScript.projectileSpawnPoint.GetComponentsInChildren<Transform>();

            if (!projectileExists)
            {
                towerScript.projectile.GetComponent<ScaleOverTime>().scaleUpSpeed = towerScript.scaleUpSpeed;
                towerScript.projectile.GetComponentInChildren<ScaleOverTime>().scaleTo = towerScript.projectileSize;
                currentProjectile = Instantiate(towerScript.projectile, towerScript.projectileSpawnPoint);
                currentProjectile.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

                projectileExists = true;

            }
            else if (closestEnemy != null && projectileExists && spawnpointChildren[1].transform.localScale.x >= 1)
            {
                if (timer <= 0)
                {
                    SetProjectileValues();

                    timer = startTime;
                    projectileExists = false;
                    reloadTimer = towerScript.reloadDelay;
                }
                else
                {
                    timer -= towerScript.attackSpeed * Time.deltaTime;
                }
            }
        }
        else
        {
            reloadTimer -= Time.deltaTime;
        }
    }

    private void SetProjectileValues()
    {
        currentProjectile.GetComponentInChildren<MoveForward>().speed = towerScript.projectileVelocity;
        currentProjectile.GetComponentInChildren<MoveForward>().target = closestEnemy;
        currentProjectile.GetComponentInChildren<UnParentTimer>().timer = towerScript.releaseTimer;
        currentProjectile.GetComponentInChildren<UnParentTimer>().enabled = true;

        if (currentProjectile.GetComponentInChildren<TargetHit>())
        {
            currentProjectile.GetComponentInChildren<TargetHit>().damage = towerScript.attackDamage;
            currentProjectile.GetComponentInChildren<TargetHit>().target = closestEnemy;
            currentProjectile.GetComponentInChildren<TargetHit>().scaleDownSpeed = towerScript.shrinkSpeed;
        }

        if (currentProjectile.GetComponentInChildren<ExplosionScript>())
        {
            currentProjectile.GetComponentInChildren<ExplosionScript>().damage = towerScript.attackDamage;
            currentProjectile.GetComponentInChildren<ExplosionScript>().target = closestEnemy;
            currentProjectile.GetComponentInChildren<ExplosionScript>().explosionRadius = towerScript.explosionRadius;
            currentProjectile.GetComponentInChildren<ExplosionScript>().scaleDownSpeed = towerScript.shrinkSpeed;
        }

        if (towerScript.shootParticle != null)
        {
            GameObject ps = Instantiate(towerScript.shootParticle, towerScript.psSpawnPoint);
            ParticleSystem parts = ps.GetComponent<ParticleSystem>();
            float totalDuration = parts.main.duration;
            Destroy(ps, totalDuration);
        }

        towerClass.AnimatorExists(towerScript.controller, true); // 애니메이터
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
*/
