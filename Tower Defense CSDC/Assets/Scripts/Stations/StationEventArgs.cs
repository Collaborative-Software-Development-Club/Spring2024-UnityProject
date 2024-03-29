using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationEventArgs : EventArgs
{
    public enum StationType {
        Base, Element, Final
    }
    public StationType stationType {get; set;}

    public StationEventArgs(StationType type) {
        this.stationType = type;
    }
}
