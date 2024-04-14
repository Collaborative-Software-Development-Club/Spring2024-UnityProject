using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class EnemyController : MonoBehaviour
{
    public EnemyInfo enemyInfo;


    // for displaying damage
    //[SerializeField] private Transform healthBarRed;
    //[SerializeField] private Transform healthBar;
    [SerializeField] private GameObject damageDisplayTextPrefab;

    public HealthBarController healthBarController;

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
        healthBarController = GetComponent<HealthBarController>();

    }
    private void Update()
    {

        // moving to target until reach the end

        if (nodesNum < nodeList.Count)
        {

            Vector3 dir = target.position - transform.position;
            dir.y = 0;

            // rotate when walking
            if (dir != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(dir.normalized);
                lookRotation *= Quaternion.Euler(0, 90, 0);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10); //10 is turn speed
            }



            transform.Translate(dir.normalized * enemyInfo.speed * Time.deltaTime, Space.World);

            // change target
            if (Vector3.Distance(transform.position, target.position) <= 10f && nodesNum < nodeList.Count)
            {
                if (nodesNum < nodeList.Count - 1)
                {
                    nodesNum++;
                }
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

        // losing health
        enemyInfo.health -= losedHealth;

        // desplay health bar
        healthBarController.UpdateHealth(enemyInfo.health);




        //desplay losing health text
        BoxCollider enemyBoxCollider = GetComponent<BoxCollider>();
        Vector3 topCenterPosition = enemyBoxCollider.bounds.center + new Vector3(0, enemyBoxCollider.bounds.extents.y + 2, 0);
        GameObject dmgNumber = Instantiate(damageDisplayTextPrefab, topCenterPosition, Quaternion.identity);
        DamageDisplayText damageDisplayText= dmgNumber.GetComponent<DamageDisplayText>();
        damageDisplayText.SetDamageText(losedHealth);

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

        Debug.LogFormat("Enemy Type: {0}, Projectile Type: {1}, Base Damage: {2}, Damage Scale: {3}, Final Damage: {4}, Enemy Health: {5} ",
                    enemyType, projectileType, baseDamage, damageScale, finalDamage, enemyInfo.health);


        LosingHealth(finalDamage);

        
        


    }


    // Set Grounded to prevent flying caused by collision
    public void SetGrounded()
    {
        Rigidbody enemyRigidbody = GetComponent<Rigidbody>();
        enemyRigidbody.constraints = RigidbodyConstraints.FreezePositionY;
    }

}
