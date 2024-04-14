using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class EnemyManager : MonoBehaviour
{

    public enum propertyType
    {
        Fire,
        Ice,
        Floating,
        Poison,
        Basic
    }

    public static EnemyManager Instance;
    public Dictionary<EnemyTypes.EnemyType, EnemyInfo> enemyDictionary;
    public List<EnemyTypes.EnemyType> sortedEnemyListByCost;
    public EnemyInfo littleGoblinInfo;
    public EnemyInfo sporeGuyInfo;
    public EnemyInfo floatingEyeInfo;
    public EnemyInfo armoredInfo;
    public EnemyInfo iceMageInfo;
    public EnemyInfo lavaHoundInfo;
    
    
    




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
            
        }
    }

    public void SetUpEnemyDictionary()
    {
        enemyDictionary.Add(EnemyTypes.EnemyType.LittleGoblin, littleGoblinInfo);
        enemyDictionary.Add(EnemyTypes.EnemyType.SporeGuy, sporeGuyInfo);
        enemyDictionary.Add(EnemyTypes.EnemyType.FloatingEye, floatingEyeInfo);
        enemyDictionary.Add(EnemyTypes.EnemyType.IceMage, iceMageInfo);
        enemyDictionary.Add(EnemyTypes.EnemyType.LavaHound, lavaHoundInfo);
        enemyDictionary.Add(EnemyTypes.EnemyType.Armored, armoredInfo);
    }

    public List<EnemyTypes.EnemyType> GetEnemyListSortedByCostDecrease() //decrease order
    {
        List<EnemyTypes.EnemyType> enemyListSortedByCostDecrease = new List<EnemyTypes.EnemyType>(this.sortedEnemyListByCost);
        enemyListSortedByCostDecrease.Reverse();
        return enemyListSortedByCostDecrease;

    }

    public void SetUpEnemyListSortedByCostIncrease()
    {
        this.sortedEnemyListByCost = enemyDictionary
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


// todo: scale is not working