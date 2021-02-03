using UnityEditor;
using UnityEngine;

namespace Editor
{
    public static class UserProfileEditor
    {
    
        [MenuItem("Tools/Reset Game")]
        public static void Reset()
        {
            PlayerPrefs.DeleteAll();
            Debug.Log("Storage Cleared...");
        }

    }

}

