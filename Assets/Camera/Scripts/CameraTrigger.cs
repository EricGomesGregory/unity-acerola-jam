using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    [SerializeField]
    CinemachineVirtualCamera virtualCamera;

    private void Start() {
        virtualCamera.LookAt = GameManager.Instance.Player.transform;
        virtualCamera.Priority = 0;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")){
            virtualCamera.Priority = 1;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            virtualCamera.Priority = 0;
        }
    }
}
