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
    }
}