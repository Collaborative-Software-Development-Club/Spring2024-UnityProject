using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStation : MonoBehaviour, IStation
{
    public GameObject storedBuilding {get; set;}
    public delegate void HandlePlayerEnter(object o, StationEventArgs sArgs);
    public static event HandlePlayerEnter OnPlayerEnter;
    private bool openedInterface = false;

    /// <summary>
    /// Opens the interface for base station.
    /// </summary>
    public void OpenInterface() {
        Debug.Log("Accessed base station!");
        openedInterface = true;
        OnPlayerEnter?.Invoke(this, new StationEventArgs(StationEventArgs.StationType.Base));
    }

    /// <summary>
    /// Closes the interface for the base station.
    /// </summary>
    public void CloseInterface() {
        openedInterface = false;
        Debug.Log("Left base station!");
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
}
