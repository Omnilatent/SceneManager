using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Omnilatent.ScenesManager
{
    public abstract class Controller : MonoBehaviour
    {
        [SerializeField] SceneAnimation sceneAnimation;
        [SerializeField] protected Canvas m_Canvas;
        public Canvas Canvas { get => m_Canvas; set => m_Canvas = value; }

        [SerializeField] protected Camera m_Camera;
        public Camera Camera { get => m_Camera; set => m_Camera = value; }

        Manager.SceneData sceneData;
        public Manager.SceneData SceneData { get => sceneData; set => sceneData = value; }
        GameObject m_Shield;

        protected virtual void Awake()
        {
            Manager.OnSceneLoaded(this);
        }

        /// <summary>
        /// Each scene must has an unique scene name.
        /// </summary>
        /// <returns>The name.</returns>
        public abstract string SceneName();

        /// <summary>
        /// This event is raised right after this view becomes active after a call of LoadScene() or ReloadScene() or Popup() of SceneManager.
        /// Same OnEnable but included the data which is transfered from the previous view. (Raised after Awake() and OnEnable())
        /// </summary>
        /// <param name="data">Data.</param>
        public virtual void OnActive(object data)
        {
        }

        public virtual void Show()
        {
            if (sceneAnimation != null)
                sceneAnimation.Show(OnEndShowAnim);
            else OnEndShowAnim();
        }

        private void OnEndShowAnim()
        {
            Manager.OnShown(this);
        }

        public void CreateShield()
        {
            if (m_Shield == null && m_Canvas.sortingOrder > 0)
            {
                m_Shield = new GameObject("Shield");
                m_Shield.layer = LayerMask.NameToLayer("UI");

                Image image = m_Shield.AddComponent<Image>();
                image.color = Manager.ShieldColor;

                Transform t = m_Shield.transform;
                t.SetParent(m_Canvas.transform);
                t.SetSiblingIndex(0);
                t.localScale = Vector3.one;
                t.localPosition = new Vector3(t.localPosition.x, t.localPosition.y, 0);

                RectTransform rt = t.GetComponent<RectTransform>();
                rt.anchorMin = Vector2.zero;
                rt.anchorMax = Vector2.one;
                rt.pivot = new Vector2(0.5f, 0.5f);
                rt.offsetMax = new Vector2(2, 2);
                rt.offsetMin = new Vector2(-2, -2);
            }
        }

        public void SetupCanvas(int sortingOrder)
        {
            if (m_Canvas == null)
            {
                m_Canvas = transform.GetComponentInChildren<Canvas>(true);
            }
            if (m_Canvas.worldCamera == null)
            {
                m_Canvas.sortingOrder = sortingOrder;
                m_Canvas.worldCamera = Manager.Object.UICamera;
            }

#if USING_URP
            if (Camera != null)
            {
                Camera.GetComponent<UnityEngine.Rendering.Universal.UniversalAdditionalCameraData>().cameraStack.Add(Manager.Object.UICamera);
            }
#endif
        }

        /// <summary>
        /// Called after this view finishes its hide-animation and disappears.
        /// </summary>
        public virtual void OnHidden()
        {
        }

        /// <summary>
        /// Called after this view appears and finishes its show-animation.
        /// </summary>
        public virtual void OnShown()
        {
        }

        /// <summary>
        /// This event is raised right after player pushs the ESC button on keyboard or Back button on android devices.
        /// You should assign this method to OnClick event of your Close Buttons.
        /// </summary>
        public virtual void OnKeyBack()
        {
            Manager.Close(this);
        }

        public void Hide()
        {
            if (sceneAnimation != null)
                sceneAnimation.Hide(OnEndHideAnim);
            else OnEndHideAnim();
        }

        private void OnEndHideAnim()
        {
            Manager.OnHidden(this);
        }

        /// <summary>
        /// Called when scene on top is hidden and this scene is in focus
        /// </summary>
        public virtual void OnReFocus()
        {
        }

        protected virtual void OnValidate()
        {
            if (sceneAnimation == null)
            {
                sceneAnimation = GetComponentInChildren<SceneAnimation>();
                if (sceneAnimation != null)
                {
                    Debug.Log($"Auto assign SceneAnimation '{sceneAnimation.gameObject.name}' to '{name}'");
                }
            }
        }
    }
}