using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class ECloneBehavior : EnemyBehavior
{
    private Transform formationCenter;
    public Transform target;
    
    public Transform bulletPoint;
    public GameObject bulletPrefab;
    private float shootCooldown;
    private float timer;
    private float randomstopdistaceTarget;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        randomstopdistaceTarget = Random.Range(8, 15);
        shootCooldown = Random.Range(0.1f, 1);
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
            HandleTakeCoverState();
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
        StartCoroutine(ActivateAndDeactivate());
        GameObject bullet = Instantiate(bulletPrefab, bulletPoint.position, bulletPoint.rotation);
        shootCooldown = Random.Range(0.1f, 0.5f);
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
            HandleFireState();
        }
        else
        {
            MoveToTarget(target);
            agent.stoppingDistance = randomstopdistaceTarget;
            HandleFireState();
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
    public GameObject FlashMuzzle;
    IEnumerator ActivateAndDeactivate()
    {
        // Activate the GameObject
        FlashMuzzle.SetActive(true);

        // Wait for 0.1 seconds
        yield return new WaitForSeconds(0.1f);

        // Deactivate the GameObject
        FlashMuzzle.SetActive(false);
    }

}