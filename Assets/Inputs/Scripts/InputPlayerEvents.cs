using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputPlayerEvents : InputMap.IPlayerActions
{
    public event Vector2Action Move;

    public void OnMove(InputAction.CallbackContext context) {
        var value = context.ReadValue<Vector2>();
        //Debug.Log($"OnMove: Value={value}");
        Move?.Invoke(value);
    }

    #region Delegates
    public delegate void Vector2Action(Vector2 value);
    #endregion
}
