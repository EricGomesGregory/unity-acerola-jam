using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeReference]
    private Canvas ui;
    [SerializeReference]
    private RectTransform mainMenu;
    [SerializeReference]
    private RectTransform settingsMenu;

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
        GoToMainMenu();
        ui.gameObject.SetActive(false);
    }

    public void GoToMainMenu() {
        mainMenu.gameObject.SetActive(true);
        settingsMenu.gameObject.SetActive(false);
    }

    public void GoToSettings() {
        mainMenu.gameObject.SetActive(false);
        settingsMenu.gameObject.SetActive(true);
    }


    private void OnPause() {
        ui.gameObject.SetActive(true);
    }

    private void OnUnpause() {
        ui.gameObject.SetActive(false);
    }
}
