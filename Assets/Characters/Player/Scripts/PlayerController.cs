using Cysharp.Threading.Tasks;
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
    [SerializeField]
    private PlayerAttacks attacks = new();

    IHealth ICharacter.Health => health;
    public CharacterInteractor Interactor { get => interactor; }
    public PlayerAttacks Attacks { get => attacks; }

    private Transform mainCamera;
    private Vector2 moveInput;

    private Vector3 moveDirection;
    private float turnSmoothVelocity;
    private bool hasTakenDamage = false;
    
    private void Start() {
        mainCamera = Camera.main.transform;
        health.Setup();
        attacks.Setup();
    }

    private void OnEnable() {
        AddEventListeners();
    }

    private async void AddEventListeners() {
        await UniTask.WaitUntil(() => InputManager.Instance);
        await UniTask.WaitUntil(() => InputManager.Instance.Player != null);

        InputManager.Instance.Player.Move += OnMove;
        InputManager.Instance.Player.Interact += interactor.OnInteract;
        InputManager.Instance.Player.Attack += OnAttack;

        health.Death += OnDeath;
        health.TakeDamage += OnTakeDamage;
    }

    private void OnDisable() {
        InputManager.Instance.Player.Move -= OnMove;
        InputManager.Instance.Player.Interact -= interactor.OnInteract;

        health.Death -= OnDeath;
        health.TakeDamage -= OnTakeDamage;
    }

    private void Update() {
        if (animator.GetBool("isDead")) {
            return;
        }

        var isMoving = moveInput.magnitude > 0.1f;

        if (isMoving) {
            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothing);

            transform.rotation = Quaternion.Euler(transform.up * angle);
        }
        
        animator.SetBool("isMoving", isMoving);
        animator.SetFloat("Speed", moveInput.magnitude);

        hasTakenDamage = false;
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

    public void BeginAttack() {
        if (animator.GetBool("isDead")) {
            return;
        }

        attacks.BeginAttack();
    }

    public void EndAttack() {
        if (animator.GetBool("isDead")) {
            return;
        }

        attacks.EndAttack();
    }

    #region Event Listeners

    private void OnMove(Vector2 value) {
        moveInput = value;
        moveDirection = GetMoveDirection();
    }

    private void OnAttack() {
        if (animator.GetBool("isDead")) {
            return;
        }
        animator.SetTrigger("Attack");
    }

    private void OnDeath() {
        Debug.Log("OnDeath");
        animator.SetBool("isDead", true);
    }

    private void OnTakeDamage() {
        if (hasTakenDamage) {
            return;
        }

        Debug.Log("OnTakeDamage");
        animator.SetTrigger("TakeDamage");

        hasTakenDamage = true;
    }

    #endregion
}
