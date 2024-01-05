using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Omnilatent.Utils
{
    public class LoadingScreen : MonoBehaviour, ILoadingScreen
    {
        [SerializeField] protected Slider loadingBar;
        
        [Tooltip("If less than 0, use default minimum loading time set in LoadingAnywhere")]
        [SerializeField] protected float minimumLoadingTime = -1f;

        public float MinimumLoadingTime { get { return minimumLoadingTime >= 0f ? minimumLoadingTime : LoadingAnywhere.MinimumLoadTime; } }

        protected float timerLoad;

        public float GetProgress()
        {
            return loadingBar.value;
        }

        public void SetProgress(float value)
        {
            loadingBar.value = value;
        }

        public void Show()
        {
            timerLoad = 0f;
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            StartCoroutine(CoHide());
        }

        IEnumerator CoHide()
        {
            while (timerLoad < MinimumLoadingTime)
            {
                yield return 0;
            }
            gameObject.SetActive(false);
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