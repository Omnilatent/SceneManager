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
            // Fade
            Object.FadeInScene();
        }

        public static void Load(string sceneName, object data = null)
        {
            m_MainSceneName = sceneName;
            Object.FadeOutScene();
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
    }
}