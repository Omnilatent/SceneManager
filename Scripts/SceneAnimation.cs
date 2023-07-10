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
        [SerializeField] SimpleAnimObject animObject;
#endif

        //public Controller Controller { get => m_Controller; set => m_Controller = value; }

        public void Show(Action onEndShow)
        {
#if OMNILATENT_SIMPLE_ANIMATION
            animObject.Show(onEndShow);
#else
            onEndShow();
#endif
        }

        public void Hide(Action onEndHide)
        {
#if OMNILATENT_SIMPLE_ANIMATION
            animObject.Hide(onEndHide);
#else
            onEndHide();
#endif
        }
    }
}