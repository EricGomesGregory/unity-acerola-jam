using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PlayerHealth : IHealth
{
    [SerializeReference]
    private PlayerHealthObject healthObject;

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
}