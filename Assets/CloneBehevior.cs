using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class CloneBehavior : EnemyBehavior
{
    private Transform formationCenter;
    public Transform target;
    
    public Transform bulletPoint;
    public GameObject bulletPrefab;
    public float shootCooldown = 1f;
    private float timer;
    private float randomstopdistaceTarget;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        randomstopdistaceTarget = Random.Range(5, 12);
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        target = player.transform;
        
        SetState(EnemyState.Walk);
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
            {
                case EnemyState.Walk:
                    HandleWalkState();
                    break;
                case EnemyState.Fire:
                    HandleFireState();
                    break;
                case EnemyState.Run:
                    HandleRunState();
                    break;
                case EnemyState.TakeCover:
                    HandleTakeCoverState();
                    break;
                case EnemyState.Formations:
                    HandleFormationsState();
                    break;
                default:

                    break;
            }
    }

    void HandleWalkState()
    {
        float distanceToTarget = Vector3.Distance(transform.position, target.position);
        if (distanceToTarget > randomstopdistaceTarget)
        {
            agent.isStopped = false;
            MoveToTarget(target);
            agent.stoppingDistance = randomstopdistaceTarget;
            HandleFireState();
        }
        else if (distanceToTarget <= randomstopdistaceTarget - 1)
        {
            Vector3 retreatDirection = transform.position - target.position;
            agent.Move(retreatDirection.normalized * agent.speed * Time.deltaTime);
            HandleFireState();
        }
        else
        {
            agent.isStopped = true;
            HandleFireState();
        }
    }

    void HandleFireState()
    {
    
        transform.LookAt(target.position);

        timer += Time.deltaTime;

        if (timer >= shootCooldown)
        {
            Shoot();
            timer = 0f;
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletPoint.position, bulletPoint.rotation);
        Destroy(bullet, 5f);
    }
    void HandleRunState()
    {
        
    }
    void HandleFormationsState()
    {
        if (formationCenter != null)
        {
            MoveToTarget(formationCenter);
        }
    }
    void HandleTakeCoverState()
    {
        Transform nearestCover = FindNearestCover("Cover");
        if (nearestCover != null)
        {
            MoveToTarget(nearestCover);
            agent.stoppingDistance = 0;
        }
        else
        {
            MoveToTarget(target);
            agent.stoppingDistance = 5;
        }
    }
    
    public void SetFormationCenter(Transform center)
    {
        formationCenter = center;
    }

    public void SetFomation()
    {
        SetState(EnemyState.Formations);
    }

}