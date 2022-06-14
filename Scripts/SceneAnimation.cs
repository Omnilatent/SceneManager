using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Omnilatent.SimpleAnimation;
using System;

namespace Omnilatent.ScenesManager
{
    public class SceneAnimation : MonoBehaviour
    {
        //[SerializeField] Controller m_Controller;
        [SerializeField] SimpleAnimObject animObject;

        //public Controller Controller { get => m_Controller; set => m_Controller = value; }

        public void Show(Action onEndShow)
        {
            animObject.Show(onEndShow);
        }

        public void Hide(Action onEndHide)
        {
            animObject.Hide(onEndHide);
        }
    }
}