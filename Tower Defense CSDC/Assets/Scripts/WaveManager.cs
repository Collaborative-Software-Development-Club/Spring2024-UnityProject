using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WaveManager : MonoBehaviour
{
    public int waveFrequence;
    [SerializeField] private int waveCount;
    public Transform EnemyListTransform;
    public GameObject enemyPrefab;
    public List<int> wavesCurrency;
    public List<EnemyTypes.EnemyType> spawningEnemyList;
    public GameObject[] nodePaths;


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
            EnemyInfo enemyInfo = EnemyManager.Instance.GetEnemyInfo(enemyType);

            GameObject enemy = Instantiate(enemyPrefab, EnemyListTransform);

            enemy.name = enemyInfo.type.ToString(); // set enemy object name
            EnemyInfo newEnemyInfo = enemyInfo;

            // set up controller
            EnemyController enemyController = enemy.AddComponent<EnemyController>();
            enemyController.SetEnemyInfo(newEnemyInfo);
            int randint = UnityEngine.Random.Range(0, nodePaths.Length - 1);
            enemyController.nodePath = nodePaths[randint];

            // set up model
            GameObject enemyModel = Instantiate(newEnemyInfo.model, enemy.transform);

            // set up rigidbody
            Rigidbody enemyRigidbody = enemy.GetComponent<Rigidbody>();
            enemyRigidbody.useGravity = newEnemyInfo.hasGravity;


            // just for armored
            Vector3 scaleSize = new Vector3(3, 4, 3);


            if (newEnemyInfo.type == EnemyTypes.EnemyType.Armored)
            {
                BoxCollider enemyBoxCollider = enemy.GetComponent<BoxCollider>();
                enemyBoxCollider.size = Vector3.Scale(scaleSize, enemy.transform.localScale);
            }


            //GameObject enemy = Instantiate(enemyPrefab, EnemyListTransform);
            //EnemyController enemyController = enemy.AddComponent<EnemyController>();
            //enemyController.SetEnemyInfo(enemyInfo);
            //enemyController.SpawnEnemy();





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