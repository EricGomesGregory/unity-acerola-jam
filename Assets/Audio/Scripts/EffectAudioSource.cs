using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectAudioSource : MonoBehaviour
{
    [SerializeField]
    private AudioSource source;

    private event Action<EffectAudioSource> onRelease;

    public void Setup(Action<EffectAudioSource> relaseFunc) {
        onRelease += relaseFunc;
    }

    public void Play() {
        source.Play();
        HandleRelease(source.clip);
    }

    public void PlayAt(AudioClip clip, Vector3 position) {
        source.clip = clip;
        transform.position = position;
        source.Play();
        HandleRelease(clip);
    }

    public void Pause() {
        source.Pause();
    }

    public void Stop() {
        source.Stop();
        onRelease?.Invoke(this);
    }

    private async void HandleRelease(AudioClip clip) {
        await UniTask.Delay(ToMiliseconds(clip.length));
        onRelease?.Invoke(this);
    }

    private static int ToMiliseconds(float seconds) {
        return (int)(seconds * 1000f);
    }
}
