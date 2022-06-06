using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Omnilatent.ScenesManager
{
    public abstract class Controller : MonoBehaviour
    {
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
            Manager.Close();
        }
    }
}