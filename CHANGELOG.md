# 1.0.0
News:
- Add Popup template scene. Move template scene to FullScene folder.
- Add getter for active main controller.
- Allow customize the amount of layer order addition each scene added.

Changes
- Template scene: set Canvas's Layer to UI, disable Simple Animation unnecessary call to Show() on Start() in inspector.
- No longer requires Simple Animation and DOTween to be installed first for code to compile.
- Loading anywhere hide: check cached loading screens key to prevent key not found exception.

Fixes
- Fix SceneCreatorWindow exception when generating scene.

# 0.2.1
News:
- Add callbacks onSceneLoadStart, onSceneLoaded, onSceneHidden when main scene is changing.

# 0.2.0
Changes:
- Deprecate Canvas field in Controller. Use List<Canvas> Canvases field instead to allow a scene to has multiple Canvases.
- If scene's canvas's sorting order is set to a value different from 0, its sorting order will not be changed to the scene's sorting order.

# 0.1.0
- First version.