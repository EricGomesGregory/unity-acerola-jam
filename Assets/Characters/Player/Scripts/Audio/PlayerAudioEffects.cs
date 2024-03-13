using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerAudioEffects
{
    [SerializeField]
    private List<AudioClip> clips = new();

    public AudioClip GetRandomClip() {
        var index = Random.Range(0, clips.Count - 1);
        return clips[index];
    }
}
