using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarController : MonoBehaviour
{
    // adjust canvas
    public Camera playerCamera;
    public Canvas healthDisplayCanvas;

    // health bar set up
    public GameObject healthBar;
    public float verticleOffSet = 1.0f;
    public GameObject enemy;
    public Transform healthBarRed;

    // enemy information
    public float health = 100.0f;
    private Collider enemyCollider;

    private void Start()
    {
        enemyCollider = enemy.GetComponent<BoxCollider>();
        playerCamera = Camera.main;
        UpdateCanvasAngle();
    }

    // Update is called once per frame
    void Update()
    {
        // adjust height of health bar
        if (enemyCollider != null)
        {
            Vector3 topOfEnemy = new Vector3(transform.position.x, enemyCollider.bounds.max.y, transform.position.z);
            healthBar.transform.position = topOfEnemy + Vector3.up * verticleOffSet;
        }

        UpdateCanvasAngle();
    }


    public void UpdateHealth(float health)
    {
        //Debug.Log("UpdateHealth called with health: " + health);
        // enable health bar
        if (!healthBar.activeSelf)
        {
            healthBar.SetActive(true);
            //Debug.Log("Health Bar Set Active");
        }

        // update health bar
        float healthScale = health / 100.0f;
        Vector3 newScale = new Vector3(healthScale, healthBarRed.localScale.y, healthBarRed.localScale.z);
        healthBarRed.localScale = newScale;
    }

    public void UpdateCanvasAngle()
    {
        healthDisplayCanvas.transform.LookAt(
            transform.position + playerCamera.transform.rotation * Vector3.forward,
            playerCamera.transform.rotation * Vector3.up);
    }
}
