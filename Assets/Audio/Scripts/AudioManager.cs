using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Pool;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioMixer mixer;
    [SerializeField]
    private AudioSettingsObject settings;

    [SerializeField]
    private MusicController music;
    [SerializeField]
    private EffectsController effects;

    public static AudioManager Instance { get; private set; }
    public MusicController Music { get => music; }
    public EffectsController Effects { get => effects; }

    private void Awake() {
        Instance = this;
    }

    private void OnEnable() {
        AddListeners();
    }

    private async void AddListeners() {
        await UniTask.WaitUntil(() => InputManager.Instance);

        InputManager.Instance.Player.Pause += OnPause;
        InputManager.Instance.UI.Unpause += OnUnpuase;
    }

    private void OnDisable() {
        
    }

    private void Start() {
        Effects.Initialize();
    }

    private void OnPause() {
        music.Pause();
    }

    private void OnUnpuase() {
        music.Play();
    }
}
