using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Omnilatent.SceneManager
{
    public class Manager : MonoBehaviour
    {

        static string mainSceneName;

        static float sceneFadeDuration;
        public static float SceneFadeDuration { get => sceneFadeDuration; set => sceneFadeDuration = value; }

        public static void Load(string sceneName, object data = null)
        {
            mainSceneName = sceneName;
        }

        public static void LoadAsync(string sceneName, Action onSceneLoaded, object data)
        {

        }
    }
}