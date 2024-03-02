using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputPlayerEvents : InputMap.IPlayerActions
{
    public event ValueAction<Vector2> Move;
    public event ValueAction<bool> Jump;

    public void OnJump(InputAction.CallbackContext context) {
        var value = context.ReadValueAsButton();
        Jump?.Invoke(value);
    }

    public void OnMove(InputAction.CallbackContext context) {
        var value = context.ReadValue<Vector2>();
        //Debug.Log($"OnMove: Value={value}");
        Move?.Invoke(value);
    }

    #region Delegates
    public delegate void ValueAction<T>(T value);
    #endregion
}
