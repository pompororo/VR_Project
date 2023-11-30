using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth;

    public GameObject EndText;

    private bool isDying;
    
    public float pullForce = 5f; // Adjust the force magnitude
    
    private bool isPulling = false;

    public Transform playerAnchor;
    
    // Start is called before the first frame update

    private void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Tab) || OVRInput.GetDown(OVRInput.Button.One)) && GameManager.currentFieldState == AffectFieldSkill.Default)
        {
            GameManager.currentFieldState = AffectFieldSkill.SlowTime;
            GameManager.hasUpdated = false;
        }
        else if((Input.GetKeyDown(KeyCode.Tab) || OVRInput.GetDown(OVRInput.Button.One)) && GameManager.currentFieldState == AffectFieldSkill.SlowTime)
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
            EndText.SetActive(true);
            //Destroy(gameObject);
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
