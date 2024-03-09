using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeWeapon : MonoBehaviour
{
    [SerializeField]
    private Collider collider;
    [SerializeField]
    private LayerMask mask;

    private float damage;
    private List<ICharacter> hitCharacters = new ();

    public void Setup(float damage) {
        this.damage = damage;
        collider.isTrigger = true;
        collider.enabled = false;
    }

    public void BeginAttack() {
        Debug.Log("BeginAttack");
        collider.enabled = true;
    }

    public void EndAttack() {
        Debug.Log("EndAttack");
        collider.enabled = false;
        hitCharacters.Clear();
    }

    private void OnTriggerEnter(Collider other) {
        if ((mask.value & (1 << other.transform.gameObject.layer)) <= 0) {
            return;
        }
        
        if (other.gameObject.TryGetComponent<ICharacter>(out var target)) {
            if (hitCharacters.Contains(target) == false) {
                Debug.Log($"Dammageing {other.gameObject.name}");
                target.Health.Damage(damage);
                hitCharacters.Add(target);
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if ((mask.value & (1 << other.transform.gameObject.layer)) <= 0) {
            return;
        }

        if (other.gameObject.TryGetComponent<ICharacter>(out var target)) {
            if (hitCharacters.Contains(target) == true) {
                hitCharacters.Remove(target);
            }
        }
    }
}
