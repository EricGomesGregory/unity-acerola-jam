using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEmmiter : MonoBehaviour
{
    [SerializeField]
    private AudioSource source;

    public void Setup(Action<AudioEmmiter> relaseFunc) {
        HandleRelease(relaseFunc);
    }

    private async void HandleRelease(Action<AudioEmmiter> relaseFunc) {
        await UniTask.WaitUntil(() => source.isPlaying == false);
        relaseFunc(this);
    }

    public void Play() {
        source.Play();
    }

    public void Pause() {
        source.Pause();
    }

    public void Stop() {
        source.Stop();
    }
}
