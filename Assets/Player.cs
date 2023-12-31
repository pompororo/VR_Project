using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    private OVRPlayerController playercon;
    public float currentHealth;
    public float maxHealth;

  //  public GameObject EndText;

    private bool isDying;
    
    public float pullForce = 5f; // Adjust the force magnitude
    
    private bool isPulling = false;

    public Transform playerAnchor;
    
    public Transform playerRestart;
    public Transform playerDeath;
    public Transform playerWin;
    
    public TextMeshProUGUI healthText;
    
    // Start is called before the first frame update

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthText();
        playercon = gameObject.GetComponent<OVRPlayerController>();
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
          MoveToDeathRoom();
            currentHealth = maxHealth;
            isDying = false;
        }
    }

    public void MoveToWinRoom()
    {
        StartCoroutine(winWithDelay());
    }

    public void MoveToRestart()
    {
        StartCoroutine(RestartWithDelay());
    }
    
    
    public void MoveToDeathRoom()
    {
        StartCoroutine(DeathWithDelay());
    }

    IEnumerator DeathWithDelay()
    {
        playercon.enabled = false;
        this.gameObject.transform.position = playerDeath.position;
        
        yield return new WaitForSeconds(0.3f); // Wait for 1 second
        
        playercon.enabled = true;
    }
    IEnumerator RestartWithDelay()
    {
        playercon.enabled = false;
        this.gameObject.transform.position = playerRestart.position;
        
        yield return new WaitForSeconds(0.3f); // Wait for 1 second
        
        playercon.enabled = true;
    }
    IEnumerator winWithDelay()
    {
        playercon.enabled = false;
        this.gameObject.transform.position = playerWin.position;
        
        yield return new WaitForSeconds(0.3f); // Wait for 1 second
        
        playercon.enabled = true;
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
    
            // Ensure health doesn't go below 0
            currentHealth = Mathf.Max(0, currentHealth);
    
            // Update health text after taking damage
            UpdateHealthText();
        }
    
        private void UpdateHealthText()
        {
            healthText.text = "HP: " + currentHealth.ToString();
        }
}


