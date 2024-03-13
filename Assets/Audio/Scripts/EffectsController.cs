using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EffectsController
{
    [SerializeReference]
    private EffectAudioSource effectSource;
    [SerializeReference]
    private Transform parent;

    [SerializeField, Min(0)]
    private int size = 10;

    private List<EffectAudioSource> sources = null;
    private Queue<EffectAudioSource> queue = null;
    
    public void Initialize() {
        sources = new(size);
        queue = new(size);
        while(sources.Count <= size) {
            var element = AudioManager.Instantiate(effectSource, parent);
            element.Setup(Release);
            sources.Add(element);
            queue.Enqueue(element);
        }
    }

    public void PlayAt(AudioClip clip, Vector3 position) {
        var source = queue.Dequeue();
        source.PlayAt(clip, position);
    }

    public void PauseAll() {
        foreach (var source in sources) {
            source.Pause();
        }
    }

    private void Release(EffectAudioSource source) {
        queue.Enqueue(source);
    }
}
