using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Game/Settings/Audio", fileName = "new Audio Settings Bridge")]
public class AudioSettingsObject : ScriptableObject
{
    [SerializeReference]
    private AudioMixer mixer;

    public void SetMasterVolume(float value) {
        mixer.SetFloat("MasterVolume", value);
    }

    public void SetMusicVolume(float value) {
        mixer.SetFloat("MusicVolume", value);
    }

    public void SetEffectsVolume(float value) {
        mixer.SetFloat("EffectsVolume", value);
    }
}
