using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InfinityPBR.Systems
{
    public class DoorController : MonoBehaviour 
    {
        public KeyCode m_interactKey = KeyCode.E;
        public Text m_uIText;
        public string m_doorOpenText = "Open";
        public AudioClip m_openDoorAudio;
        public string m_doorCloseText = "Close";
        public AudioClip m_closeDoorAudio;
        public float m_interactRange = 3f;
        public LayerMask m_interactLayers = -1;
        public float openAngle = -100f;
        public float closeAngle = 0f;
        private InteractiveDoor m_currentDoor;
        private GameObject m_currentGameobject;
        private bool m_process = false;

        private void LateUpdate()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, m_interactRange, m_interactLayers))
            {
                m_currentDoor = hit.transform.gameObject.GetComponent<InteractiveDoor>();
                m_currentGameobject = hit.transform.gameObject;
            }

            if (m_currentDoor != null && m_uIText != null)
            {
                m_uIText.enabled = true;
                if (m_currentDoor.m_doorState)
                {
                    m_uIText.text = m_doorCloseText + " (" + m_interactKey + ")";
                }
                else
                {
                    m_uIText.text = m_doorOpenText + " (" + m_interactKey + ")";
                }

                if (Input.GetKeyDown(m_interactKey))
                {
                    if (m_currentGameobject != null)
                    {
                        if (m_currentDoor.m_doorState)
                        {
                            m_process = true;
                        }
                        else
                        {
                            m_process = true;
                        }
                    }
                }

                if (m_process)
                {
                    if (m_currentDoor.m_doorState)
                    {
                        Process(false);
                    }
                    else
                    {
                        Process(true);
                    }
                }
            }
            else
            {
                m_uIText.enabled = false;
            }
        }

        private void Process(bool state)
        {
            Vector3 currentRotation = m_currentGameobject.transform.eulerAngles;
            if (state)
            {
                m_currentDoor.PlaySound(m_openDoorAudio, 0.45f);
                //m_currentDoor.desiredAngle = m_openAngle;
                currentRotation.y = openAngle;
            }
            else
            {
                m_currentDoor.PlaySound(m_closeDoorAudio, 0.45f);
                //m_currentDoor.desiredAngle = m_closeAngle;
                currentRotation.y = closeAngle;
            }

            m_currentGameobject.transform.localEulerAngles = currentRotation;
            m_currentDoor.m_doorState = state;
            m_process = false;
        }
    }
}