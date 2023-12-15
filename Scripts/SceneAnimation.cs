using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if OMNILATENT_SIMPLE_ANIMATION
using Omnilatent.SimpleAnimation;
#endif
using System;

namespace Omnilatent.ScenesManager
{
    public class SceneAnimation : MonoBehaviour
    {
        //[SerializeField] Controller m_Controller;
        #if OMNILATENT_SIMPLE_ANIMATION
        [SerializeField] protected SimpleAnimObject animObject;
        #endif
        [Tooltip("If true, show animation will be skipped")] [SerializeField]
        protected bool showInstantly = false;

        [Tooltip("If true, hide animation will be skipped")] [SerializeField]
        protected bool hideInstantly = false;

        //public Controller Controller { get => m_Controller; set => m_Controller = value; }

        public virtual void Show(Action onEndShow)
        {
            #if OMNILATENT_SIMPLE_ANIMATION
            animObject.Show(onEndShow, showInstantly);
            #else
            onEndShow();
            #endif
        }

        public virtual void Hide(Action onEndHide)
        {
            #if OMNILATENT_SIMPLE_ANIMATION
            animObject.Hide(onEndHide, hideInstantly);
            #else
            onEndHide();
            #endif
        }
    }
}