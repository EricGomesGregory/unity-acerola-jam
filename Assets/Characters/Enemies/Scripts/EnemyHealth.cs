using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class EnemyHealth : IHealth
{
    [SerializeField, Min(0f)]
    private float total = 100f;
    [SerializeField, DisplayField]
    private float current;

    public event UnityAction<float> CurrentChanged;
    public event UnityAction<float> TotalChanged;


    public float Total {
        get => total;
        set {
            total = Mathf.Max(value, 0f);
            TotalChanged?.Invoke(total);
        }
    }
    public float Current {
        get => current;
        set {
            current = Mathf.Clamp(value, 0f, total);
            CurrentChanged?.Invoke(current);
        }
    }

    public void Damage(float value) {
        Current = Current - value;
    }

    public void Heal(float value) {
        Current = Current + value;
    }

    [ContextMenu("Reset")]
    public void Reset() {
        Current = Total;
    }

    public void Setup(IHealth.HealthData? data = null) {
        if (data != null) {
            Total = (float)(data?.Total);
            Current = (float)(data?.Current);
        }

        Reset();
    }
}
