using UnityEngine;
using UnityEngine.AI;

public class KnightBehavior : MonoBehaviour
{
    public Transform witch;
    public float chaseRange = 10f;
    public float patrolSpeed = 2f;
    public float chaseSpeed = 3.5f;
    public Transform leftPoint;
    public Transform rightPoint;

    private NavMeshAgent agent;
    private bool isChasing = false;
    private bool isPossessed = false;
    private float seenTimer = 0f;
    private Transform currentTarget;

    private Animator knightAnimator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        knightAnimator = GetComponentInChildren<Animator>();

        currentTarget = rightPoint;
        agent.speed = patrolSpeed;
        GoToNextPatrolPoint();
    }

    void Update()
    {
        if (isPossessed)
        {
            agent.isStopped = true;
            UpdateAnimation();  // Ensure animation reflects stopped state
            return;
        }

        float distanceToWitch = Vector3.Distance(transform.position, witch.position);

        // Start chasing if within range
        if (distanceToWitch < chaseRange && !isChasing)
        {
            isChasing = true;
            seenTimer = 0f;
            agent.speed = chaseSpeed;
        }

        if (isChasing)
        {
            agent.SetDestination(witch.position);
            seenTimer += Time.deltaTime;

            if (seenTimer >= 3f)
            {
                isPossessed = true;
                Debug.Log("✅ Knight Possessed!");
                // Possession logic can go here if needed
            }
        }
        else
        {
            Patrol();
        }

        UpdateAnimation();
    }

    void Patrol()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.3f)
        {
            currentTarget = (currentTarget == leftPoint) ? rightPoint : leftPoint;
            GoToNextPatrolPoint();
        }
    }

    void GoToNextPatrolPoint()
    {
        if (currentTarget != null)
        {
            agent.speed = patrolSpeed;
            agent.SetDestination(currentTarget.position);
        }
    }

    void UpdateAnimation()
    {
        bool isWalking = agent.velocity.magnitude > 0.1f;
        if (knightAnimator != null)
        {
            knightAnimator.SetBool("isWalking", isWalking);
        }
    }

    public void SeenByWitch()
    {
        isChasing = true;
    }
}
