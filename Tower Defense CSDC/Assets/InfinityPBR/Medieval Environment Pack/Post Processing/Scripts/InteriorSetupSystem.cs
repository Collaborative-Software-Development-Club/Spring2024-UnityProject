using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfinityPBR.Systems
{
    public class InteriorSetupSystem : MonoBehaviour
    {
        public Transform m_player;
        public GameObject m_interior;
        public float m_interiorActivationDistance = 5f;
        [HideInInspector]
        [SerializeField]
        private bool m_currentStatus = false;

        private void Start()
        {
            Setup();
        }
        private void LateUpdate()
        {
            SetStatus(ValidateStatus());
        }

        private bool ValidateStatus()
        {
            if (m_player == null)
            {
                Debug.LogError("No player transform set");
                return false;
            }

            float value = Vector3.Distance(gameObject.transform.localPosition, m_player.transform.localPosition);
            if (value < m_interiorActivationDistance)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void SetStatus(bool enabled, bool ignoreStatCheck = false)
        {
            if (ignoreStatCheck)
            {
                if (m_interior != null)
                {
                    m_interior.SetActive(enabled);
                    m_currentStatus = enabled;
                }
            }
            else
            {
                if (enabled != m_currentStatus)
                {
                    if (m_interior != null)
                    {
                        m_interior.SetActive(enabled);
                        m_currentStatus = enabled;
                    }
                }
            }
        }
        private void Setup()
        {
            if (m_player == null)
            {
                GameObject playerObject = GameObject.Find("FPSController");
                if (playerObject != null)
                {
                    m_player = playerObject.transform;
                }

                if (Camera.main != null)
                {
                    m_player = Camera.main.transform;
                }
            }

            if (m_interior != null)
            {
                m_currentStatus = m_interior.activeInHierarchy;
            }
        }
    }
}