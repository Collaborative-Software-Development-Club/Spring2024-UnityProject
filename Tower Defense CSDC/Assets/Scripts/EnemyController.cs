using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyController : MonoBehaviour
{
    public EnemyInfo enemyInfo;
    public NodesManager nodesManager;
    public Transform target;
    public int nodesNum = 0;
    

    private void Start()
    {
        nodesManager = GameObject.Find("NodesManager").GetComponent<NodesManager>();
        transform.localScale = new Vector3(enemyInfo.scale, enemyInfo.scale, enemyInfo.scale);
        target = nodesManager.nodeList[0].transform;
    }
    private void Update()
    {

        // moving to target until reach the end

        if (nodesNum < nodesManager.nodeList.Length)
        {

            Vector3 dir = target.position - transform.position;
            transform.Translate(dir.normalized * enemyInfo.speed * Time.deltaTime, Space.World);

            // change target
            if (Vector3.Distance(transform.position, target.position) <= 1f && nodesNum < nodesManager.nodeList.Length)
            {
                nodesNum++;
                if (nodesNum < nodesManager.nodeList.Length)
                {
                    target = nodesManager.nodeList[nodesNum].transform;
                }
            }
        }


        //destroy when health reach 0
        if (enemyInfo.health == 0)
        {
            DestroyThisUnit();
        }
    }

    public void DestroyThisUnit()
    {
        Destroy(gameObject);
    }

    public void LosingHealth(int losedHealth)
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


    }



}
