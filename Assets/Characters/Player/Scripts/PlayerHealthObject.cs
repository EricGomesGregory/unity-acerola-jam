using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Game/Player/Healt", fileName = "new Health")]
public class PlayerHealthObject : ScriptableObject
{
    [SerializeField, Min(0f)]
    private float total = 100f;
    [SerializeField, DisplayField]
    private float current;

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

    public event UnityAction<float> TotalChanged;
    public event UnityAction<float> CurrentChanged;

    private void OnValidate() {
        Current = Total;
    }
}