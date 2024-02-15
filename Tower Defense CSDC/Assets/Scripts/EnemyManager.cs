using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;
    public Dictionary<EnemyTypes.EnemyType, EnemyInfo> enemyDictionary;
    public EnemyInfo littleGoblinInfo;
    public List<EnemyTypes.EnemyType> sortedEnemyListByCostDecrease;
    public List<EnemyTypes.EnemyType> sortedEnemyListByCostIncrease;





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
            SetUpEnemyListSortedByCostIncrease();
            SetUpEnemyListSortedByCostDecrease();
            
        }
    }

    public void SetUpEnemyDictionary()
    {
        enemyDictionary.Add(EnemyTypes.EnemyType.LittleGoblin, littleGoblinInfo);
    }

    public void SetUpEnemyListSortedByCostDecrease() //decrease order
    {
        this.sortedEnemyListByCostIncrease.Reverse();

    }

    public void SetUpEnemyListSortedByCostIncrease()
    {
        this.sortedEnemyListByCostIncrease = enemyDictionary
        .OrderBy(entry => entry.Value.cost) // Order by the cost of the EnemyInfo
        .Select(entry => entry.Key) // Select the keys after sorting
        .ToList();
    }

    public EnemyInfo GetEnemyInfo(EnemyTypes.EnemyType enemyType)
    {
        EnemyInfo enemyInfo;
        bool found = enemyDictionary.TryGetValue(enemyType, out enemyInfo);
        if (!found)
        {
            Debug.LogError("Enemy type of {enemyType} haven't set up");
        }
        return enemyInfo;
    }






}
