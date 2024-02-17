using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceProjectileBehavior : MonoBehaviour
{
    [HideInInspector] public bool markForDestroy = false;
    void OnTriggerEnter(Collider col) {
        markForDestroy = true;
    }
}
