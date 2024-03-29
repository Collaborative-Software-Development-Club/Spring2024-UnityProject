using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProjectile
{
    public enum ProjectileType {
        Normal,
        Fire,
        Ice,
        Poison
    }

    public ProjectileType Type {get; set;}
    public float BaseDamage {get; set;}
    public float ProjectileSpeed {get; set;}
    public void SetProperties(ProjectileType type, float damage, float speed);
}
