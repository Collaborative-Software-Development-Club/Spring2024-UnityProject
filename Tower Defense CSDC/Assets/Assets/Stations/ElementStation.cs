using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementStation : MonoBehaviour, IStation
{
    public GameObject storedBuilding {get; set;}

    /// <summary>
    /// Opens the interface for element station.
    /// </summary>
    public void OpenInterface() {
        Debug.Log("Accessed element station!");
    }

    public void OpenGUI() {
        Debug.Log("Opened GUI");
    }

    /// <summary>
    /// Closes the interface for the element station.
    /// </summary>
    public void CloseInterface() {

    }

    /// <summary>
    /// Stores a building into the station
    /// </summary>
    public void StoreBuilding() {

    }

    /// <summary>
    /// Retrieves the stored building
    /// </summary>
    /// <returns> The stored building </returns>
    public GameObject GetStoredBuilding() {
        return storedBuilding;
    }
    public void Build() {

    }
    private void selectElement() {
        
    }
}
