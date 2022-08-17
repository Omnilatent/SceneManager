using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Omnilatent.Utils
{
    public interface ILoadingScreen
    {
        float GetProgress();
        void SetProgress(float value);
        void Show();
        void Hide();
        GameObject GetGameObject();
    }
}
