using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth;

    public bool isDying;
    
    public float pullForce = 5f; // Adjust the force magnitude
    
    private bool isPulling = false;

    public Transform playerAnchor;
    
    // Start is called before the first frame update

    private void StartForcePull()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit))
        {
            //Debug.Log("Hit object: " + hit.collider.gameObject.name);
            
            if (hit.collider.CompareTag("HandleLightsaber"))
            {
                Transform parenthitTransform = hit.collider.transform.parent;
                Debug.Log("Hit lightsaber start pulling");
                isPulling = true;
                Rigidbody pulledObject = parenthitTransform.GetComponent<Rigidbody>();
                
                Vector3 direction = transform.position - parenthitTransform.position;
                if (pulledObject != null)
                {
                    pulledObject.velocity = direction * pullForce * Time.deltaTime;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
