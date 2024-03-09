using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeWeapon : MonoBehaviour
{
    [SerializeField]
    private Collider collider;
    [SerializeField]
    private LayerMask mask;
    [SerializeField]
    private List<string> whiteList = new() { "Enemy" };

    private float damage;
    private GameObject selfObject;
    [SerializeField]
    private List<ICharacter> hitCharacters = new();

    public void Setup(EnemyController self, float damage) {
        this.damage = damage;
        selfObject = self.gameObject;
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

        if (other.gameObject == selfObject || InWhiteList(other)) {
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

        if (other.gameObject == selfObject || InWhiteList(other)) {
            return;
        }

        if (other.gameObject.TryGetComponent<ICharacter>(out var target)) {
            if (hitCharacters.Contains(target) == true) {
                hitCharacters.Remove(target);
            }
        }
    }

    private bool InWhiteList(Collider other) {
        return whiteList.Contains(other.gameObject.tag);
    }
}
