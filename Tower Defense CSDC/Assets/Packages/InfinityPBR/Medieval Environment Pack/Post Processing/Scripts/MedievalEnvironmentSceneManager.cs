using UnityEngine;

namespace InfinityPBR.Systems
{
    public class MedievalEnvironmentSceneManager : MonoBehaviour
    {
        public Transform m_player;
        public bool m_interiorStatus = false;
        public float m_interiorRange = 5f;

        private void Start()
        {
            UpdateInterior(false);
        }
        public void UpdateInterior(bool state)
        {
            InteriorSetupSystem[] systems = FindObjectsOfType<InteriorSetupSystem>();
            if (systems.Length < 1)
            {
                return;
            }

            for (int i = 0; i < systems.Length; i++)
            {
                systems[i].SetStatus(state, true);
                systems[i].m_interiorActivationDistance = m_interiorRange;
                if (m_player != null)
                {
                    systems[i].m_player = m_player;
                }
            }
        }
    }
}