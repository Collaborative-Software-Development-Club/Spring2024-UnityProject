using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceProjectileBehavior : MonoBehaviour, IProjectile
{
    public IProjectile.ProjectileType Type {get; set;}
    public float BaseDamage {get; set;}
    public float ProjectileSpeed {get; set;}

    [HideInInspector] public bool markForDestroy = false;
    void OnTriggerEnter(Collider col) {
        markForDestroy = true;
    }
    public void SetProperties(IProjectile.ProjectileType type, float damage, float speed) {
        Type = type;
        BaseDamage = damage;
        ProjectileSpeed = speed;
    }
}
