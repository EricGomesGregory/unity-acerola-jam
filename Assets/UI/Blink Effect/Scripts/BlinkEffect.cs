using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkEffect : MonoBehaviour
{
    [SerializeReference]
    private Material material;
    [SerializeField]
    private AnimationCurve widthCurve;
    [SerializeField]
    private AnimationCurve heightCurve;
    [SerializeField, Min(0.01f)]
    private float frequency = 2f;

    private float time = 0f;

    private void Start() {
        material.SetFloat("_WidthValue", 0f);
        material.SetFloat("_HeightValue", 0f);
    }

    private void Update() {
        time = Time.time * frequency;

        float width = widthCurve.Evaluate(time);
        float height = heightCurve.Evaluate(time);

        material.SetFloat("_WidthValue", width);
        material.SetFloat("_HeightValue", height);
    }
}
