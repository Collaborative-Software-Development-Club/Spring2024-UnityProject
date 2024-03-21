using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class StationManager : MonoBehaviour
{
    [SerializeField] private TMP_Text interactPrompt;
    private IStation activeStation;
    void OnEnable() {
        interactPrompt.enabled = false;
        activeStation = null;
        BaseStation.OnPlayerEnter += OpenInterface;
        BaseStation.OnPlayerExit += CloseInterface;
    }

    void OnDisable() {
        BaseStation.OnPlayerEnter -= OpenInterface;
        BaseStation.OnPlayerExit -= CloseInterface;
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
        }
    }
    private void CloseInterface(object o, StationEventArgs sArgs) {
        interactPrompt.enabled = false;
        activeStation = null;
    }

    private void Update() {
        if (activeStation != null && Input.GetKeyDown(KeyCode.E)) {
            activeStation.OpenGUI(); 
            Debug.Log("PROPERKEY");
        }
    }
}
