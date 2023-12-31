using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class PlayerControllerTest : MonoBehaviour
{

    private Camera mainCamera;
    public GameObject skillobject;

    public bool isSkillActive ;
    public GameObject EnemytargetTrasforms;
    public List<GameObject> Enemytargets;

    public float duration = 5f; // Set the duration in seconds
    private float timer;
    public float rayLength = 10f;
    void Start()
    {
        isSkillActive  = false;
        mainCamera = Camera.main;
        Enemytargets = new List<GameObject>();
    
    }

    void Update()
    {

        // Remove null references from the list
        Enemytargets.RemoveAll(item => item == null);

        
        //forcelighting
        if (OVRInput.GetDown(OVRInput.RawButton.Y) || OVRInput.GetDown(OVRInput.RawButton.B)|| Input.GetKeyDown(KeyCode.A))
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit[] hits = Physics.RaycastAll(ray, rayLength);

            // Check if the ray hits something
            foreach (var hit in hits)
            {
                if (hit.collider.CompareTag("Ai"))
                {
                    Enemytargets.Clear();
                    Enemytargets.Add(hit.collider.gameObject);
                }
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
                Vector3 newPosition = Enemytargets[0].transform.position; // Get the current position

                // Update the Y component to be +2
                newPosition.y = 2;

                // Assign the new position to the target transform
                EnemytargetTrasforms.transform.position = newPosition;
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
    void OnDrawGizmos()
    {
        // Draw the ray in the Scene view for visualization
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * rayLength);
    }
}