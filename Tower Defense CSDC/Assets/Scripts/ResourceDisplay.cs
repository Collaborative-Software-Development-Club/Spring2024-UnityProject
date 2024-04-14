using TMPro;
using UnityEngine;

public class ResourceDisplay : MonoBehaviour
{
    public TextMeshProUGUI metalText;
    public TextMeshProUGUI woodText;

    private void Start()
    {
        // Assuming your script is attached to the same GameObject as the TextMeshPro components
        metalText = GameObject.Find("MetalText").GetComponent<TextMeshProUGUI>();
        woodText = GameObject.Find("WoodText").GetComponent<TextMeshProUGUI>();

        if (metalText == null || woodText == null)
        {
            //Debug.LogError("TextMeshPro components not found!");
        }
        else
        {
            //Debug.Log("TextMeshPro components found.");
        }

        // Example of updating the TextMeshPro components with integer values
        UpdateResourceText();
    }

    public void UpdateResourceText()
    {
        // Accessing the static int variables from the MineableResource script
        int metalResource = MineableResource.metalResource;
        int woodResource = MineableResource.woodResource;

        // Update the TextMeshPro components
        metalText.text = metalResource.ToString();
        woodText.text = woodResource.ToString();
        Debug.Log("UpdateResourceText executed.");
    }
}
