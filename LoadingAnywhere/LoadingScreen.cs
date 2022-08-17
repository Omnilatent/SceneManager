using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Omnilatent.LoadingUtils
{
    public class LoadingScreen : MonoBehaviour, ILoadingScreen
    {
        [SerializeField] Slider loadingBar;
        float timerLoad;

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
            while (timerLoad < LoadingAnywhere.MinimumLoadTime)
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