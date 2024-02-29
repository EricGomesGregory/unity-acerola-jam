using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private InputMap inputMap;
    private InputPlayerEvents playerEvents = new();

    public static InputManager Instance { get; private set; }
    public InputPlayerEvents Player { get => playerEvents; }

    private void Awake() {
        Instance = this; //TODO: Fix singleton
        inputMap = new InputMap();
    }

    private void OnEnable() {
        inputMap.Player.AddCallbacks(playerEvents);
        inputMap.Player.Enable();
    }

    private void OnDisable() {
        inputMap.Player.Disable();
        inputMap.Player.RemoveCallbacks(playerEvents);
    }
}
