using UnityEngine;
using System.Collections.Generic;

public class CentralPoint : MonoBehaviour
{
    public float damageInterval = 1f; // Time interval between each damage application
    public int maxHealth = 100; // Maximum health of the central point
    private int currentHealth; // Current health of the central point
    private bool isDamaging = false; // Flag to track if the central point is currently taking damage
    private float timer; // Timer to control damage intervals
    private List<EnemyInfo> enemyPool = new List<EnemyInfo>(); // Pool of enemies in the central point

    void Start()
    {
        currentHealth = maxHealth;
        timer = damageInterval;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            // Add the enemy to the pool
            EnemyInfo enemyInfo = other.GetComponent<EnemyInfo>();
            if (enemyInfo != null)
            {
                enemyPool.Add(enemyInfo);
            }

            // Start applying damage to the central point at regular intervals
            isDamaging = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            // Remove the enemy from the pool when it exits the central point
            EnemyInfo enemyInfo = other.GetComponent<EnemyInfo>();
            if (enemyInfo != null)
            {
                enemyPool.Remove(enemyInfo);
            }

            // Stop applying damage when there are no enemies in the central point
            if (enemyPool.Count == 0)
            {
                isDamaging = false;
            }
        }
    }

    void Update()
    {
        // Apply damage at regular intervals while enemies are within the central point
        if (isDamaging)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                //Debug.Log("Update: Dealing Damage...");
                ApplyDamage();
                timer = damageInterval;
            }
        }
    }

    void ApplyDamage()
    {
        // Apply damage to each enemy in the pool
        foreach (EnemyInfo enemyInfo in enemyPool)
        {
            int damageAmount = enemyInfo.damage;
            //Debug.Log("ApplyDamage: Dealing Damage...");
            TakeDamage(damageAmount);
        }
    }

    void TakeDamage(int damage)
    {
        // Reduce the central point's health
        //Debug.Log("TakeDamage: Dealing Damage...");
        currentHealth -= damage;

        // Check if the central point's health has reached zero
        if (currentHealth <= 0)
        {
            // Trigger game over event
            GameManager.Instance.GameOver();
            // Disable further damage application
            isDamaging = false;
        }
    }
}
