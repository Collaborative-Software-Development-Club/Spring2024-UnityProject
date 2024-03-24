using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStation
{
    public GameObject storedBuilding {get; set;}
    public void OpenInterface();
    public void OpenGUI();
    public void CloseInterface();
    public void StoreBuilding(GameObject replacement);
    public void Build();
    public GameObject GetStoredBuilding();
}
