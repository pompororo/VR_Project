using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyBehavior : MonoBehaviour
{
    public Transform target; // Reference to the player
    protected NavMeshAgent agent;
    protected int health; // Health points of the enemy

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Initialize();
    }

    protected virtual void Initialize()
    {
        // This method can be overridden in derived classes for additional initialization
    }

    protected void MoveToTarget(Transform targetToGo)
    {
        if (targetToGo != null)
        {
            agent.SetDestination(targetToGo.position);
        }
        else
        {
            Debug.LogError("Target is null in MoveToTarget method.");
        }
    }
}