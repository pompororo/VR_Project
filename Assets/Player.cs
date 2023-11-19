using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth;

    public bool isDying;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((OVRInput.GetDown(OVRInput.Button.One) || Input.GetKey(KeyCode.Tab)) && GameManager.currentFieldState != AffectFieldSkill.SlowTime)
        {
            GameManager.ActiveSlowTime();
        }
        else
        {
            GameManager.DefaultFieldState();
        }
    }
}
