using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class FixedRotation : MonoBehaviour
{
    public Vector3 rotationAmount;
    public float speed = 1.0f;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(rotationAmount);
    }
}
