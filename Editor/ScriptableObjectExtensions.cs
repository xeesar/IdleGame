using UnityEditor;
using UnityEngine;

namespace Editor
{
    public static class ScriptableObjectExtensions
    {
        public static void SaveData(this ScriptableObject scriptableObject)
        {
            EditorUtility.SetDirty(scriptableObject);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}