using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterInteractor : IInteractor
{
    [SerializeField]
    private Transform origin;
    [Space]
    [SerializeField, Min(0f)]
    private float range = 0.5f;
    [SerializeField, Min(0f)]
    private float radius = 0.5f;
    [SerializeField]
    private LayerMask mask;

    private ICharacter interactor;

    public Transform Origin { get => origin; }

    public void Initialize(ICharacter interactor) {
        this.interactor = interactor;
    }

    public void OnInteract() {
        var ray = new Ray(origin.position, origin.forward);
        RaycastHit hit;
        if (Physics.SphereCast(ray, radius, out hit, range, mask) == false) {
            return;
        }

        if (hit.collider.TryGetComponent<IInteractable>(out var interactable)) {
            interactable.Interact(interactor);
        }
    }
}
