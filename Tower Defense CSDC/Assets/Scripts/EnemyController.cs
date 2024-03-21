using UnityEngine;
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
    public float moreDamageScale = 1.25f;
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
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            Debug.Log("Taking Damage");
            ProjectileBehavior projectileBehavior;
            isHit = collision.gameObject.TryGetComponent<ProjectileBehavior>(out projectileBehavior);

            if (isHit)
            {


                if (!projectileBehavior.Type.Equals(enemyInfo.type))
                {
                    LosingHealth(projectileBehavior.BaseDamage);
                }
            }
        }
    }




}
