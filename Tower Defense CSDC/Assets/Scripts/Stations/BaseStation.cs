using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseStation : MonoBehaviour, IStation
{
    [SerializeField] private GameObject UIPanel;
    [SerializeField] private GameObject buildingPrefab;
    public GameObject storedBuilding {get; set;}
    public delegate void HandlePlayerEnter(object o, StationEventArgs sArgs);
    public static event HandlePlayerEnter OnPlayerEnter;
    public delegate void HandlePlayerExit(object o, StationEventArgs sArgs);
    public static event HandlePlayerExit OnPlayerExit;

    private void Awake() {if (UIPanel is not null) UIPanel.SetActive(false);}

    /// <summary>
    /// Opens the interface for base station.
    /// </summary>
    public void OpenInterface() {
        Debug.Log("Accessed base station!");
        OnPlayerEnter?.Invoke(this, new StationEventArgs(StationEventArgs.StationType.Base));
    }

    public void OpenGUI() {
        Debug.Log("Opened GUI");
        UIPanel.SetActive(true);
    }
    public void CloseGUI() {
        Debug.Log("Closed GUI");
        UIPanel.SetActive(false);
    }

    /// <summary>
    /// Closes the interface for the base station.
    /// </summary>
    public void CloseInterface() {
        Debug.Log("Left base station!");
        OnPlayerExit?.Invoke(this, new StationEventArgs(StationEventArgs.StationType.Base));
        UIPanel.SetActive(false);
    }

    /// <summary>
    /// Stores a building into the station
    /// </summary>
    public void StoreBuilding(GameObject replacement) {
        storedBuilding = Instantiate(replacement, new Vector3(this.transform.position.x, this.transform.position.y + 2, this.transform.position.z), Quaternion.identity, this.transform);
        storedBuilding.name = replacement.name;
    }

    /// <summary>
    /// Retrieves the stored building
    /// </summary>
    /// <returns> The stored building </returns>
    public GameObject GetStoredBuilding() {
        GameObject copy = Instantiate(storedBuilding);
        copy.name = storedBuilding.name;
        Destroy(storedBuilding);
        return copy;
    }

    public void Build() {
        storedBuilding = Instantiate(buildingPrefab, new Vector3(this.transform.position.x, this.transform.position.y + 2, this.transform.position.z), Quaternion.identity, this.transform);
        storedBuilding.name = buildingPrefab.name;
    }
}
