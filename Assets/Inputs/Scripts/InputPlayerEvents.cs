using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputPlayerEvents : InputMap.IPlayerActions
{
    public event ValueAction<Vector2> Move;
    public event ValueAction<bool> Jump;
    public event UnityAction Interact;
    public event UnityAction Attack;

    public event UnityAction Pause;
    
    
    public void OnJump(InputAction.CallbackContext context) {
        var value = context.ReadValueAsButton();
        Jump?.Invoke(value);
    }

    public void OnMove(InputAction.CallbackContext context) {
        var value = context.ReadValue<Vector2>();
        //Debug.Log($"OnMove: Value={value}");
        Move?.Invoke(value);
    }

    public void OnInteract(InputAction.CallbackContext context) {
        if (context.phase == InputActionPhase.Performed) {
            Interact?.Invoke();
        }
    }

    public void OnAttack(InputAction.CallbackContext context) {
        if (context.phase == InputActionPhase.Performed) {
            Attack?.Invoke();
        }
    }

    public void OnMenu(InputAction.CallbackContext context) {
        if (context.phase == InputActionPhase.Performed) {
            Pause?.Invoke();
        }
    }

    #region Delegates
    public delegate void ValueAction<T>(T value);
    #endregion
}
