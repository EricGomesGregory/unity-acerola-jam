using UnityEngine;

public interface IInteractable
{
    public bool TryGetData(out InteractableData result);
    public void Interact(ICharacter interactor);
}

public class InteractableData: ScriptableObject {
}