using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class CloneBehavior : EnemyBehavior
{
    private Transform formationCenter;
    public Transform target; 

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        target = player.transform;
        
        SetState(EnemyState.TakeCover);
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
        MoveToTarget(target);
        agent.stoppingDistance = 5;
    }
    void HandleFireState()
    {
        
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