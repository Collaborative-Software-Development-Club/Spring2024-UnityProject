using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerDetectStation : MonoBehaviour
{
    void OnTriggerEnter(Collider col) {
        if (col.gameObject.tag.Equals("Station")) {
            col.GetComponent<IStation>().OpenInterface();
        }
    }
    void OnTriggerExit(Collider col) {
        if (col.gameObject.tag.Equals("Station")) {
            col.GetComponent<IStation>().CloseInterface();
        }
    }
}
