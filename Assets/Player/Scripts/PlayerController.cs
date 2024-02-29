using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector2 moveInput;

    private void OnEnable() {
        AddInputListeners();
    }

    private async void AddInputListeners() {
        await UniTask.WaitUntil(() => InputManager.Instance);
        
        InputManager.Instance.Player.Move += OnMove;
    }

    private void OnDisable() {
        InputManager.Instance.Player.Move -= OnMove;
    }

    private void OnMove(Vector2 value) {
        moveInput = value;
    }
}
