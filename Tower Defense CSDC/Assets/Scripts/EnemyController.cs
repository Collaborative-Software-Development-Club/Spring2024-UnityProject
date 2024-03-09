using UnityEngine;
using System.Collections;
using System.Collections.Generic;


// for controlling movement
public class EnemyController : MonoBehaviour
{
    public EnemyInfo enemyInfo;
    public NodesManager nodesManager;
    public Transform target;
    public int nodesNum = 0;
    public GameObject nodePath;
    

    private void Start()
    {
        nodesManager = GameObject.Find("NodesManager").GetComponent<NodesManager>();
        transform.localScale = new Vector3(enemyInfo.scale, enemyInfo.scale, enemyInfo.scale);
        target = nodePath.transform.Find("StartNode");
        //nodePaths[0] = GameObject.Find("Path1");
        //nodePaths[1] = GameObject.Find("Path2");
        //nodePaths[2] = GameObject.Find("Path3");
        //nodePaths[3] = GameObject.Find("Path4");
        //nodePaths[4] = GameObject.Find("Path5");

    }
    private void Update()
    {

        // moving to target until reach the end

        if (nodesNum < nodePath.transform.childCount)
        {

            Vector3 dir = target.position - transform.position;
            dir.y = 0;
            transform.Translate(dir.normalized * enemyInfo.speed * Time.deltaTime, Space.World);

            // change target
            if (Vector3.Distance(transform.position, target.position) <= 10f && nodesNum < nodePath.transform.childCount)
            {
                nodesNum++;
                if (nodesNum < nodePath.transform.childCount)
                {
                    target = nodePath.transform.GetChild(nodesNum);
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
        enemyInfo.propertyType = newEnemyInfo.propertyType;
        enemyInfo.scale = newEnemyInfo.scale;
        enemyInfo.hasGravity = newEnemyInfo.hasGravity;


    }

    public void SpawnEnemy()
    {
        //int randint = UnityEngine.Random.Range(0, nodePaths.Length - 1);

        //nodePath = nodePaths[randint];

        //Transform spawningPoint = nodePath.transform.Find("StartNode");


        //if (spawningPoint is null)
        //{
        //    Debug.Log("null");
        //}



        //gameObject.name = enemyInfo.type.ToString(); // set enemy object name
        //EnemyInfo newEnemyInfo = enemyInfo;
        //SetEnemyInfo(newEnemyInfo);
        //GameObject enemyModel = Instantiate(newEnemyInfo.model, gameObject.transform);
        //Rigidbody enemyRigidbody = gameObject.GetComponent<Rigidbody>();
        //enemyRigidbody.useGravity = newEnemyInfo.hasGravity;


        //// just for armored
        //Vector3 scaleSize = new Vector3(3, 4, 3);


        //if (newEnemyInfo.type == EnemyTypes.EnemyType.Armored)
        //{
        //    BoxCollider enemyBoxCollider = gameObject.GetComponent<BoxCollider>();
        //    enemyBoxCollider.size = Vector3.Scale(scaleSize, gameObject.transform.localScale);
        //}


    }



}
