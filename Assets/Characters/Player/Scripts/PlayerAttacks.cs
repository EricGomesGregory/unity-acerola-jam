using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerAttacks
{
    [SerializeField, Min(0f)]
    private float damage = 0f;
    [SerializeField]
    private PlayerMeleeWeapon meleeWeapon;

    
    public void Setup() {
        meleeWeapon.Setup(damage);
    }

    public void BeginAttack() {
        meleeWeapon.BeginAttack();
    }

    public void EndAttack() {
        meleeWeapon.EndAttack();
    }
}
