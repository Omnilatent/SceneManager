using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Omnilatent.ScenesManager
{
    public class Manager
    {
        public class SceneData
        {
            public object data;
            public Action onShown;
            public Action onHidden;
            public Scene scene;

            public SceneData()
            {
            }

            public SceneData(object data, Action onShown, Action onHidden)
            {
                this.data = data;
                this.onShown = onShown;
                this.onHidden = onHidden;
            }
        }

        public static Color ShieldColor; //shield under the popup scene
        public static float SceneAnimationDuration;
        public static ManagerObject Object;

        static string m_MainSceneName;
        static Controller m_MainController;

        static float sceneFadeDuration;
        public static float SceneFadeDuration { get => sceneFadeDuration; set => sceneFadeDuration = value; }

        static LinkedList<Controller> m_ControllerList = new LinkedList<Controller>();

        //Store temporary data passing between scenes
        static Dictionary<string, SceneData> interSceneDatas = new Dictionary<string, SceneData>();

        static Manager()
        {
            Application.targetFrameRate = 60;

            SceneManager.sceneLoaded += OnUnitySceneLoaded;

            ShieldColor = new Color(0f, 0f, 0f, 0.45f);
            SceneFadeDuration = 0.15f;
            SceneAnimationDuration = 0.283f;

            Object = ((GameObject)GameObject.Instantiate(Resources.Load("ManagerObject"))).GetComponent<ManagerObject>();
        }

        private static void OnUnitySceneLoaded(Scene scene, LoadSceneMode mode)
        {
            var data = GetSceneData(scene.name, true);
            data.scene = scene;
            // Single Mode automatically destroy all scenes, so we have to clear the stack.
            if (mode == LoadSceneMode.Single)
            {
                m_ControllerList.Clear();
            }
        }

        public static void OnSceneLoaded(Controller sender)
        {
            if (string.IsNullOrEmpty(m_MainSceneName) || sender.SceneName() == m_MainSceneName)
            {
                m_MainController = sender;
            }

            m_ControllerList.AddFirst(sender);

            SceneData data = GetSceneData(sender.SceneName(), true);
            sender.SceneData = data;
            sender.OnActive(data.data);
            // Animation
            if (m_ControllerList.Count == 1)
            {
                // Own Camera
                /*if (sender.Camera != null)
                {
                    Object.ActivateBackgroundCamera(false);

                    if (sender.Camera.GetComponent<CameraDestroyer>() == null)
                    {
                        sender.Camera.gameObject.AddComponent<CameraDestroyer>();
                    }
                }*/

                // Main Scene
                m_MainController = sender;

                // Fade
                Object.FadeInScene();
            }
            else
            {
                // Popup Scene
                sender.Show();
            }
        }

        static void AddSceneData(string sceneName, SceneData sceneData)
        {
            interSceneDatas.Add(sceneName, sceneData);
        }

        static SceneData GetSceneData(string sceneName, bool createIfNotExist)
        {
            if (!interSceneDatas.TryGetValue(sceneName, out var sceneData) && createIfNotExist)
            {
                AddSceneData(sceneName, new SceneData());
                sceneData = interSceneDatas[sceneName];
            }
            return sceneData;
        }

        public static void Load(string sceneName, object data = null)
        {
            m_MainSceneName = sceneName;
            Object.FadeOutScene();
            SceneData sceneData = new SceneData(data, null, null);
            interSceneDatas.Add(sceneName, sceneData);
        }

        public static void LoadAsync(string sceneName, Action onSceneLoaded, object data)
        {

        }

        public static void Add(string sceneName, object data = null, Action onShown = null, Action onHidden = null)
        {
            Object.ShieldOn();
            SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
            SceneData sceneData = new SceneData(data, onShown, onHidden);
            interSceneDatas.Add(sceneName, sceneData);
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

        public static void OnShown(Controller controller)
        {
            /*if (controller.FullScreen && m_ControllerStack.Count > 1)
            {
                ActivatePreviousController(controller, false);
            }*/

            controller.OnShown();
            SceneData sceneData = GetSceneData(controller.SceneName(), true);
            sceneData.onShown?.Invoke();

            Object.ShieldOff();
        }

        public static Controller TopController()
        {
            if (m_ControllerList.Count > 0)
            {
                return m_ControllerList.First.Value;
            }

            return null;
        }

        public static void Close(Controller sender)
        {
            m_ControllerList.Remove(sender);
            Object.ShieldOn();
            sender.Hide();
        }

        public static void OnHidden(Controller sender)
        {
            sender.OnHidden();
            sender.SceneData.onHidden?.Invoke();
            Unload(sender);
            TopController().OnReFocus();
            Object.ShieldOff();
        }

        static void Unload(Controller controller)
        {
            SceneManager.UnloadSceneAsync(controller.SceneData.scene);
        }
    }
}