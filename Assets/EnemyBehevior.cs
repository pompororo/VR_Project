using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyBehavior : MonoBehaviour
{
    protected NavMeshAgent agent;
    public int health; // Health points of the enemy
    protected EnemyState currentState = EnemyState.Idle;
    public float maxSightDistance ; // Adjust this value as needed
    public float fieldOfViewAngle ; // Adjust this value to set the field of view angle
    protected Transform FindNearestCover(string coverTag)
    {
        GameObject[] coverObjects = GameObject.FindGameObjectsWithTag(coverTag);

        Transform nearestCover = null;
        float nearestDistance = float.MaxValue;

        foreach (GameObject coverObject in coverObjects)
        {
            CoverTerrain coverTerrain = coverObject.GetComponent<CoverTerrain>();

            if (coverTerrain != null && coverTerrain.transformList.Count > 0)
            {
                foreach (Transform coverTransform in coverTerrain.transformList)
                {
                    float distance = Vector3.Distance(transform.position, coverTransform.position);
                    if (distance < nearestDistance)
                    {
                        nearestDistance = distance;
                        nearestCover = coverTransform;
                    }
                }
            }
        }

        return nearestCover;
    }

    protected void MoveToTarget(Transform targetToGo)
    {
        if (targetToGo != null)
        {
            agent.SetDestination(targetToGo.position);
        }
        else
        {
         
        }
    }
    
    

    protected void TakeDamage(int damage)
    {
        health -= damage;

        // Check if the enemy is still alive
        if (health <= 0)
        {
            Die();
        }
    }
    protected void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            TakeDamage(50);
        }
        else if (other.CompareTag("BladeSaber"))
        {
            TakeDamage(80);
        }
    }

    protected void Die()
    {
        // Implement your logic for enemy death here
        // For example, play death animation, destroy the object, etc.
        Destroy(gameObject);
    }
    
    protected void SetState(EnemyState newState)
    {
        currentState = newState;
    }
    
   public bool IsInFieldOfView(Vector3 targetPosition)
    {
        Vector3 directionToTarget = targetPosition - transform.position;
        float angleToTarget = Vector3.Angle(transform.forward, directionToTarget);

        // Check if the target is within the field of view angle
        if (angleToTarget < fieldOfViewAngle * 0.5f)
        {
            RaycastHit hit;

            // Adjust the layer mask as needed to include only your obstacles layer
            int layerMask = 1 << LayerMask.NameToLayer("Player");

            // Cast a capsule to represent the triangular field of view
            if (Physics.CapsuleCast(transform.position, transform.position + transform.forward * maxSightDistance,
                    maxSightDistance * 0.5f, directionToTarget.normalized, out hit, maxSightDistance, layerMask))
            {
                if (hit.collider.CompareTag("Player")) // Change "Obstacle" to the tag of your obstacles
                {
                    // If an obstacle is blocking the line of sight, return false
                    return false;
                }
            }

            // If no obstacles are blocking the line of sight, return true
            return true;
        }

        return false;
    }
    public enum EnemyState
    {
        Idle,
        Walk,
        Fire,
        Run,
        TakeCover,
        Formations 
    }
}