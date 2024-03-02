using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WaveManager : MonoBehaviour
{
    public int waveFrequence;
    [SerializeField] private int waveCount;
    public Transform spawningPoint;
    public GameObject enemyPrefab;
    public List<int> wavesCurrency;
    public List<EnemyTypes.EnemyType> spawningEnemyList;


    // Start is called before the first frame update
    void Start()
    {
        spawningEnemyList = new List<EnemyTypes.EnemyType>();
        StartCoroutine(WaveTimer());


    }

    IEnumerator WaveTimer()
    {
        while (waveCount < wavesCurrency.Count)
        {
            
            yield return new WaitForSeconds(waveFrequence);
            SetUpEnemyListByCurrency(waveCount);
            StartSpawningEnemies();
            waveCount++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartNextWave()
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

            GameObject enemy = Instantiate(enemyPrefab, spawningPoint); // spawn enemy object
            
            


            enemy.name = enemyType.ToString(); // set enemy object name
            EnemyController enemyController = enemy.GetComponent<EnemyController>();
            EnemyInfo newEnemyInfo = EnemyManager.Instance.GetEnemyInfo(enemyType);
            enemyController.SetEnemyInfo(newEnemyInfo);
            GameObject enemyModel = Instantiate(newEnemyInfo.model, enemy.transform);
            Rigidbody enemyRigidbody = enemy.GetComponent<Rigidbody>();
            enemyRigidbody.useGravity = newEnemyInfo.hasGravity;

            Vector3 scaleSize = new Vector3(3, 4, 3);


            if (newEnemyInfo.type == EnemyTypes.EnemyType.Armored)
            {
                BoxCollider enemyBoxCollider = enemy.GetComponent<BoxCollider>();
                enemyBoxCollider.size = Vector3.Scale(scaleSize, enemy.transform.localScale);
            }



            //Debug.Log(enemyType);
            yield return new WaitForSeconds(3f); // time space for spawning
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
        


        List<EnemyTypes.EnemyType> sortedEnemy = EnemyManager.Instance.GetEnemyListSortedByCostDecrease();
        
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
                Debug.Log(enemyNum);
                
            }
            waveCurrency -= enemyNum * enemyCost;
            while (enemyNum != 0)
            {
                spawningEnemyList.Add(enemyType);
                //Debug.Log("Added: " + enemyType);
                enemyNum--;
            }
           

        }
    }



}


// To do next time: add enemy model