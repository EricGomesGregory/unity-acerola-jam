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

    public void ForceUnpause() {
        Unpause?.Invoke();
    }

    public void OnNavigate(InputAction.CallbackContext context) {
        Debug.LogWarning("OnNavigate: To be implemented.");
    }

    public void OnSubmit(InputAction.CallbackContext context) {
        Debug.LogWarning("OnSubmit: To be implemented.");
    }

    public void OnCancel(InputAction.CallbackContext context) {
        Debug.LogWarning("OnCancel: To be implemented.");
    }

    public void OnPoint(InputAction.CallbackContext context) {
        Debug.LogWarning("OnPoint: To be implemented.");
    }

    public void OnClick(InputAction.CallbackContext context) {
        Debug.LogWarning("OnClick: To be implemented.");
    }

    public void OnScrollWheel(InputAction.CallbackContext context) {
        Debug.LogWarning("OnScrollWheel: To be implemented.");
    }

    public void OnMiddleClick(InputAction.CallbackContext context) {
        Debug.LogWarning("OnMiddleClick: To be implemented.");
    }

    public void OnRightClick(InputAction.CallbackContext context) {
        Debug.LogWarning("OnRightClick: To be implemented.");
    }

    public void OnTrackedDevicePosition(InputAction.CallbackContext context) {
        Debug.LogWarning("OnTrackedDevicePosition: To be implemented.");
    }

    public void OnTrackedDeviceOrientation(InputAction.CallbackContext context) {
        Debug.LogWarning("OnTrackedDeviceOrientation: To be implemented.");
    }
}
