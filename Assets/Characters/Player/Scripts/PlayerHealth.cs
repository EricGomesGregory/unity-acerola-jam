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

    public void Damage(float value) {
        healthObject.Current = healthObject.Current - value;
    }

    public void Heal(float value) {
        healthObject.Current = healthObject.Current + value;
    }

    public void Setup(IHealth.HealthData data) {
        healthObject.Total = data.Total;
        healthObject.Current = data.Current;
    }

    public void Reset() {
        Debug.Log($"PlayerHealth: Resetting Current: {healthObject.Current} to Total: {healthObject.Total}");
        healthObject.Current = healthObject.Total;
        Debug.Log($"PlayerHealth: Resetting Current: {healthObject.Current}");
    }

    public void AddEventToCurrentChanged(UnityAction<float> callback) {
        healthObject.CurrentChanged += callback;
    }

    public void RemoveEventFromCurrentChanged(UnityAction<float> callback) {
        healthObject.CurrentChanged -= callback;
    }

    public void AddEventToTotalChanged(UnityAction<float> callback) {
        healthObject.TotalChanged += callback;
    }

    public void RemoveEventFromTotalChanged(UnityAction<float> callback) {
        healthObject.TotalChanged -= callback;
    }
}