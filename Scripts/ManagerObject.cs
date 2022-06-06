using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Omnilatent.ScenesManager
{
    public class ManagerObject : MonoBehaviour
    {
        public enum State
        {
            SHIELD_OFF,
            SHIELD_ON,
            SHIELD_FADE_IN,
            ShieldFadeOut,
            SCENE_LOADING
        }

        // Shield & Transition Vars
        bool m_ShieldActive;
        State m_State;
        public State GetState() { return m_State; }
        public bool ShieldActive
        {
            get
            {
                return m_ShieldActive;
            }
            protected set
            {
                m_ShieldActive = value;
                m_Shield.gameObject.SetActive(m_ShieldActive);
            }
        }

        // Shield & Transition
        [SerializeField] Image m_Shield; //block interaction
        [SerializeField] SceneTransitionShield sceneTransitionShield;
        [SerializeField] Color m_ShieldColor = Color.black;
        bool createPersistentEventSystem = true;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            if (createPersistentEventSystem)
            {
                EventSystem prefab = Resources.Load<EventSystem>("PersistentEventSystem");
                var eventSystem = Instantiate(prefab);
                DontDestroyOnLoad(eventSystem.gameObject);
            }
        }

        // Scene gradually appear
        public void FadeInScene()
        {
            if (this != null)
            {
                if (Manager.SceneFadeDuration == 0)
                {
                    ShieldOff();
                }
                else
                {
                    ShieldActive = true;
                    sceneTransitionShield.Hide();
                    m_State = State.SHIELD_FADE_IN;
                    StartCoroutine(CoFadeInScene());
                }
            }
        }

        IEnumerator CoFadeInScene()
        {
            yield return new WaitForSecondsRealtime(Manager.SceneFadeDuration);
            OnFadedIn();
        }

        // Scene gradually disappear
        public void FadeOutScene()
        {
            if (this != null)
            {
                if (Manager.SceneFadeDuration == 0)
                {
                    OnFadedOut();
                    ShieldOn();
                }
                else
                {
                    ShieldActive = true;
                    sceneTransitionShield.Show();
                    m_State = State.ShieldFadeOut;
                    StartCoroutine(CoFadeOutScene());
                }
            }
        }

        IEnumerator CoFadeOutScene()
        {
            yield return new WaitForSecondsRealtime(Manager.SceneFadeDuration);
            OnFadedOut();
        }

        public void OnFadedIn()
        {
            if (this != null)
            {
                m_State = State.SHIELD_OFF;
                ShieldActive = false;
                Manager.OnFadedIn();
            }
        }

        public void OnFadedOut()
        {
            m_State = State.SCENE_LOADING;
            Manager.OnFadedOut();
        }

        public void ShieldOn()
        {
            if (m_State == State.SHIELD_OFF)
            {
                m_State = State.SHIELD_ON;
                ShieldActive = true;
            }
        }

        public void ShieldOff()
        {
            if (m_State == State.SHIELD_ON)
            {
                m_State = State.SHIELD_OFF;
                ShieldActive = false;
            }
        }
    }
}