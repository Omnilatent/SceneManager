using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Omnilatent.ScenesManager
{
    public class ActivateManagerCameraOnDestroy : MonoBehaviour
    {
        private void Start()
        {
            Manager.Object.ToggleBackgroundCamera(false);
        }

        private void OnDestroy()
        {
            Manager.Object.ToggleBackgroundCamera(true);
        }
    }
}