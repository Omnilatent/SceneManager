using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Omnilatent.LoadingUtils
{
    public class LoadingAnywhere
    {
        static ILoadingScreen loadingScreenCached;

        static string prefabPath = "LoadingScreen";

        static bool isInitialized;

        static float minimumLoadTime = .6f;
        public static float MinimumLoadTime { get => minimumLoadTime; set => minimumLoadTime = value; }

        static bool loading = false;
        public static bool Loading { get => loading; }

        public static void Init()
        {
            if (loadingScreenCached == null)
            {
                GameObject prefab = Resources.Load<GameObject>(prefabPath);
                var loadingScreenObject = MonoBehaviour.Instantiate(prefab);
                loadingScreenCached = loadingScreenObject.GetComponent<ILoadingScreen>();
                loadingScreenObject.SetActive(false);
                isInitialized = true;
            }
        }

        /// <summary>
        /// Change path to loading screen in Resources. Must be called before initialization
        /// </summary>
        /// <param name="path"></param>
        public static void SetPrefabPath(string path)
        {
            prefabPath = path;
            if (isInitialized)
            {
                Debug.LogError("Loading Screen object has already been initialized.");
            }
        }

        public static void Show()
        {
            Init();
            loadingScreenCached.Show();
            loading = true;
        }

        public static void Hide()
        {
            if (!isInitialized)
            {
                Debug.LogError("Loading Screen hasn't been initialized");
                return;
            }
            loadingScreenCached.Hide();
            loading = false;
        }
    }
}