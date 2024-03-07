using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyHealth : IHealth
{
    [SerializeField, Min(0f)]
    private float total = 100f;
    [SerializeField, DisplayField]
    private float current;

    public float Total {
        get => total;
        set => total = Mathf.Max(value, 0f); }
    public float Current {
        get => current;
        set => current = Mathf.Clamp(value, 0f, total);
    }

    public void Damage(float value) {
        Current = Current - value;
    }

    public void Heal(float value) {
        Current = Current + value;
    }

    public void Reset() {
        Current = Total;
    }

    public void Setup(IHealth.HealthData data) {
        Total = data.Total;
        Current = data.Current;
    }
}
