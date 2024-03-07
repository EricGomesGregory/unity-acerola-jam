using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour, ICharacter
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private NavMeshAgent agent;
    
    [Header("Settings")]
    [SerializeField, Min(0f)]
    private float checkRange = 5f;
    [SerializeField]
    private LayerMask mask;
    
    [Space]
    [SerializeField]
    private EnemyHealth health;
    [SerializeField]
    private CharacterInteractor interactor;

    IHealth ICharacter.Health => health;

    private PlayerController player;
    
    private Vector2 smoothDeltaPosition;
    private Vector2 velocity;

    private NavMeshTriangulation Triangulation;


    private void Awake() {
        Triangulation = NavMesh.CalculateTriangulation();
        animator.applyRootMotion = true;
        agent.updatePosition = false;
        agent.updateRotation = true;
    }

    private void Start() {
        player = GameManager.Instance.Player;
        health.Reset();
    }

    private void Update() {
        if (ChechSpehere(interactor.Origin.position, checkRange, mask, out var others)) {
            foreach (var other in others) {
                if (other.CompareTag("Player")) {
                    agent.SetDestination(player.transform.position);
                }
            }
        }
            
        SynchronizeAnimatorAndAgent();
        
    }

    private void OnAnimatorMove() {
        Vector3 rootPosition = animator.rootPosition;
        rootPosition.y = agent.nextPosition.y;
        transform.position = rootPosition;
        agent.nextPosition = rootPosition;
    }

    private void SynchronizeAnimatorAndAgent() {
        Vector3 worldDeltaPosition = agent.nextPosition - transform.position;
        worldDeltaPosition.y = 0;
        // Map 'worldDeltaPosition' to local space
        float deltaRight = Vector3.Dot(transform.right, worldDeltaPosition);
        float deltaForward = Vector3.Dot(transform.forward, worldDeltaPosition);
        Vector2 deltaPosition = new Vector2(deltaRight, deltaForward);

        // Low-pass filter the deltaMove
        float smooth = Mathf.Min(1, Time.deltaTime / 0.1f);
        smoothDeltaPosition = Vector2.Lerp(smoothDeltaPosition, deltaPosition, smooth);

        velocity = smoothDeltaPosition / Time.deltaTime;
        if (agent.remainingDistance <= agent.stoppingDistance) {
            velocity = Vector2.Lerp(Vector2.zero, velocity, agent.remainingDistance);
        }

        bool shouldMove = velocity.magnitude > 0.5f || agent.remainingDistance > agent.stoppingDistance;

        animator.SetBool("isMoving", shouldMove);
        animator.SetFloat("Speed", velocity.magnitude);

        float deltaMagnitude = worldDeltaPosition.magnitude;
        if (deltaMagnitude > agent.radius / 2) {
            transform.position = Vector3.Lerp(animator.rootPosition, agent.nextPosition, smooth);
        }


    }


    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, checkRange);
    }

    private bool ChechSpehere(Vector3 position, float radius, LayerMask mask, out Collider[] result) {
        var data = Physics.OverlapSphere(position, radius, mask);
        result = data;
        return result != null;
    }
}
