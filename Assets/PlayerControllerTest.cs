using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerControllerTest : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Camera mainCamera;
    

    void Start()
    {
        mainCamera = Camera.main;
        // Get the NavMeshAgent component attached to the player GameObject
        navMeshAgent = GetComponent<NavMeshAgent>();

        // Check if the NavMeshAgent component is not found
        if (navMeshAgent == null)
        {
            Debug.LogError("NavMeshAgent component not found on the player GameObject.");
        }
    }

    void Update()
    {
        if (mainCamera != null && Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                navMeshAgent.SetDestination(hit.point);
            }
        }
    }

}
