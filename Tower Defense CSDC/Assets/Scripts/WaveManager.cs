using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private int waveFrequence;
    [SerializeField] private int waveCount;
    public Transform spawningPoint;
    public GameObject enemyPrefab;

    //public GameObject enemyPrefab;
    public List<int> wavesCurrency;
    public List<EnemyTypes.EnemyType> spawningEnemyList;


    // Start is called before the first frame update
    void Start()
    {
        spawningEnemyList = new List<EnemyTypes.EnemyType>();
        SetUpEnemyListByCurrency(0);
        StartSpawningEnemies();


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartNextWave()
    {

    }

    public void SetSortedAllowedEnemyListByWave()
    {

    }

    public void StartSpawningEnemies()
    {
        StartCoroutine(SpawnEnemyByList());
    }

    IEnumerator SpawnEnemyByList()
    {
        while (spawningEnemyList.Count!= 0)
        {
            System.Random random = new System.Random();
            int nextRandom = random.Next(spawningEnemyList.Count);
            EnemyTypes.EnemyType enemyType = spawningEnemyList[nextRandom];
            spawningEnemyList.RemoveAt(nextRandom); // get random enemy and remove it from spawning list

            GameObject enemy = Instantiate(enemyPrefab, spawningPoint);
            EnemyController enemyController = enemy.GetComponent<EnemyController>();
            EnemyInfo newEnemyInfo = EnemyManager.Instance.GetEnemyInfo(enemyType);
            enemyController.SetEnemyInfo(newEnemyInfo);
            yield return new WaitForSeconds(1f); // time space for spawning
        }

    }

    public void SetUpEnemyListByCurrency(int waveCount)
    {
        if (waveCount >= wavesCurrency.Count)
        {
            Debug.LogError("Wave index out of range.");
            return;
        }
        
        int waveCurrency = wavesCurrency[waveCount];
        


        List<EnemyTypes.EnemyType> sortedEnemy = EnemyManager.Instance.sortedEnemyListByCostDecrease;
        foreach (EnemyTypes.EnemyType enemyType in sortedEnemy)
        {
            
            if (waveCurrency == 0) break;
            EnemyInfo enemyInfo;
            EnemyManager.Instance.enemyDictionary.TryGetValue(enemyType, out enemyInfo);
            int enemyCost = enemyInfo.cost;
            int enemyNum = 0;
            if (enemyCost == 1)
            {
                enemyNum = waveCurrency;
            }
            else {
                enemyNum = UnityEngine.Random.Range(0, waveCurrency / enemyCost);
            }
            waveCurrency -= enemyNum * enemyCost;
            while (enemyNum != 0)
            {
                spawningEnemyList.Add(enemyType);
                enemyNum--;
            }
           

        }
    }



}
