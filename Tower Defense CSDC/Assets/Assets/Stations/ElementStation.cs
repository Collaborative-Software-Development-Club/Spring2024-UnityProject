using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElementStation : MonoBehaviour, IStation
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
    /// Opens the interface for element station.
    /// </summary>
    public void OpenInterface() {
        Debug.Log("Accessed element station!");
        openedInterface = true;
        OnPlayerEnter?.Invoke(this, new StationEventArgs(StationEventArgs.StationType.Element));
    }

    public void OpenGUI() {
        Debug.Log("Opened GUI");
        UIPanel.SetActive(true);
    }

    /// <summary>
    /// Closes the interface for the element station.
    /// </summary>
    public void CloseInterface() {
        Debug.Log("Left base station!");
        openedInterface = false;
        OnPlayerExit?.Invoke(this, new StationEventArgs(StationEventArgs.StationType.Element));
        UIPanel.SetActive(false);
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
