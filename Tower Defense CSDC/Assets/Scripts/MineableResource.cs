using UnityEngine;

public class MineableResource : MonoBehaviour
{
    public static int metalResource = 0;
    public static int woodResource = 0;

    public enum MaterialType
    {
        Wood,
        Metal
    }
    
    // Adjust this value in the Inspector to change the mine time
    public float mineTime = 1.0f;

    // Track if the player is currently in range
    private bool isInRange = false;

    // Track how long the player has been mining this resource
    private float mineTimer = 0.0f;

    // Define the amount of resources to increase when mined
    public int resourceIncreaseAmount = 1;

    // Define the material type of the resource
    public MaterialType materialType;

    // Update is called once per frame
    void Update()
    {
        // Check if the player is mining this resource
        if (isInRange && Input.GetKey(KeyCode.E))
        {
            // Increment the mine timer
            mineTimer += Time.deltaTime;

            // Check if the mine timer has reached the mine time
            if (mineTimer >= mineTime)
            {
                // Check the material type and increase the corresponding resource
                if (materialType == MaterialType.Wood)
                {
                    woodResource += resourceIncreaseAmount;
                    Debug.Log("Wood: " + woodResource);
                }
                else if (materialType == MaterialType.Metal)
                {
                    metalResource += resourceIncreaseAmount;
                    Debug.Log("Metal: " + metalResource);
                }

                // Destroy this resource object
                Destroy(gameObject);
            }
        }
    }

    // OnTriggerEnter is called when the Collider other enters the trigger
    void OnTriggerEnter(Collider other)
    {
        isInRange = true;
    }

    // OnTriggerExit is called when the Collider other exits the trigger
    void OnTriggerExit(Collider other)
    {
        isInRange = false;
    }
}
