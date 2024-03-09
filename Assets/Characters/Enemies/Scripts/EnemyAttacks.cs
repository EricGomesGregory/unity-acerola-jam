using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyAttacks
{
    [SerializeField, Min(0f)]
    private float damage = 0f;
    [SerializeField, Min(0f)]
    private float range = 2f;
    [SerializeField]
    private EnemyMeleeWeapon meleeWeapon;

    public float Range { get => range; }

    public void Setup(EnemyController self) {
        meleeWeapon.Setup(self, damage);
    }

    public void BeginAttack() {
        meleeWeapon.BeginAttack();
    }

    public void EndAttack() {
        meleeWeapon.EndAttack();
    }
}
