using System.Collections.Generic;
using UnityEngine;

namespace InfinityPBR.Systems
{
    public class PrefabRandomizer : MonoBehaviour
    {
        [Header("Setup")] [Tooltip("Add variations that are generated on Awake")]
        public List<GameObject> m_variations = new List<GameObject>();

        [Tooltip("Sets the variation to be randomized")]
        public bool m_randomized = true;

        [Tooltip("Sets the active variation")] public int m_selectedVariation = 0;

        private void Awake()
        {
            SelectVariation(m_selectedVariation);
        }

        private void SelectVariation(int variation)
        {
            if (variation > m_variations.Count - 1 || variation < 0)
            {
                Debug.LogError("Selected index: " + variation +
                               " is out of range of the variations list which has a count of " + m_variations.Count);
                return;
            }

            if (m_randomized)
            {
                variation = Random.Range(0, m_variations.Count - 1);
            }

            for (int i = 0; i < m_variations.Count; i++)
            {
                if (i == variation)
                {
                    m_variations[i].SetActive(true);
                }
                else
                {
                    m_variations[i].SetActive(false);
                }
            }
        }
    }
}