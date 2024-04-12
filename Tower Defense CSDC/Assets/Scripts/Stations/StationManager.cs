using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using SoulGames.EasyGridBuilderPro;

public class StationManager : MonoBehaviour
{
    [SerializeField] private TMP_Text interactPrompt;
    private IStation activeStation;
    private StationEventArgs.StationType sType;
    private GameObject stored;
    private List<EasyGridBuilderPro> easyGridBuilderProList;
    private bool isActivelyBuilding = false;
    

    void OnEnable() {
        interactPrompt.enabled = false;
        activeStation = null;
        stored = null;

        BaseStation.OnPlayerEnter += OpenInterface;
        BaseStation.OnPlayerExit += CloseInterface;

        ElementStation.OnPlayerEnter += OpenInterface;
        ElementStation.OnPlayerExit += CloseInterface;

        FinalStation.OnPlayerEnter += OpenInterface;
        FinalStation.OnPlayerExit += CloseInterface;
        FinalStation.OnBuiltObject += SetToActiveBuild;

        easyGridBuilderProList = MultiGridManager.Instance.easyGridBuilderProList;
    }

    void OnDisable() {
        BaseStation.OnPlayerEnter -= OpenInterface;
        BaseStation.OnPlayerExit -= CloseInterface;

        ElementStation.OnPlayerEnter -= OpenInterface;
        ElementStation.OnPlayerExit -= CloseInterface;

        FinalStation.OnPlayerEnter -= OpenInterface;
        FinalStation.OnPlayerExit -= CloseInterface;
        FinalStation.OnBuiltObject -= SetToActiveBuild;

        easyGridBuilderProList = null;

    }

    private void OpenInterface(object o, StationEventArgs sArgs) {
        interactPrompt.enabled = true;
        activeStation = o as IStation;
        if (activeStation is not null) {
            switch (sArgs.stationType) {
                case StationEventArgs.StationType.Base:
                break;
                case StationEventArgs.StationType.Element:
                break;
                case StationEventArgs.StationType.Final:
                break;
            }
            sType = sArgs.stationType;
        }
    }
    public void CloseAll() {
        Time.timeScale = 1.0f;
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;   
        UnityEngine.Cursor.visible = false;
        if (activeStation != null) activeStation.CloseGUI();
        interactPrompt.enabled = true;
    }

    private void CloseInterface(object o, StationEventArgs sArgs) {
        interactPrompt.enabled = false;
        activeStation = null;
    }
    private void ClosePrompt() {

    }

    private void Update() {
        if (activeStation != null && Input.GetKeyDown(KeyCode.E)) {
            interactPrompt.enabled = false;

            Time.timeScale = 0;
            UnityEngine.Cursor.lockState = CursorLockMode.None;
            UnityEngine.Cursor.visible = true;

            activeStation.OpenGUI(); 
        }
        else if (activeStation != null && sType != StationEventArgs.StationType.Base && Input.GetMouseButtonDown(1) && stored != null) {
            if (activeStation.storedBuilding == null) {
                activeStation.StoreBuilding(stored);
                Destroy(stored);
            }
        }
        else if (activeStation != null && Input.GetMouseButtonDown(1) && stored == null) {
            stored = activeStation.GetStoredBuilding();
        }
        else if (Input.GetMouseButtonDown(0) && isActivelyBuilding) {
            isActivelyBuilding = false;
            foreach (EasyGridBuilderPro egbp in easyGridBuilderProList) {
                egbp.TriggerBuildablePlacementCancelled();
                egbp.SetGridMode(GridMode.None);
            }
        }
    }
    public void SetToActiveBuild(object o) {
        isActivelyBuilding = true;
    }
}
