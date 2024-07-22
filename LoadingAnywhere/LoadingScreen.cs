using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Omnilatent.Utils
{
    public class LoadingScreen : MonoBehaviour, ILoadingScreen
    {
        [SerializeField] protected Slider loadingBar;
        float _progress;

        [Tooltip("If less than 0, use default minimum loading time set in LoadingAnywhere")] [SerializeField]
        protected float minimumLoadingTime = -1f;

        public float MinimumLoadingTime { get { return minimumLoadingTime >= 0f ? minimumLoadingTime : LoadingAnywhere.MinimumLoadTime; } }

        protected float timerLoad;

        public float GetProgress()
        {
            return _progress;
        }

        public void SetProgress(float value)
        {
            _progress = value;
            if (loadingBar != null)
                loadingBar.value = _progress;
        }

        public void Show(Action onShown = null)
        {
            timerLoad = 0f;
            gameObject.SetActive(true);
            onShown?.Invoke();
        }

        public void Hide(Action onHide = null)
        {
            if (!gameObject.activeInHierarchy) //loading screen is already hidden
            {
                onHide?.Invoke();
                return;
            }

            StartCoroutine(CoHide(onHide));
        }

        IEnumerator CoHide(Action onHide)
        {
            while (timerLoad < MinimumLoadingTime)
            {
                yield return 0;
            }

            gameObject.SetActive(false);
            onHide?.Invoke();
        }

        public GameObject GetGameObject()
        {
            return gameObject;
        }

        private void Update()
        {
            timerLoad += Time.deltaTime;
        }
    }
}