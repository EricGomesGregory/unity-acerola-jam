using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour, IInteractable
{
    [SerializeField]
    private ChestData data;
    [Space]
    [SerializeField]
    private Animator animator;

    public void Interact(ICharacter interactor) {
        animator.SetTrigger("Open");
    }

    public bool TryGetData(out InteractableData result) {
        result = data;
        return data == null;
    }
}

public class ChestData : InteractableData
{

}