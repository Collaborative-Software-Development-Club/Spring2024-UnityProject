using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationManager : MonoBehaviour
{
    void OnEnable() {
        BaseStation.OnPlayerEnter += OpenInterface;
    }

    void OnDisable() {
        BaseStation.OnPlayerEnter -= OpenInterface;
    }

    private void OpenInterface(object o, StationEventArgs sArgs) {
        switch (sArgs.stationType) {
            case StationEventArgs.StationType.Base:
            break;
            case StationEventArgs.StationType.Element:
            break;
            case StationEventArgs.StationType.Final:
            break;
        }
    }
    private void CloseInterface() {

    }
}
