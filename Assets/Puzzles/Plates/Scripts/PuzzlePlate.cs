using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PuzzlePlate : MonoBehaviour
{
    [SerializeField, DisplayField]
    private int id = -1;

    [SerializeField]
    private UnityEvent OnPressed;
    [SerializeField]
    private UnityEvent OnReleased;

    private bool isArmed = true;
    private GameObject character;

    public bool Pressed { get => character != null; }

    public event UnityAction<int> Triggered;
    public event UnityAction Released;

#if UNITY_EDITOR
    public void SetId(int value) {
        id = value;
    }
#endif

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player") == false) {
            return;
        }
        
        character = other.gameObject;

        if (isArmed) {
            Triggered?.Invoke(id);
            OnPressed?.Invoke();
            isArmed = false;
        }
    }

    private void OnTriggerExit(Collider other) {
        Debug.Log("[PuzzlePlato] OnTriggerExit: Begin");
        if (other.gameObject == character) {
            character = null;
            Released?.Invoke();
            OnReleased?.Invoke();
        }
    }

    public void OnReset() {
        isArmed = true;
    }
}
