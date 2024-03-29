using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SplatMapImporter))]
public class SplatMapImporterEditor : Editor
{
    private static SplatMapImporter m_profile;
    private GUIStyle m_boxStyle;

    public override void OnInspectorGUI()
    {
        //Get Profile
        if (m_profile == null)
        {
            m_profile = (SplatMapImporter)target;
        }

        //Set up the box style
        if (m_boxStyle == null)
        {
            m_boxStyle = new GUIStyle(GUI.skin.box);
            m_boxStyle.normal.textColor = GUI.skin.label.normal.textColor;
            m_boxStyle.fontStyle = FontStyle.Bold;
            m_boxStyle.alignment = TextAnchor.UpperLeft;
        }

        EditorGUI.BeginChangeCheck();

        EditorGUILayout.BeginVertical(m_boxStyle);
        EditorGUILayout.LabelField("Global Settings", EditorStyles.boldLabel);
        m_profile.m_terrain = (Terrain)EditorGUILayout.ObjectField("Terrain", m_profile.m_terrain, typeof(Terrain), true);
        m_profile.m_splatMap = (Texture2D)EditorGUILayout.ObjectField("Splat Map", m_profile.m_splatMap, typeof(Texture2D), true, GUILayout.Height(16f));
        EditorGUILayout.EndVertical();

        if (m_profile.m_terrain != null && m_profile.m_splatMap != null)
        {
            if (GUILayout.Button("Apply Splat Map"))
            {
                m_profile.ApplySplatMap(m_profile.m_splatMap);
            }
        }

        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(m_profile, "Changes Made");
            EditorUtility.SetDirty(m_profile);
        }
    }
}
