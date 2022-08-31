using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

/*
public class TowerScript : MonoBehaviour
{
    [Header("Tower Stats")] 
    [Tooltip("Attack Damage adjusts the amount of damage the projectile will deal to the enemy")]
    public int attackDamage = 1; // 공격 AttackStat
    [Tooltip("Attack Speed adjusts how often the tower instantiates a new projectile")]
    public float attackSpeed = 1; // 재장전 속도?
    [Tooltip("Projectile Velocity adjusts the travel speed of the projectile")]
    public float projectileVelocity = 10; // 
    [Tooltip("Attack Range adjusts how far the tower detects enemies")]
    public float attackRange = 1; // 사거리
    [Tooltip("Explosion Radius adjusts how big the explosion will be")]
    public float explosionRadius = 1; // 폭발 범위

    [Header("Hold Attack Settings")]
    [Tooltip("Reload Delay adjusts the time before reloading the tower with a new projectile, *this affect how fast the tower is firing!")]
    public float reloadDelay = 1; // 재장전 딜레이
    [Tooltip("Release Timer is the the time it take for the projectile to let go of the spawnpoint")]
    public float releaseTimer = 0.1f; // ?

    [Header("Projectile")] // 투사체 설정
    [Tooltip("Projectile is the prefab you wish to shoot from the tower")]
    public GameObject projectile; // 투사체 오브젝트
    [Tooltip("Projectile Spawn Point is the location where the projectile will be instantiated at")]
    public Transform projectileSpawnPoint; // 투사체 리스폰 위치

    [Header("Shoot Particle")] // 모르겠음!
    [Tooltip("Shoot Particle plays when firing a projectile, for example a muzzle flash")]
    public GameObject shootParticle;
    [Tooltip("Ps SpawnPoint is the the location where the projectile will be instantiated at")]
    public Transform psSpawnPoint;

    [Header("Projectile Settings")] // 투사체 설정
    [Tooltip("Projectile Size adjusts the size of the projectile")]
    public Vector3 projectileSize = new Vector3(1, 1, 1); // 투사체 크기
    [Tooltip("Scale Up Speed adjusts how quickly a projectile scales up when it is first instantiated, *This affects how fast the tower is firing!")]
    public float scaleUpSpeed = 1; // 크기 증가 속도
    [Tooltip("Shrink Speed adjusts how quickly a projectile shrinks to nothingness when it hits a target")]
    public float shrinkSpeed = 1; // 타겟에 닿았을 때 줄어드는 속도

    [Header("Other Settings")] 
    [Tooltip("Rotation Speed adjusts how quick the tower rotates towards the enemy")]
    public float rotationSpeed = 10; // 타워 회전 속도
    [Tooltip("Controller will default to the animator on the gameobject, if there is no controller leave this empty")]
    public Animator controller; // 애니메이터 컨트롤러
    [Tooltip("Attach a LookTowards Script on the base of the tower if you want the base to track the closest enemy")]
    public LookTowardsObject lookTowardsObj;
    [Tooltip("If the tower is looking away from the target then enable this to flip the tower to look the right direction")]
    public bool flipRotation = false;
    [Tooltip("If the tower isn't suppose to move or look around check this box to make the tower stationary")]
    public bool isStationary = false;
}
*/
