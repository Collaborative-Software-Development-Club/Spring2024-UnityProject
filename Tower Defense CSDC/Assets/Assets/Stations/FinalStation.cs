using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SoulGames.EasyGridBuilderPro;
using Unity.VisualScripting.Antlr3.Runtime.Collections;

public class FinalStation : MonoBehaviour, IStation
{
    [SerializeField] private GameObject UIPanel;
    [System.Serializable] public struct Storable {
        public string name;
        public BuildableGridObjectTypeSO gridObject;
    }
    [SerializeField] private List<Storable> storables;
    public GameObject storedBuilding {get; set;}
    public delegate void HandlePlayerEnter(object o, StationEventArgs sArgs);
    public static event HandlePlayerEnter OnPlayerEnter;
    public delegate void HandlePlayerExit(object o, StationEventArgs sArgs);
    public static event HandlePlayerExit OnPlayerExit;
    public delegate void HandleBuildable(object o);
    public static event HandleBuildable OnBuiltObject;
    private bool openedInterface = false;
    private List<EasyGridBuilderPro> buildList;

    private void Awake() {
        if (UIPanel is not null) UIPanel.SetActive(false);
        buildList = MultiGridManager.Instance.easyGridBuilderProList;
    }

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

    /// <summary>
    /// Builds a building on the grid
    /// </summary>
    public void Build() {
        if (storedBuilding != null) {
            Time.timeScale = 1.0f;
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;   
            UnityEngine.Cursor.visible = false;
            CloseInterface();
            foreach (EasyGridBuilderPro easyGridBuilderPro in buildList) {
                easyGridBuilderPro.SetGridModeBuilding();
                string typeToUse = "";
                switch(storedBuilding.name) {
                    case "Basic Building":
                    typeToUse = "Basic Tower";
                    break;
                    case "Fire Building":
                    typeToUse = "Fire Tower";
                    break;
                    case "Ice Building":
                    typeToUse = "Ice Tower";
                    break;
                    case "Poison Building":
                    typeToUse = "Poison Tower";
                    break;
                }
                foreach (Storable s in storables) {
                    if (s.name.Equals(typeToUse)) {
                        easyGridBuilderPro.SetSelectedBuildableGridObjectType(s.gridObject);
                        easyGridBuilderPro.TriggerBuildablePlacement();
                        OnBuiltObject?.Invoke(this);
                    }
                }
                if (!typeToUse.Equals("")) Destroy(storedBuilding);
            }
        }
    }
}
