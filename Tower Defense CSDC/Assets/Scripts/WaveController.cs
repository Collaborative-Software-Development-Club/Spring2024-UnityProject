using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WaveController : MonoBehaviour
{
    [SerializeField] private int waveFrequence;
    [SerializeField] private int waveCount;
    public GameObject enemyPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartWave()
    {

    }

    public void SetEnemyList()
    {
        foreach(EnemyTypes.EnemyType type in Enum.GetValues(typeof(EnemyTypes.EnemyType)))
        {

        }
    }
}
