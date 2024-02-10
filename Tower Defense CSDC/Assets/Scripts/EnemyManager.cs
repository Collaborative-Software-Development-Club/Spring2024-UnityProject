using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;
    public Dictionary<EnemyTypes.EnemyType, EnemyInfo> enemyDictionary;
    public EnemyInfo littleGoblinInfo;
    public List<EnemyTypes.EnemyType> sortedEnemyListByCost;


    private void Awake()
    {
        SetUpInstance();
    }

    public void SetUpInstance()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            enemyDictionary = new Dictionary<EnemyTypes.EnemyType, EnemyInfo>();
            SetUpEnemyDictionary();
            SetUpEnemyListSortedByCost();
            
        }
    }

    public void SetUpEnemyDictionary()
    {
        enemyDictionary.Add(EnemyTypes.EnemyType.LittleGoblin, littleGoblinInfo);
    }

    public void SetUpEnemyListSortedByCost()
    {
        this.sortedEnemyListByCost = enemyDictionary
        .OrderBy(entry => entry.Value.cost) // Order by the cost of the EnemyInfo
        .Select(entry => entry.Key) // Select the keys after sorting
        .ToList();
        this.sortedEnemyListByCost.Reverse();

    }





}
