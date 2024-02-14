using UnityEngine;
using System.Collections;

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
        enemyInfo = newEnemyInfo;
    }



}
