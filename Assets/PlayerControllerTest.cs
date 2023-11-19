using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerControllerTest : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Camera mainCamera;
    public GameObject skillobject;

    public GameObject EnemytargetTrasforms;
    public List<GameObject> Enemytargets;

    void Start()
    {
        mainCamera = Camera.main;
        Enemytargets = new List<GameObject>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        skillobject.SetActive(false);
        if (navMeshAgent == null)
        {
            Debug.LogError("NavMeshAgent component not found on the player GameObject.");
        }
    }

    void Update()
    {
        // Remove null references from the list
        Enemytargets.RemoveAll(item => item == null);

        if (Enemytargets.Count > 0)
        {
            useForceL();
            EnemytargetTrasforms.transform.position = Enemytargets[0].transform.position;
        }
        else
        {
            skillobject.SetActive(false);
        }

        if (mainCamera != null && Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Ai"))
                {
                    // Check if the object is not null before adding it to the list
                    if (hit.collider.gameObject != null)
                    {
                        Enemytargets.Clear();
                        Enemytargets.Add(hit.collider.gameObject);
                    }
                }
                navMeshAgent.SetDestination(hit.point);
            }
        }
    }

    public void useForceL()
    {
        skillobject.SetActive(true);
    }
    
}