using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyController : MonoBehaviour
{
    public EnemyInfo enemyInfo;
    

    private void Start()
    {

    }
    private void Update()
    {

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
