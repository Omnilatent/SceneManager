
# Dependencies
- Simple Animation: https://github.com/JacatGameStudio/SimpleAnimation

# Setup
- Import Extra package: go to "Tools/Omnilatent/Scenes Manager/Import Extra Package" or import the package "ScenesManagerExtra" included in the asset.
- In case the project use URP, go to "Tools/Omnilatent/Scenes Manager/Import Extra Package URP" to import additional Manager Object specifically made for URP.

# Usage
## Create new scene
- Go to "Tools/Omnilatent/Scenes Manager/Generate Scene".
- Parameters:
    - Scene Name: name of new scene.
    - Scene Directory Path: path to parent folder where new scene will be created. Relative to project's Assets folder.
    - Scene Template File: name of the template scene file. Set to "TemplateScene.unity" for default.
    - Fullscreen: if true, background scenes will be deactivated when this scene is added.
- When you're ready to create new scene, press "Generate".

## Loading scene
There are 3 ways to load scene:

### `Manager.Load(string sceneName, object data = null)`
Fade out current scene and load new scene. New scene will be the main scene, all currently loaded scenes will be unloaded.

---
### `Manager.LoadAsync(string sceneName, object data = null, Action onSceneLoaded = null, Action<float> onProgressUpdate = null)`
Similar to Manager.Load() but the scene will be loaded asynchronous. `onProgressUpdate` will be called every 20 ms with the scene load progress's percentage (from 0 to 0.9).

`onSceneLoaded` will be called when the progress reach 0.9 and the scene has completed loading.

---
### `Manager.Add(string sceneName, object data = null, Action onShown = null, Action onHidden = null)`
New scene will be loaded addictively and placed on top of the currently loaded scene(s).

## Passing data between scene

When you use Manager.Load() or Manager.Add(), you can pass data between scene using paramater `data`. The new scene will receive data in its controller's OnActive() method. The data can be any type.

E.g:
```
int stageID = 1;
Manager.Load(GameController.GAME_SCENE_NAME, stageID); //pass stage ID to the next scene
```

```
public class GameController : Controller
    {
        public const string GAME_SCENE_NAME = "Game";
        public override void OnActive(object data)
        {
            base.OnActive(data);
            var stageID = (int)data;
            Debug.Log($"Stage {stageID}"); //logged: "Stage 1"
        }
    }
```


## Setup camera
When ScenesManager is initialized, the Manager Object (located in Assets/Omnilatent/Extra/ScenesManagerExtra/ after you've imported the extra files) will be instantiated.

The Manager Object contains BG Camera (for rendering world objects) and UI Camera.

If you want to use the scene's camera instead of BG Camera, inspect the scene controller and drag the scene camera into the Camera field. When this scene is loaded, Manager Object's BG Camera will be disabled.

## Custom Shield Color
When an addictive scene is added using Manager.Add(), a shield will be placed on top of the main scene, darkened it and block interaction with the main scene's UI.

The shield's color can be customized using Manager.ShieldColor property. E.g: `Manager.ShieldColor = new Color(0f,0f,0.5f,0.5f);`.