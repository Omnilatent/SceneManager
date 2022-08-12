using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
using System.Linq;

namespace Omnilatent.ScenesManager.Editor
{
    [InitializeOnLoad]
    public static class MenuSetup
    {
        [MenuItem("Tools/Omnilatent/Scenes Manager/Import Extra Package", priority = 11)]
        public static void ImportExtraPackage()
        {
            string path = GetPackagePath("Assets/Omnilatent/ScenesManager/ScenesManagerExtra.unitypackage", "ScenesManagerExtra");
            AssetDatabase.ImportPackage(path, true);
        }

        [MenuItem("Tools/Omnilatent/Scenes Manager/Import Extra Package for URP", priority = 12)]
        public static void ImportExtraPackageURP()
        {
            string path = GetPackagePath("Assets/Omnilatent/ScenesManager/ScenesManagerExtraURP.unitypackage", "ScenesManagerExtraURP");
            AssetDatabase.ImportPackage(path, true);
        }

        static string GetPackagePath(string path, string filename)
        {
            if (!File.Exists($"{Application.dataPath}/../{path}"))
            {
                Debug.Log($"{filename} not found at {path}, attempting to search whole project for {filename}");
                string[] guids = AssetDatabase.FindAssets($"{filename} l:package");
                if (guids.Length > 0)
                {
                    path = AssetDatabase.GUIDToAssetPath(guids[0]);
                }
                else
                {
                    Debug.LogError($"{filename} not found at {Application.dataPath}/../{path}");
                    return null;
                }
            }
            return path;
        }
    }
}