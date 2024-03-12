using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputUIEvents : InputMap.IUIActions
{
    public event UnityAction Unpause;

    public void OnMenu(InputAction.CallbackContext context) {
        if (context.phase == InputActionPhase.Performed) {
            Unpause?.Invoke();
        }
    }
}
