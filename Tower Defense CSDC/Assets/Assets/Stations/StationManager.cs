using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class StationManager : MonoBehaviour
{
    [SerializeField] private TMP_Text interactPrompt;
    [SerializeField] private UnityEngine.UI.Button[] buttons;
    private IStation activeStation;

    void OnEnable() {
        interactPrompt.enabled = false;
        activeStation = null;

        BaseStation.OnPlayerEnter += OpenInterface;
        BaseStation.OnPlayerExit += CloseInterface;

        ElementStation.OnPlayerEnter += OpenInterface;
        ElementStation.OnPlayerExit += CloseInterface;

        FinalStation.OnPlayerEnter += OpenInterface;
        FinalStation.OnPlayerExit += CloseInterface;

        foreach (UnityEngine.UI.Button b in buttons) { b.onClick.AddListener(CloseAll); }
    }

    void OnDisable() {
        BaseStation.OnPlayerEnter -= OpenInterface;
        BaseStation.OnPlayerExit -= CloseInterface;

        ElementStation.OnPlayerEnter -= OpenInterface;
        ElementStation.OnPlayerExit -= CloseInterface;

        FinalStation.OnPlayerEnter -= OpenInterface;
        FinalStation.OnPlayerExit -= CloseInterface;
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
    private void CloseAll() {
        Time.timeScale = 1.0f;
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;   
        UnityEngine.Cursor.visible = false;
        if (activeStation != null) activeStation.CloseInterface();
    }

    private void CloseInterface(object o, StationEventArgs sArgs) {
        interactPrompt.enabled = false;
        activeStation = null;
    }

    private void Update() {
        if (activeStation != null && Input.GetKeyDown(KeyCode.E)) {
            interactPrompt.enabled = false;

            Time.timeScale = 0;
            UnityEngine.Cursor.lockState = CursorLockMode.None;
            UnityEngine.Cursor.visible = true;

            activeStation.OpenGUI(); 
            Debug.Log("PROPERKEY");
        }
    }
}
