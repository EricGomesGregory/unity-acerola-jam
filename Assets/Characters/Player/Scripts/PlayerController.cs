using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, ICharacter
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private CharacterController character;
    [Space]
    [SerializeField, Range(0f, 1f)]
    private float turnSmoothing = 0.1f;
    [Space]
    [SerializeField]
    private PlayerHealth health = new();
    [SerializeField]
    private CharacterInteractor interactor = new();

    IHealth ICharacter.Health => health;
    public CharacterInteractor Interactor { get => interactor; }


    private Transform mainCamera;
    private Vector2 moveInput;

    private Vector3 moveDirection;
    private float turnSmoothVelocity;
    
    private void Start() {
        mainCamera = Camera.main.transform;
        health.Reset();
    }

    private void OnEnable() {
        InputManager.Instance.Player.Move += OnMove;
        InputManager.Instance.Player.Interact += interactor.OnInteract;
    }

    private void OnDisable() {
        
    }

    private void Update() {
        var isMoving = moveInput.magnitude > 0.1f;

        if (isMoving) {
            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothing);

            transform.rotation = Quaternion.Euler(transform.up * angle);
        }
        
        animator.SetBool("isMoving", isMoving);
        animator.SetFloat("Speed", moveInput.magnitude);
    }

    private void OnAnimatorMove() {
        Vector3 velocity = animator.deltaPosition;
        if (character.isGrounded) {
            velocity.y = 0f;
        }

        velocity.y += GameManager.Instance.Gravity * Time.deltaTime;

        character.Move(velocity);
    }


    private Vector3 GetMoveDirection() {
        var forward = mainCamera.forward;
        var right = mainCamera.right;
        forward.y = 0;
        right.y = 0;
        forward = forward.normalized;
        right = right.normalized;

        return forward * moveInput.y + right * moveInput.x;
    }

    #region Event Listeners

    private void OnMove(Vector2 value) {
        moveInput = value;
        moveDirection = GetMoveDirection();
    }

    #endregion
}
