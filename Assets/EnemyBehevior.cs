using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyBehavior : MonoBehaviour
{
    protected NavMeshAgent agent;
    public int health; // Health points of the enemy
    protected EnemyState currentState = EnemyState.Idle;

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