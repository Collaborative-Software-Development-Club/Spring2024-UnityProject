using UnityEditor;
using UnityEngine;

namespace InfinityPBR
{
    
    public abstract class InfinityEditorScriptableObject<T> : InfinityEditor where T : ScriptableObject
    {
        protected T Script;

        protected virtual void OnEnable()
        {
            Script = (T) target;
            Setup();
        }
        
        protected abstract void Setup();
        protected abstract void Cache();

        protected abstract void Draw();
        protected abstract void Header();

        public override void OnInspectorGUI()
        {
            if (!Script) return;
            // April 23, 2021 -- I'm pretty sure the serializedObject bits here do nothing at all.
            serializedObject.Update();
            Header();
            Draw();
            serializedObject.ApplyModifiedProperties();
        }
        
        protected virtual void SetDirty(Object obj = null)
        {
            if (obj == null)
                obj = Script;
            EditorUtility.SetDirty(obj);
        }

        protected virtual void BeginChangeCheck() => EditorGUI.BeginChangeCheck();

        protected virtual void EndChangeCheck(bool setDirty = true)
        {
            if (!EditorGUI.EndChangeCheck()) return;
            if (!setDirty) return;
            SetDirty();
        }

    }

}