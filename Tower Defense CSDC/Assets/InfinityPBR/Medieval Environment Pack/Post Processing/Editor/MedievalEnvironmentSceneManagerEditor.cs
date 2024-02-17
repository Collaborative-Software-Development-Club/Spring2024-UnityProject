using UnityEditor;
using UnityEngine;

namespace InfinityPBR.Systems
{
    [CustomEditor(typeof(MedievalEnvironmentSceneManager))]
    public class MedievalEnvironmentSceneManagerEditor : Editor
    {
        private MedievalEnvironmentSceneManager m_editor;
        private GUIStyle m_boxStyle;

        private void OnEnable()
        {
            if (m_editor == null)
            {
                m_editor = (MedievalEnvironmentSceneManager) target;
            }

            m_editor.UpdateInterior(m_editor.m_interiorStatus);
        }

        public override void OnInspectorGUI()
        {
            if (m_editor == null)
            {
                m_editor = (MedievalEnvironmentSceneManager) target;
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
            EditorGUILayout.LabelField("Interior Global System", EditorStyles.boldLabel);
            m_editor.m_interiorStatus = EditorGUILayout.Toggle("Interior Status", m_editor.m_interiorStatus);
            m_editor.m_player = (Transform)EditorGUILayout.ObjectField("Player", m_editor.m_player, typeof(Transform), true);
            m_editor.m_interiorRange = EditorGUILayout.FloatField("Interior Range", m_editor.m_interiorRange);
            if (m_editor.m_interiorRange < 0.1f)
            {
                m_editor.m_interiorRange = 0.1f;
            }
            EditorGUILayout.EndVertical();

            if (EditorGUI.EndChangeCheck())
            {
                m_editor.UpdateInterior(m_editor.m_interiorStatus);
            }
        }
    }
}