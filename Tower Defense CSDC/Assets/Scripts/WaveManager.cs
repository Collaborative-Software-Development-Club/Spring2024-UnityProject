using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class WaveManager : MonoBehaviour
{
    public int waveFrequence;
    [SerializeField] private int waveCount;
    public Transform spawningPoint;
    public GameObject enemyPrefab;
    public List<int> wavesCurrency;
    public List<EnemyTypes.EnemyType> spawningEnemyList;
    public NodesManager nodesManager;

    // timer text
    //public TextMeshProUGUI timerTMP;
    public float timeLeft;


    // Start is called before the first frame update
    void Start()
    {
        spawningEnemyList = new List<EnemyTypes.EnemyType>();
        nodesManager = GameObject.Find("NodesManager").GetComponent<NodesManager>();
        StartCoroutine(WaveTimer());
        timeLeft = waveFrequence;


    }

    IEnumerator WaveTimer()
    {
        while (waveCount < wavesCurrency.Count)
        {
            timeLeft = waveFrequence;
            Debug.Log("Time Left: " + timeLeft);
            
            //yield return new WaitForSeconds(waveFrequence);

            while (timeLeft >= 0)
            {
                //timerTMP.text = "Next Wave: " + Mathf.CeilToInt(timeLeft).ToString();
                timeLeft -= Time.deltaTime;
                yield return null;
            }

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


            int pathInt = nodesManager.GetRandomPathInt();
            spawningPoint = nodesManager.GetSpawningPointFromPath(pathInt);

            // Set Enemy Game Object
            GameObject enemy = Instantiate(enemyPrefab, spawningPoint); // spawn enemy object
            enemy.name = enemyType.ToString(); // set enemy object name

            //Set Enemy Script
            EnemyController enemyController = enemy.GetComponent<EnemyController>();
            EnemyInfo newEnemyInfo = EnemyManager.Instance.GetEnemyInfo(enemyType);
            enemyController.SetEnemyInfo(newEnemyInfo);
            enemyController.pathInt = pathInt;
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
                //Debug.Log(enemyNum);
                
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


