using Omnilatent.ScenesManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupTemplateController : Controller
{
    public const string POPUPTEMPLATE_SCENE_NAME = "PopupTemplate";

    public override string SceneName()
    {
        return POPUPTEMPLATE_SCENE_NAME;
    }
}
