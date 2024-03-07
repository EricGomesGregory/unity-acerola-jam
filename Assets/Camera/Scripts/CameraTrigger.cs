using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraTrigger : MonoBehaviour
{
    [SerializeField]
    CinemachineVirtualCamera virtualCamera;
    [Header("Events")]
    [SerializeField]
    private PlayerEvents Player;


    private void Start() {
        virtualCamera.LookAt = GameManager.Instance.Player.transform;
        virtualCamera.Priority = 0;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.TryGetComponent<PlayerController>(out var player)) {
            virtualCamera.Priority = 1;
            Player.OnEnter.Invoke(player);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.TryGetComponent<PlayerController>(out var player)) {
            virtualCamera.Priority = 0;
            Player.OnExit.Invoke(player);
        }
    }

    [System.Serializable]
    public struct PlayerEvents
    {
        public UnityEvent<PlayerController> OnEnter;
        [Space]
        public UnityEvent<PlayerController> OnExit;
    }
}
