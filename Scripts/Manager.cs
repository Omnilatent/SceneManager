using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Omnilatent.ScenesManager
{
    public class Manager
    {
        public static Color ShieldColor; //shield under the popup scene
        public static float SceneAnimationDuration;
        public static ManagerObject Object;

        static string m_MainSceneName;
        static Controller m_MainController;

        static float sceneFadeDuration;
        public static float SceneFadeDuration { get => sceneFadeDuration; set => sceneFadeDuration = value; }

        //Store temporary data passing between scenes
        static Dictionary<string, object> interSceneDatas = new Dictionary<string, object>();

        static Manager()
        {
            Application.targetFrameRate = 60;

            //SceneManager.sceneLoaded += OnSceneLoaded;

            ShieldColor = new Color(0f, 0f, 0f, 0.45f);
            SceneFadeDuration = 0.15f;
            SceneAnimationDuration = 0.283f;

            Object = ((GameObject)GameObject.Instantiate(Resources.Load("ManagerObject"))).GetComponent<ManagerObject>();
        }

        public static void OnSceneLoaded(Controller sender)
        {
            if (string.IsNullOrEmpty(m_MainSceneName) || sender.SceneName() == m_MainSceneName)
            {
                m_MainController = sender;
            }

            object data;
            if (interSceneDatas.TryGetValue(sender.SceneName(), out data))
            {
                interSceneDatas.Remove(sender.SceneName());
            };
            sender.OnActive(data);

            // Fade
            Object.FadeInScene();
        }

        public static void Load(string sceneName, object data = null)
        {
            m_MainSceneName = sceneName;
            Object.FadeOutScene();
            if (data != null)
            {
                interSceneDatas.Add(sceneName, data);
            }
        }

        public static void LoadAsync(string sceneName, Action onSceneLoaded, object data)
        {

        }

        public static void OnFadedIn()
        {
            m_MainController.OnShown();
        }

        public static void OnFadedOut()
        {
            if (m_MainController != null)
            {
                m_MainController.OnHidden();
            }

            SceneManager.LoadScene(m_MainSceneName, LoadSceneMode.Single);
        }

        public static void Close()
        {
        }
    }
}