using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Omnilatent.Utils
{
    public class LoadingAnywhere
    {
        //static ILoadingScreen loadingScreenCached;

        public const string defaultPrefabPath = "LoadingScreen";

        static float minimumLoadTime = 0f;
        public static float MinimumLoadTime { get => minimumLoadTime; set => minimumLoadTime = value; }

        static bool loading = false;
        public static bool Loading { get => loading; }

        static Dictionary<string, ILoadingScreen> cachedLoadingScreens = new Dictionary<string, ILoadingScreen>();
        static ILoadingScreen currentLoadingScreen;

        public static void Init(string _prefabPath = defaultPrefabPath)
        {
            if (!cachedLoadingScreens.ContainsKey(_prefabPath))
            {
                GameObject prefab = Resources.Load<GameObject>(_prefabPath);
                var loadingScreenObject = MonoBehaviour.Instantiate(prefab);
                var loadingScreen = loadingScreenObject.GetComponent<ILoadingScreen>();
                cachedLoadingScreens.Add(_prefabPath, loadingScreen);
                loadingScreenObject.SetActive(false);
                MonoBehaviour.DontDestroyOnLoad(loadingScreenObject);
            }
        }

        /// <summary>
        /// Change path to loading screen in Resources. Must be called before initialization
        /// </summary>
        /// <param name="path"></param>
        [System.Obsolete("Use Init(string _prefabPath) instead.", true)]
        public static void SetPrefabPath(string path)
        {
            /*prefabPath = path;
            if (isInitialized)
            {
                Debug.LogError("Loading Screen object has already been initialized.");
            }*/
        }

        public static void Show(string _prefabPath = defaultPrefabPath, Action onShown = null)
        {
            Init(_prefabPath);
            currentLoadingScreen = cachedLoadingScreens[_prefabPath];
            currentLoadingScreen.Show(onShown);
            loading = true;
        }

        public static void Hide(string _prefabPath = null, Action onHide = null)
        {
            /*if (!isInitialized)
            {
                Debug.LogError("Loading Screen hasn't been initialized");
                return;
            }*/
            ILoadingScreen loadingScreenToHide;
            if (_prefabPath == null)
            {
                if (currentLoadingScreen == null)
                {
                    Debug.LogWarning("Hide failed. No loading screen is showing.");
                    onHide?.Invoke();
                    return;
                }
                loadingScreenToHide = currentLoadingScreen;
            }
            else if (!cachedLoadingScreens.TryGetValue(_prefabPath, out loadingScreenToHide))
            {
                Debug.LogWarning($"cachedLoadingScreens does not contain key '{_prefabPath}'. Check if the loading screen has been initialized.");
            }

            if (loadingScreenToHide != null)
            {
                loadingScreenToHide.Hide(onHide);
            }
            else
            {
                onHide?.Invoke();
            }

            loading = false;
            currentLoadingScreen = null;
        }
    }
}