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
    [SerializeField, Min(0)]
    private int defaultSize = 10;
    [SerializeField, Min(0)]
    private int maxSize = 20;

    private ObjectPool<AudioEmmiter> pool;

    public static AudioManager Instance { get; private set; }


    private void Awake() {
        Instance = this;
    }

    private void Start() {
        pool = new ObjectPool<AudioEmmiter>(CreateAudioemmiter, GetAudioEmmiter, ReleaseAudioEmmiter, DestroyAudioEmmiter, false, defaultSize, maxSize);
    }

    public bool TryGet(out AudioEmmiter emmiter) {
        emmiter = pool.Get();
        return emmiter;
    }

    #region Pool 

    private AudioEmmiter CreateAudioemmiter() {
        var emmiter = Instantiate(new AudioEmmiter(), transform);
        emmiter.Setup(ReleaseAudioEmmiter);
        return emmiter;
    }

    private void GetAudioEmmiter(AudioEmmiter emmiter) {
        emmiter.Play();
    }

    private void ReleaseAudioEmmiter(AudioEmmiter emmiter) {
        emmiter.Pause();
    }

    private void DestroyAudioEmmiter(AudioEmmiter emmiter) {
        Destroy(emmiter);
    }

    #endregion
}
