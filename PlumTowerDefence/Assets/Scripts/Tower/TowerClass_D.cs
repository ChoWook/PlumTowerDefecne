using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
public class TowerClass
{
    
    public GameObject SetClosestEnemy(List<GameObject> enemies, Vector3 origin)
    {
        GameObject closestEnemy = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = origin;
        foreach (GameObject enemy in enemies)
        {
            Vector3 directionToTarget = enemy.transform.position - currentPosition;
            float dSqrToEnemy = directionToTarget.sqrMagnitude;
            if (dSqrToEnemy < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToEnemy;
                closestEnemy = enemy.gameObject;
            }
        }

        return closestEnemy;
    }

    public GameObject CheckForEnemies(Vector3 originPoint, float radius, LookTowardsObject lookTowards)
    {
        Collider[] nearbyTargets = Physics.OverlapCapsule(originPoint + new Vector3(0, 20, 0), originPoint + new Vector3(0, -20, 0), radius);
        List<GameObject> nearbyEnemies = new List<GameObject>();
        foreach (var target in nearbyTargets)
        {
            if (target.tag == "enemy")
            {
                nearbyEnemies.Add(target.gameObject);
            }
        }

        GameObject closestEnemy = SetClosestEnemy(nearbyEnemies, originPoint);
        if (closestEnemy != null && lookTowards != null)
            lookTowards.GetComponent<LookTowardsObject>().ga = closestEnemy.transform;

        return closestEnemy;
    }

    public void AnimatorExists(Animator controller, bool isShooting)
    {
        if (controller != null)
            controller.SetBool("isShooting", isShooting);
    }

    
}

*/
