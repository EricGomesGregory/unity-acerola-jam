using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private InputMap inputMap;
    private InputPlayerEvents playerEvents = new();
    private InputUIEvents uiEvents = new();

    public static InputManager Instance { get; private set; }
    public InputPlayerEvents Player { get => playerEvents; }
    public InputUIEvents UI { get => uiEvents; }

    private void Awake() {
        Instance = this; //TODO: Fix singleton
        inputMap = new InputMap();
    }

    private void OnEnable() {
        inputMap.Player.AddCallbacks(playerEvents);
        inputMap.UI.AddCallbacks(uiEvents);
        
        playerEvents.Pause += OnPause;

        uiEvents.Unpause += OnUnpause;

        inputMap.Player.Enable();
    }

    private void OnDisable() {
        inputMap.Player.Disable();
        inputMap.Player.RemoveCallbacks(playerEvents);
        inputMap.UI.RemoveCallbacks(uiEvents);
    }

    public void Pause() {
        playerEvents.ForcePause();
    }

    private void OnPause() {
        inputMap.Player.Disable();
        inputMap.UI.Enable();
    }

    public void Unpause() {
        uiEvents.ForceUnpause();
    }

    private void OnUnpause() {
        inputMap.Player.Enable();
        inputMap.UI.Disable();
    }
}
