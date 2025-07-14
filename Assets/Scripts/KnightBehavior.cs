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
        currentTarget = rightPoint;
        agent.speed = patrolSpeed;
        GoToNextPatrolPoint();

        knightAnimator = GetComponentInChildren<Animator>();

        // Warn if Root Motion is accidentally ON
        if (knightAnimator != null && knightAnimator.applyRootMotion)
        {
            Debug.LogWarning("⚠️ Knight Animator has Root Motion enabled! Please disable it for proper NavMeshAgent animation control.");
        }
    }

    void Update()
    {
        if (isPossessed)
        {
            agent.isStopped = true;
            SetWalking(false);  // stop animation too
            return;
        }

        float distanceToWitch = Vector3.Distance(transform.position, witch.position);

        // Start chasing if Witch is close
        if (distanceToWitch < chaseRange && !isChasing)
        {
            isChasing = true;
            seenTimer = 0f;
            agent.speed = chaseSpeed;
            agent.isStopped = false;
            Debug.Log("👀 Witch detected — Knight starts chasing!");
        }

        if (isChasing)
        {
            agent.SetDestination(witch.position);
            seenTimer += Time.deltaTime;

            if (seenTimer >= 3f)
            {
                isPossessed = true;
                Debug.Log("✅ Knight Possessed!");
                agent.velocity = Vector3.zero;
                agent.isStopped = true;
                SetWalking(false);
            }
        }
        else
        {
            Patrol();
        }

        // 🔄 Animation update
        if (!isPossessed)
        {
            SetWalking(agent.velocity.magnitude > 0.1f);
        }
    }

    void Patrol()
    {
        if (agent.isStopped)
            agent.isStopped = false;

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

    void SetWalking(bool state)
    {
        if (knightAnimator != null)
        {
            knightAnimator.SetBool("isWalking", state);
        }
    }
}
