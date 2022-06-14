using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Omnilatent.ScenesManager.Utils
{
    public class Path
    {
        public static string GetRelativePath(string absolutePath)
        {
            string projectPath = Application.dataPath.Substring(0, Application.dataPath.Length - "Assets".Length);
            return absolutePath.Replace(projectPath, string.Empty);
        }

        public static string GetAbsolutePath(string relativePath)
        {
            return System.IO.Path.Combine(Application.dataPath, relativePath);
        }

        public static string GetRelativePathWithAssets(string relativePath)
        {
            return System.IO.Path.Combine("Assets", relativePath);
        }
    }

    public class File
    {
        public static string Copy(string sourceFileName, string targetRelativePath, bool replaceExistFile = true)
        {
            string targetFullPath = System.IO.Path.Combine(Application.dataPath, targetRelativePath);

            string directoryPath = System.IO.Path.GetDirectoryName(targetFullPath);
            string templatePath = Searcher.SearchFileInProject(sourceFileName);

            if (templatePath == null)
            {
                return null;
            }

            if (!System.IO.Directory.Exists(directoryPath))
            {
                System.IO.Directory.CreateDirectory(directoryPath);
            }

            if (System.IO.File.Exists(targetFullPath))
            {
                if (replaceExistFile)
                {
                    System.IO.File.Delete(targetFullPath);
                }
            }

            if (!System.IO.File.Exists(targetFullPath))
            {
                UnityEditor.FileUtil.CopyFileOrDirectory(templatePath, targetFullPath);
            }

            return targetFullPath;
        }

        public static void ReplaceFileContent(string fullPath, string oldString, string newString)
        {
            string fileContents = System.IO.File.ReadAllText(fullPath);
            fileContents = fileContents.Replace(oldString, newString);
            System.IO.File.WriteAllText(fullPath, fileContents);
        }
    }

    public class Searcher
    {
        private static List<FileInfo> SearchFile(DirectoryInfo dir, string fileName)
        {
            List<FileInfo> foundItems = dir.GetFiles(fileName).ToList();
            DirectoryInfo[] dis = dir.GetDirectories();
            foreach (DirectoryInfo di in dis)
            {
                foundItems.AddRange(SearchFile(di, fileName));
            }

            return foundItems;
        }

        public static string SearchFileInProject(string fileName)
        {
            DirectoryInfo di = new DirectoryInfo(Application.dataPath);
            List<FileInfo> fis = SearchFile(di, fileName);

            if (fis.Count >= 1)
            {
                return fis[0].FullName;
            }

            return null;
        }
    }

    public class Scene
    {
        public static void OpenScene(string fullScenePath)
        {
#if UNITY_EDITOR

#if UNITY_5_0 || UNITY_5_1 || UNITY_5_2
            UnityEditor.EditorApplication.OpenScene(fullScenePath);
#else
            UnityEditor.SceneManagement.EditorSceneManager.OpenScene(fullScenePath);
#endif

#endif
        }

        public static void SaveScene()
        {
#if UNITY_EDITOR

#if UNITY_5_0 || UNITY_5_1 || UNITY_5_2
            UnityEditor.EditorApplication.SaveScene();
#else
            UnityEditor.SceneManagement.EditorSceneManager.SaveOpenScenes();
#endif

#endif
        }

        public static void MarkCurrentSceneDirty()
        {
#if UNITY_EDITOR

#if UNITY_5_0 || UNITY_5_1 || UNITY_5_2
            UnityEditor.EditorApplication.MarkSceneDirty();
#else
            UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(UnityEditor.SceneManagement.EditorSceneManager.GetActiveScene());
#endif

#endif
        }
    }
}