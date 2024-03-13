using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class MusicController
{
    [SerializeReference]
    private AudioSource source;

    public void Play(AudioClip clip = null) {
        if (clip == null) {
            source.Play();
            Debug.Assert(source.clip != null, "Play: Source has no audio clip to play.");
            return;
        }

        source.clip = clip;
        source.Play();
    }

    public void Pause() {
        source.Pause();
    }

    public void Stop() {
        source.Stop();
    }
}
