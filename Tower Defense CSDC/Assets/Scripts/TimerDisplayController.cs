using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerDisplayController : MonoBehaviour
{
    public TextMeshProUGUI timer;
    private float startTime;
    WaveManager waveManager;

    float timeToNextWave;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        waveManager = GameObject.Find("WaveManager").GetComponent<WaveManager>();

        
        timeToNextWave = (float)waveManager.waveFrequence;
        Debug.Log(waveManager.waveFrequence);
    }

    // Update is called once per frame
    void Update()
    { 
        if(timeToNextWave > 0)
        {
            timeToNextWave -= Time.deltaTime;
            timer.text = Mathf.Ceil(timeToNextWave).ToString();
            
        }
        else
        {
            ResetTimer();
        }

    }

    private void ResetTimer()
    {
        timeToNextWave = waveManager.waveFrequence;
    }
}
