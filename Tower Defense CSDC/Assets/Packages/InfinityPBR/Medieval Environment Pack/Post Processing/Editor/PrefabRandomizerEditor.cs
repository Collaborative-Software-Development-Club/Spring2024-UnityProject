using UnityEditor;
using UnityEngine;

namespace InfinityPBR.Systems
{
    [CustomEditor(typeof(PrefabRandomizer))]
    public class PrefabRandomizerEditor : Editor
    {
        private static PrefabRandomizer m_profile;
        private GUIStyle m_boxStyle;

        public override void OnInspectorGUI()
        {
            //Get Profile
            if (m_profile == null)
            {
                m_profile = (PrefabRandomizer)target;
            }

            //Set up the box style
            if (m_boxStyle == null)
            {
                m_boxStyle = new GUIStyle(GUI.skin.box)
                {
                    normal = {textColor = GUI.skin.label.normal.textColor},
                    fontStyle = FontStyle.Bold,
                    alignment = TextAnchor.UpperLeft
                };
            }

            EditorGUI.BeginChangeCheck();

            EditorGUILayout.BeginVertical(m_boxStyle);
            EditorGUILayout.LabelField("Global Settings", EditorStyles.boldLabel);
            m_profile.m_randomized = EditorGUILayout.Toggle(new GUIContent("Randomized", "Sets the variation to be randomized"), m_profile.m_randomized);
            if (!m_profile.m_randomized)
            {
                m_profile.m_selectedVariation = EditorGUILayout.IntSlider(new GUIContent("Selected Variation", "Sets the active variation"), m_profile.m_selectedVariation, 0, m_profile.m_variations.Count - 1);
            }

            if (m_profile.m_variations.Count < 1)
            {
                EditorGUILayout.HelpBox("Press 'Add New Variation' to add variation to use on this prefab", MessageType.Info);
            }
            else
            {
                for (int i = 0; i < m_profile.m_variations.Count; i++)
                {
                    EditorGUILayout.BeginHorizontal();
                    m_profile.m_variations[i] = (GameObject)EditorGUILayout.ObjectField(new GUIContent("[" + i + "] Variation", "Variations that are generated on Awake"), m_profile.m_variations[i], typeof(GameObject), true);
                    if (GUILayout.Button("Remove"))
                    {
                        m_profile.m_variations.RemoveAt(i);
                    }
                    EditorGUILayout.EndHorizontal();
                }
            }

            if (GUILayout.Button("Add New Variation"))
            {
                m_profile.m_variations.Add(null);
            }

            EditorGUILayout.EndVertical();

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(m_profile, "Changes Made");
                EditorUtility.SetDirty(m_profile);
            }
        }
    }
}