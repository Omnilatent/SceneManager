using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Omnilatent.SceneManager
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
        State m_State;

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
                    Active = true;

                    m_StartAlpha = 0;
                    m_EndAlpha = 1;

                    this.m_AnimationDuration = Manager.SceneFadeDuration;
                    this.Play();

                    m_State = State.ShieldFadeOut;
                }
            }
        }
    }
}