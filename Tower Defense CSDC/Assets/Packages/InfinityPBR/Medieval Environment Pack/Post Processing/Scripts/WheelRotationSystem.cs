using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WheelDirection { Forward, Backwards }
[ExecuteAlways]
public class WheelRotationSystem : MonoBehaviour
{
    public Transform m_wheelRoot;
    public WheelDirection m_direction = WheelDirection.Forward;
    public float m_rotationSpeed = 10f;

    // Update is called once per frame
    private void Update()
    {
        if (m_wheelRoot != null)
        {
            switch (m_direction)
            {
                case WheelDirection.Forward:
                    m_wheelRoot.Rotate(Vector3.left * m_rotationSpeed * Time.deltaTime); 
                    break;
                case WheelDirection.Backwards:
                    m_wheelRoot.Rotate(Vector3.right * m_rotationSpeed * Time.deltaTime); 
                    break;
            }
        }
    }
}
