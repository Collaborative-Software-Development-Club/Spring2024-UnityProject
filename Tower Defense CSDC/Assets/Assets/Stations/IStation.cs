using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStation
{
    public GameObject storedBuilding {get; set;}
    public void OpenInterface();
    public void CloseInterface();
    public void StoreBuilding();
    public void Build();
    public GameObject GetStoredBuilding();
}
