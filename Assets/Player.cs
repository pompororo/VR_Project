using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth;

    private bool isDying;
    
    public float pullForce = 5f; // Adjust the force magnitude
    
    private bool isPulling = false;

    public Transform playerAnchor;
    
    // Start is called before the first frame update

    /*private void StartForcePull()
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
    }*/

    private void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Tab) || OVRInput.GetDown(OVRInput.Button.One) && GameManager.currentFieldState == AffectFieldSkill.Default))
        {
            GameManager.currentFieldState = AffectFieldSkill.SlowTime;
            GameManager.hasUpdated = false;
        }
        else if((Input.GetKeyDown(KeyCode.Tab) || OVRInput.GetDown(OVRInput.Button.One) && GameManager.currentFieldState == AffectFieldSkill.SlowTime))
        {
            GameManager.currentFieldState = AffectFieldSkill.Default;
            GameManager.hasUpdated = false;
        }
        
        if (!isDying)
        {
            if (currentHealth <= 0)
            {
                isDying = true;
            }
        }

        if (isDying)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
            {
                InputDamage(20);
                Destroy(other.gameObject);
            }
        
    }


    private void InputDamage(int damage)
    {
        currentHealth -= damage;
    }
}
