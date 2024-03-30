using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour, IProjectile
{
    public IProjectile.ProjectileType Type {get; set;}
    public float BaseDamage {get; set;}
    public float ProjectileSpeed {get; set;}

    [HideInInspector] public bool markForDestroy = false;
    void OnTriggerEnter(Collider col) {
        markForDestroy = true;

        // damage enemy
        GameObject targetEnemy = col.gameObject;
        EnemyController targetEnemyController = targetEnemy.GetComponent<EnemyController>();
        targetEnemyController.TakeDamageFromProjectile(Type, BaseDamage);

        //Debug.LogFormat("Type: {0}, Damage: {1}, Speed: {2}", Type, BaseDamage, ProjectileSpeed);
    }
    public void SetProperties(IProjectile.ProjectileType type, float damage, float speed) {
        Type = type;
        BaseDamage = damage;
        ProjectileSpeed = speed;
    }
}
