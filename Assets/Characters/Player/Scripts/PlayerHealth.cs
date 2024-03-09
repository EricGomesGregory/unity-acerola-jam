using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class PlayerHealth : IHealth
{
    [SerializeReference]
    private PlayerHealthObject healthObject;

    public float Current => healthObject.Current;
    public float Total => healthObject.Total;

    public event UnityAction TakeDamage;
    public event UnityAction Death;

    public void Damage(float value) {
        healthObject.Current = healthObject.Current - value;
    }

    public void Heal(float value) {
        healthObject.Current = healthObject.Current + value;
    }

    public void Setup(IHealth.HealthData? data = null) {
        healthObject.CurrentChanged += HandleCurrentChanged;
        if (data != null) {
            healthObject.Total = (float)(data?.Total);
            healthObject.Current = (float)(data?.Current);
        }
        Debug.Log("Setup: health events");
    }

    public void Remove() {
        healthObject.CurrentChanged -= HandleCurrentChanged;
    }

    public void Reset() {
        Debug.Log($"PlayerHealth: Resetting Current: {healthObject.Current} to Total: {healthObject.Total}");
        healthObject.Current = healthObject.Total;
        Debug.Log($"PlayerHealth: Resetting Current: {healthObject.Current}");
    }

    private void HandleCurrentChanged(float current) {
        if (current <= 0) {
            Death?.Invoke();
            return;
        }

        TakeDamage?.Invoke();
    }
}