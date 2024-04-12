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

    private ResourceDisplay resourceDisplay; // Reference to ResourceDisplay script

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

    private string actionLabel = "";

    // Update is called once per frame
    void Update()
    {
        if (isInRange)
        {
            if (materialType == MaterialType.Wood)
            {
                actionLabel = "Hold E to chop tree";
            }
            else if (materialType == MaterialType.Metal)
            {
                actionLabel = "Hold E to mine";
            }
        }

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

                // Get a reference to the ResourceDisplay script and update the text
                UpdateResourceDisplay();

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

    // Get a reference to the ResourceDisplay script and update the text
    public void UpdateResourceDisplay()
    {
        if (resourceDisplay == null)
        {
            // Find the ResourceDisplay script and get a reference
            resourceDisplay = GameObject.Find("ResourceDisplay").GetComponent<ResourceDisplay>();

            // Check if the reference is null (script not found or not attached)
            if (resourceDisplay == null)
            {
                Debug.LogError("ResourceDisplay script not found!");
                return;
            }
        }

        // Update the ResourceDisplay script
        resourceDisplay.UpdateResourceText();
    }

    void OnGUI()
    {
        if (isInRange)
        {
            // Calculate the center of the screen
            float centerX = Screen.width / 2f;
            float centerY = Screen.height / 2f;

            // Adjust the label position relative to the center
            float labelWidth = 200f;
            float labelHeight = 20f;
            float labelX = centerX - (labelWidth / 2f);
            float labelY = centerY - (labelHeight / 2f);

            // Create a GUIStyle with the desired font size
            GUIStyle guiStyle = new GUIStyle(GUI.skin.label);
            guiStyle.fontSize = 16;

            // Display the action label in the center of the screen with the specified font size
            GUI.Label(new Rect(labelX, labelY+2, labelWidth, labelHeight), actionLabel, guiStyle);
        }
    }
}
