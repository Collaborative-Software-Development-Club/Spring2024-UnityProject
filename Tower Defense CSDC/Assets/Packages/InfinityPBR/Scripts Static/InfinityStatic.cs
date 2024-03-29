using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

/*
 * These are static methods used in the editor scripts from Infinity PBR
 */

namespace InfinityPBR
{
    [System.Serializable]
    public static class InfinityStatic
    {
#if UNITY_EDITOR
        public static string[] AllPrefabGuids => AssetDatabase.FindAssets("t:Prefab");
        public static string[] AllPrefabPaths => AllPrefabGuids.Select(AssetDatabase.GUIDToAssetPath).ToArray();
#endif
        
        public static Vector3 WorldPositionOf(Transform transform, Vector3 positionOffset) => transform.TransformPoint(positionOffset);
        
        public static Object[] FindAssetsByLabel(string label, bool sortAlpha = true)
        {
            var guids = AssetDatabase.FindAssets($"l:{label}");
            var objects = new Object[guids.Length];
            for (var i = 0; i < guids.Length; i++)
            {
                var assetPath = AssetDatabase.GUIDToAssetPath(guids[i]);
                objects[i] = AssetDatabase.LoadAssetAtPath<Object>(assetPath);
            }
            
            return sortAlpha ? objects.OrderBy(o => o.name).ToArray() : objects;
        }
        
        public static List<string> GetAllLabels()
        {
            var guids = AssetDatabase.FindAssets("l:Infinity");

            // Extract labels from guids
            var allLabels = new List<string>();
            foreach (var guid in guids)
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                var labels = AssetDatabase.GetLabels(AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(path));
                allLabels.AddRange(labels);
            }

            // Remove duplicate labels
            allLabels = allLabels.Distinct().ToList();

            // Ensure "Infinity" is in the label list
            if (!allLabels.Contains("Infinity"))
                allLabels.Insert(0, "Infinity");

            return allLabels;
        }
        
        public static Object[] FindAssetsByLabel(int labelMask, string searchString = "", bool requireAll = true, bool sortAlpha = true)
        {
            var allLabels = GetAllLabels(); // Implement this function as per your needs

            
            // Convert the mask to a list of selected labels
            /*
            var labels = new List<string>();
            for (var i = 0; i < allLabels.Count; i++)
            {
                if ((labelMask & (1 << i)) != 0)
                {
                    labels.Add(allLabels[i]);
                }
            }
            */
            // Convert the mask to a list of selected labels
            var selectedLabels = new List<string>();
            for (int i = 0; i < allLabels.Count; i++)
            {
                if ((labelMask & (1 << i)) != 0)
                {
                    selectedLabels.Add(allLabels[i]);
                }
            }

            var objects = new List<Object>();
            
            foreach (var label in selectedLabels)
            {
                var searchFilter = "l:" + label;

                var guids = AssetDatabase.FindAssets(searchFilter);
                foreach (var guid in guids)
                {
                    var assetPath = AssetDatabase.GUIDToAssetPath(guid);
                    var asset = AssetDatabase.LoadAssetAtPath<Object>(assetPath);
                    if (asset == null || (string.IsNullOrWhiteSpace(searchString) == false && asset.name.Contains(searchString) == false))
                        continue;

                    // Check if the asset has all the selected labels
                    var assetLabels = AssetDatabase.GetLabels(asset);
                    if (selectedLabels.All(label => assetLabels.Contains(label)))
                    {
                        objects.Add(asset);
                    }
                }
            }

            // Remove duplicates
            objects = objects.Distinct().ToList();
            
            /*
            var searchFilter = requireAll ? "l:" + string.Join(" l:", labels) : "l:" + string.Join(" ", labels);

            var guids = AssetDatabase.FindAssets(searchFilter);
            foreach (var guid in guids)
            {
                var assetPath = AssetDatabase.GUIDToAssetPath(guid);
                var asset = AssetDatabase.LoadAssetAtPath<Object>(assetPath);
                if (asset == null || (string.IsNullOrWhiteSpace(searchString) == false && asset.name.Contains(searchString) == false))
                    continue;
        
                objects.Add(asset);
            }
            */

            return sortAlpha ? objects.OrderBy(o => o.name).ToArray() : objects.ToArray();
        }
        
        public static Object[] FindAssetsByLabel(string[] labels, string searchString = "", bool requireAll = true, bool sortAlpha = true)
        {
            var objects = new List<Object>();
            var searchFilter = requireAll ? "l:" + string.Join(" l:", labels) : "l:" + string.Join(" ", labels);
        
            var guids = AssetDatabase.FindAssets(searchFilter);
            foreach (var guid in guids)
            {
                var assetPath = AssetDatabase.GUIDToAssetPath(guid);
                var asset = AssetDatabase.LoadAssetAtPath<Object>(assetPath);
                if (asset == null || (string.IsNullOrWhiteSpace(searchString) == false && asset.name.Contains(searchString) == false))
                    continue;
                
                objects.Add(asset);
            }

            return sortAlpha ? objects.OrderBy(o => o.name).ToArray() : objects.ToArray();
        }

        public static void AddLabel(this UnityEngine.Object obj, string label)
        {
            var currentLabels = AssetDatabase.GetLabels(obj);
        
            if (!currentLabels.Contains(label))
            {
                List<string> labelList = new List<string>(currentLabels) { label };
                AssetDatabase.SetLabels(obj, labelList.ToArray());
            }
        }
    }
}
