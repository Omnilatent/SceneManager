using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Omnilatent.ScenesManager
{
    public class SceneTransitionShield : MonoBehaviour
    {
        Image m_Image;
        Color startColor = Color.clear;
        Color endColor = Color.black;
        bool initialized = false;

        private void Awake()
        {
            Init();
        }

        void Init()
        {
            if (!initialized)
            {
                m_Image = GetComponent<Image>();
                initialized = true;
            }
        }

        public void Show()
        {
            Init();
            gameObject.SetActive(true);
            m_Image.color = startColor;
            #if OMNILATENT_SIMPLE_ANIMATION
            m_Image.DOColor(endColor, Manager.SceneFadeDuration).SetUpdate(true);
            #else
            m_Image.color = endColor;
            #endif
        }

        public void Hide()
        {
            Init();
            m_Image.color = endColor;
            #if OMNILATENT_SIMPLE_ANIMATION
            m_Image.DOColor(startColor, Manager.SceneFadeDuration).SetUpdate(true).OnComplete(OnFadeComplete);
            #else
            m_Image.color = startColor;
            OnFadeComplete();
            #endif
        }

        private void OnFadeComplete()
        {
            gameObject.SetActive(false);
        }
    }
}