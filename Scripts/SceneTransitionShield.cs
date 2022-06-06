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
            m_Image.DOColor(endColor, Manager.SceneFadeDuration);
        }

        public void Hide()
        {
            Init();
            m_Image.color = endColor;
            m_Image.DOColor(startColor, Manager.SceneFadeDuration).OnComplete(OnFadeComplete);
        }

        private void OnFadeComplete()
        {
            gameObject.SetActive(false);
        }
    }
}