using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PuzzlePlatesController : MonoBehaviour
{
    [SerializeField]
    private List<PuzzlePlate> plates = new();
    [SerializeField]
    private List<int> order = new();

    [Header("Events")]
    [SerializeField]
    private UnityEvent OnSucceeded;

    private List<int> sequence = new();

    private event UnityAction reset;

#if UNITY_EDITOR
    private void OnValidate() {
        for (int i = 0; i < plates.Count; i++) {
            plates[i].SetId(i);
        }
    }
#endif
    private void Start() {
        foreach (var plate in plates) {
            plate.Triggered += OnPlateTriggered;
            plate.Released += CheckSequence;
            reset += plate.OnReset;
        }
    }

    private void OnPlateTriggered(int id) {
        sequence.Add(id);
    }

    private void CheckSequence() {
        if (sequence.Count > order.Count) {
            ResetSequence();
            return;
        }

        if (sequence.Count == order.Count) {
            for (int i = 0; i < sequence.Count; i++) {
                if (sequence[i] != order[i]) {
                    ResetSequence();
                    return;
                }
            }

            OnSucceeded?.Invoke();
        }
    }

    private void ResetSequence() {
        reset?.Invoke();
        sequence.Clear();
    }
}
