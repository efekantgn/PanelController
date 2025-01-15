# PanelController System

## Documentation for PanelController System

### Overview
The `PanelController` class is a Unity script designed to manage UI panels using animations for movement, scaling, and fading. This script leverages the **DOTween** library to handle smooth transitions and animations, making it easy to show or hide UI panels with various effects.

### Features
- Smooth movement of panels on and off the screen.
- Scaling effects to show and hide panels.
- Fade-in and fade-out animations for UI elements.
- Customizable animation durations.
- Events triggered at the start and end of each animation for easy extensibility.

### Requirements
- Unity 2020.3 or later.
- DOTween must be installed in your project. Install it via the Asset Store or Unity's Package Manager.

### Installation
1. Ensure DOTween is installed in your Unity project.
2. Import the `PanelController` script into your project.
3. Attach the script to a GameObject containing a `RectTransform` and optionally a `CanvasGroup`.

### Usage
1. Attach the `PanelController` script to your UI panel GameObject.
2. Assign the panel's `RectTransform` and `CanvasGroup` in the inspector (optional; they will be added automatically if missing).
3. Customize the movement, scale, and fade durations in the inspector or via script.
4. Call the desired method to show or hide the panel with the desired animation:
   - `ShowPanel()`
   - `HidePanel()`
   - `ScaleIn()`
   - `ScaleOut()`
   - `FadeIn()`
   - `FadeOut()`

### Methods

#### ShowPanel()
Moves the panel into view with a smooth back-easing effect.

#### HidePanel()
Moves the panel off-screen with an in-back easing effect.

#### ScaleIn()
Scales the panel from zero to full size with a back-easing effect.

#### ScaleOut()
Scales the panel to zero with an in-back easing effect.

#### FadeIn()
Gradually fades the panel into view.

#### FadeOut()
Gradually fades the panel out of view.

### Custom Events
The script provides actions to hook into the start and completion of each animation. For example, you can listen for the completion of a fade-in animation using:

```csharp
panelController.OnFadeInComplete += () => Debug.Log("Fade-in complete!");
```

#### Available Events
- `OnShowPanelStart`, `OnShowPanelComplete`
- `OnHidePanelStart`, `OnHidePanelComplete`
- `OnScaleInStart`, `OnScaleInComplete`
- `OnScaleOutStart`, `OnScaleOutComplete`
- `OnFadeInStart`, `OnFadeInComplete`
- `OnFadeOutStart`, `OnFadeOutComplete`

