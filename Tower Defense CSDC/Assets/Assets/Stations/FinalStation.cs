using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalStation : MonoBehaviour, IStation
{
    [SerializeField] private GameObject UIPanel;
    public GameObject storedBuilding {get; set;}
    public delegate void HandlePlayerEnter(object o, StationEventArgs sArgs);
    public static event HandlePlayerEnter OnPlayerEnter;
    public delegate void HandlePlayerExit(object o, StationEventArgs sArgs);
    public static event HandlePlayerExit OnPlayerExit;
    private bool openedInterface = false;

    private void Awake() {if (UIPanel is not null) UIPanel.SetActive(false);}

    /// <summary>
    /// Opens the interface for final station.
    /// </summary>
    public void OpenInterface() {
        Debug.Log("Accessed final station!");
        openedInterface = true;
        OnPlayerEnter?.Invoke(this, new StationEventArgs(StationEventArgs.StationType.Final));
    }

    public void OpenGUI() {
        Debug.Log("Opened GUI");
        UIPanel.SetActive(true);
    }

    /// <summary>
    /// Closes the interface for the final station.
    /// </summary>
    public void CloseInterface() {
        Debug.Log("Left final station!");
        openedInterface = false;
        OnPlayerExit?.Invoke(this, new StationEventArgs(StationEventArgs.StationType.Final));
        UIPanel.SetActive(false);
    }

    /// <summary>
    /// Stores a building into the station
    /// </summary>
    public void StoreBuilding(GameObject replacement) {
        storedBuilding = Instantiate(replacement, new Vector3(this.transform.position.x, this.transform.position.y + 2, this.transform.position.z), Quaternion.identity, this.transform);
    }

    /// <summary>
    /// Retrieves the stored building
    /// </summary>
    /// <returns> The stored building </returns>
    public GameObject GetStoredBuilding() {
        GameObject copy = Instantiate(storedBuilding);
        Destroy(storedBuilding);
        return copy;
    }
    public void Build() {
        
    }
}
