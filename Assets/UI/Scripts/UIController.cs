using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeReference]
    private Canvas ui;

    private void OnEnable() {
        SetupEvemntListeners();
    }

    private async void SetupEvemntListeners() {
        await UniTask.WaitUntil(() => InputManager.Instance);
        InputManager.Instance.Player.Pause += OnPause;
        InputManager.Instance.UI.Unpause += OnUnpause;
    }

    private void OnDisable() {
        InputManager.Instance.Player.Pause -= OnPause;
        InputManager.Instance.UI.Unpause -= OnUnpause;
    }

    private void Start() {
        ui.gameObject.SetActive(false);
    }

    private void OnPause() {
        ui.gameObject.SetActive(true);
    }

    private void OnUnpause() {
        ui.gameObject.SetActive(false);
    }
}
