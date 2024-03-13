using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioController : MonoBehaviour
{
    [SerializeField]
    private PlayerAudioEffects footsteps;
    [SerializeField]
    private PlayerAudioEffects attacks;

    public void PlayFootstep() {
        var clip = footsteps.GetRandomClip();
        AudioManager.Instance.Effects.PlayAt(clip, transform.position);
    }

    public void PlayAttack() {
        var clip = attacks.GetRandomClip();
        AudioManager.Instance.Effects.PlayAt(clip, transform.position);
    }
}
