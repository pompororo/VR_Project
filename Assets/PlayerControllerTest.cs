using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class PlayerControllerTest : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Camera mainCamera;
    public GameObject skillobject;

    public bool isSkillActive ;
    public GameObject EnemytargetTrasforms;
    public List<GameObject> Enemytargets;

    public float duration = 5f; // Set the duration in seconds
    private float timer;
    void Start()
    {
        isSkillActive  = false;
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

        
        //forcelighting
        if (mainCamera != null && OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
        {
            Vector3 screenPoint = new Vector3(Screen.width * OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger), Screen.height / 2, 0);
          Ray ray = mainCamera.ScreenPointToRay(screenPoint);

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
        if (Enemytargets.Count > 0)
        {
            if (!isSkillActive)
            {
                ActivateSkill();
            }
            else
            {
                timer += Time.deltaTime;
                Debug.LogWarning(timer);
                if (timer >= duration)
                {
                    DeactivateSkill();
                }
            }

            if (Enemytargets.Count > 0)
            {
                EnemytargetTrasforms.transform.position = Enemytargets[0].transform.position;
            }
            else
            {
                
            }
           
        }
        else
        {
            DeactivateSkill();
        }


      
    }
    private void ActivateSkill()
    {
        skillobject.SetActive(true);
        isSkillActive = true;

    }

    private void DeactivateSkill()
    {
        skillobject.SetActive(false);
        isSkillActive = false;
        Enemytargets.Clear();
        timer = 0f;
    }
    
    
}