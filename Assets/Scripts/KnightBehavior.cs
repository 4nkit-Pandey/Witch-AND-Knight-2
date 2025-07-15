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
    public Animator knightAnimator;          // ✅ drag KnightModel's Animator here

    private NavMeshAgent agent;
    private bool isChasing = false;
    private bool isPossessed = false;
    private float seenTimer = 0f;
    private Transform currentTarget;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();  // ✅ make sure this is on KnightFPS
        currentTarget = rightPoint;
        agent.speed = patrolSpeed;
        GoToNextPatrolPoint();
    }

    void Update()
    {
        if (isPossessed)
        {
            agent.isStopped = true;
            knightAnimator.SetBool("isWalking", false);
            return;
        }

        float distanceToWitch = Vector3.Distance(transform.position, witch.position);

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
                // You can trigger control transfer here
            }
        }
        else
        {
            Patrol();
        }

        // ✅ Updated animation control based on agent velocity
        bool isWalking = agent.velocity.magnitude > 0.1f && !isPossessed;
        knightAnimator.SetBool("isWalking", isWalking);
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

    public void SeenByWitch()
    {
        isChasing = true;
    }
}
