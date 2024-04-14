using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedController : MonoBehaviour
{
    public EnemyController enemyController;

    private void Start()
    {
        enemyController = GetComponent<EnemyController>();
    }

    void OnTriggerEnter(Collider col)
    {
        
        if (col.CompareTag("Terrain"))
        {
            Debug.Log("Collided with the terrian");
            enemyController.SetGrounded();
            this.enabled = false;
        }
    }
}
