using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ElementStation : MonoBehaviour, IStation
{
    [SerializeField] private GameObject UIPanel;
    [SerializeField] private TMP_Text elementText;
    [SerializeField] private GameObject[] towerPrefabs;
    public GameObject storedBuilding {get; set;}
    public delegate void HandlePlayerEnter(object o, StationEventArgs sArgs);
    public static event HandlePlayerEnter OnPlayerEnter;
    public delegate void HandlePlayerExit(object o, StationEventArgs sArgs);
    public static event HandlePlayerExit OnPlayerExit;
    private bool openedInterface = false;
    private string element;

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
    public void StoreBuilding(GameObject replacement) {
        Debug.Log("Storing...");
        if (storedBuilding is not null) Destroy(storedBuilding);
        storedBuilding = Instantiate(replacement, new Vector3(this.transform.position.x, this.transform.position.y + 2, this.transform.position.z), Quaternion.identity, this.transform);
    }

    /// <summary>
    /// Switches element name
    /// </summary>
    /// <param name="elementType"> The name of the element to switch to </param>
    public void SwitchElement(string elementType) {
        elementText.text = "Element type: " + elementType;
        element = elementType;
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
        if (storedBuilding is not null) {
            Destroy(storedBuilding);
            int i = -1;
            switch (element) {
                case "Basic":
                i = 3;
                break;
                case "Ice":
                i = 1;
                break;
                case "Fire":
                i = 0;
                break;
                case "Poison":
                i = 2;
                break;
            }
            storedBuilding = Instantiate(towerPrefabs[i], new Vector3(this.transform.position.x, this.transform.position.y + 2, this.transform.position.z), Quaternion.identity, this.transform);
        }
    }
}
