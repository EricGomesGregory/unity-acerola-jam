using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private CharacterController character;
    [SerializeField]
    private Animator animator;

    [Header("Settings")]
    [SerializeField, Range(0f, 1f)]
    private float turnSpeed = 0.1f;

    private Vector2 moveInput;
    
    private Transform povCamera;
    private Vector3 moveDirection;
    private float turnSmoothVelocity;

    private void OnEnable() {
        AddInputListeners();
    }

    private async void AddInputListeners() {
        await UniTask.WaitUntil(() => InputManager.Instance);
        
        InputManager.Instance.Player.Move += OnMove;
    }

    private void Start() {
        povCamera = Camera.main.transform;
        animator.applyRootMotion = true;
    }

    private void OnDisable() {
        InputManager.Instance.Player.Move -= OnMove;
    }

    private void Update() {
        bool isMoving = moveInput != Vector2.zero;
        if (isMoving) {
            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSpeed);
            transform.rotation = Quaternion.Euler(transform.up * angle);
        }

        animator.SetBool("isMoving", isMoving);
        animator.SetFloat("Speed", moveInput.magnitude);
    }

    private void OnAnimatorMove() {
        var direction = animator.deltaPosition;
        character.Move(direction);
    }

    private Vector3 GetMoveDirection() {
        var forward = povCamera.forward;
        var right = povCamera.right;
        forward.y = 0;
        right.y = 0;

        return forward.normalized * moveInput.y + right.normalized * moveInput.x;
    }

    #region Event Listeners

    private void OnMove(Vector2 value) {
        moveInput = value;
        moveDirection = GetMoveDirection();
    }
    
    #endregion
}
