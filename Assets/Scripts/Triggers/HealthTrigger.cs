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
        if (other.TryGetComponent<ICharacter>(out var character) == false) {
            return;
        }
        
        switch (type) {
            case InteractionType.Damage:
                character.Health.Damage(value);
                break;
            case InteractionType.Heal:
                character.Health.Heal(value);
                break;
        }
    }

    public enum InteractionType
    { Damage, Heal }
}
