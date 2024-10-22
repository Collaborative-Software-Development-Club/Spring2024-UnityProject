﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyController : MonoBehaviour
{
    public EnemyInfo enemyInfo;

    //for moving
    public NodesManager nodesManager;
    public Transform target;
    public int nodesNum = 0;
    public List<Transform> nodeList;
    public int pathInt = 0;

    //for taking damage
    public float moreDamageScale = 2.0f;
    public float immueDamageScale = 0.0f;
    public bool isHit = false;
    
    

    private void Start()
    {
        nodesManager = GameObject.Find("NodesManager").GetComponent<NodesManager>();
        transform.localScale = new Vector3(enemyInfo.scale, enemyInfo.scale, enemyInfo.scale);
        nodeList = nodesManager.GetNodesInPath(pathInt);
        target = nodeList[0].transform;
    }
    private void Update()
    {

        // moving to target until reach the end

        if (nodesNum < nodeList.Count)
        {

            Vector3 dir = target.position - transform.position;
            dir.y = 0;
            transform.Translate(dir.normalized * enemyInfo.speed * Time.deltaTime, Space.World);

            // change target
            if (Vector3.Distance(transform.position, target.position) <= 10f && nodesNum < nodeList.Count)
            {
                nodesNum++;
                if (nodesNum < nodeList.Count)
                {
                    target = nodeList[nodesNum].transform;
                }
            }
        }


        //destroy when health reach 0
        if (enemyInfo.health <= 0)
        {
            DestroyThisUnit();
        }
    }

    public void DestroyThisUnit()
    {
        Destroy(gameObject);
    }

    public void LosingHealth(float losedHealth)
    {
        enemyInfo.health -= losedHealth;
    }

    public void SetEnemyInfo(EnemyInfo newEnemyInfo)
    {
        if (enemyInfo == null)
        {
            enemyInfo = gameObject.AddComponent<EnemyInfo>();
        }

        enemyInfo.type = newEnemyInfo.type;
        enemyInfo.health = newEnemyInfo.health;
        enemyInfo.speed = newEnemyInfo.speed;
        enemyInfo.cost = newEnemyInfo.cost;
        enemyInfo.damage = newEnemyInfo.damage;
        enemyInfo.model = newEnemyInfo.model;
        enemyInfo.propertyType = newEnemyInfo.propertyType;
        enemyInfo.scale = newEnemyInfo.scale;
        enemyInfo.hasGravity = newEnemyInfo.hasGravity;

    }


    // for detect projectile and lose health
    public void TakeDamageFromProjectile(IProjectile.ProjectileType projectileType, float baseDamage)
    {
        //float losingHealth = 0.0f;
        EnemyTypes.EnemyType enemyType = enemyInfo.type;
        float damageScale = 1.0f;

        // for Spore Guy
        if (enemyType == EnemyTypes.EnemyType.SporeGuy)
        {
            if (projectileType == IProjectile.ProjectileType.Poison)
            {
                damageScale = immueDamageScale;
            }

            if (projectileType == IProjectile.ProjectileType.Fire)
            {
                damageScale = moreDamageScale;
            }
        }

        // for Floating Eye
        if (enemyType == EnemyTypes.EnemyType.FloatingEye)
        {
            if (projectileType == IProjectile.ProjectileType.Normal)
            {
                damageScale = immueDamageScale;
            }

        }

        // for Armored
        if (enemyType == EnemyTypes.EnemyType.Armored)
        {
            if (projectileType == IProjectile.ProjectileType.Normal)
            {
                damageScale = immueDamageScale;
            }

            if (projectileType == IProjectile.ProjectileType.Poison)
            {
                damageScale = moreDamageScale;
            }
        }

        // for Ice Mage
        if (enemyType == EnemyTypes.EnemyType.IceMage)
        {
            if (projectileType == IProjectile.ProjectileType.Ice)
            {
                damageScale = immueDamageScale;
            }

            if (projectileType == IProjectile.ProjectileType.Fire)
            {
                damageScale = moreDamageScale;
            }
        }

        // for Lava Hount
        if (enemyType == EnemyTypes.EnemyType.LavaHound)
        {
            if (projectileType == IProjectile.ProjectileType.Fire)
            {
                damageScale = immueDamageScale;
            }

            if (projectileType == IProjectile.ProjectileType.Ice)
            {
                damageScale = moreDamageScale;
            }
        }

        float finalDamage = baseDamage * damageScale;

        Debug.LogFormat("Enemy Type: {0}, Projectile Type: {1}, Base Damage: {2}, Damage Scale: {3}, Final Damage: {4} ",
                    enemyType, projectileType, baseDamage, damageScale, finalDamage);


        LosingHealth(finalDamage);


    }




}
