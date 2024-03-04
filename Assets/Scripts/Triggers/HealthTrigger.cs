using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthTrigger : MonoBehaviour
{
    [SerializeField]
    private InteractionType type;
    [SerializeField, Min(0f)]
    private float value = 0f;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            HandlePlayer(other);
        }
    }

    private void HandlePlayer(Collider other) {
        if (other.TryGetComponent<PlayerController>(out var player) == false) {
            return;
        }

        switch (type) {
            case InteractionType.Damage:
                player.Health.Damage(value);
                break;
            case InteractionType.Heal:
                player.Health.Heal(value);
                break;
        }
    }

    public enum InteractionType
    { Damage, Heal }
}
