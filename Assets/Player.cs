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
    
    public Transform playerRestart;
    public Transform playerDeath;
    public Transform playerWin;
    
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
            this.gameObject.transform.position = playerDeath.position;
            currentHealth = maxHealth;
            isDying = false;
        }
    }

    public void MoveToWinRoom()
    {
        this.gameObject.transform.position = playerWin.position;
    }

    public void MoveToRestart()
    {
        this.gameObject.transform.position = playerRestart.position;
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
