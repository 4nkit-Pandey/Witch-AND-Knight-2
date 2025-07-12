using UnityEngine;
using UnityEngine.AI;

public class KnightBehavior : MonoBehaviour
{
    public Transform witch;
    public float runSpeed = 3.5f;
    private float timeSeen = 0f;
    private bool isSeen = false;
    private bool isPossessed = false;

    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (isSeen && !isPossessed)
        {
            timeSeen += Time.deltaTime;
            agent.SetDestination(witch.position);

            if (timeSeen >= 3f)
            {
                PossessKnight();
            }
        }
    }

    public void SeenByWitch()
    {
        isSeen = true;
    }

    void PossessKnight()
    {
        isPossessed = true;
        agent.isStopped = true;
        Debug.Log("👁️ Knight possessed!");
        // Optionally add visual or sound effect here
        // TODO: Enable player control of knight here
    }
}
