using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth;

    public bool isDying;
    
    [SerializeField] private float pullForce = 100f; // Adjust the force magnitude
    [SerializeField] private float pullRange = 100f; // Adjust the range of the force pull
    private bool isPulling = false;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void StartForcePull()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log("Hit object: " + hit.collider.gameObject.name);
            
            Vector3 direction = (transform.position - hit.point).normalized;
            if (hit.collider.CompareTag("HandleLightsaber"))
            {
                Debug.Log("Hit lightsaber");
                isPulling = true;
                Rigidbody pulledObject = hit.collider.GetComponent<Rigidbody>();
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
        if ((OVRInput.GetDown(OVRInput.Button.One) || Input.GetKeyDown(KeyCode.Tab)) && GameManager.currentFieldState != AffectFieldSkill.SlowTime)
        {
            GameManager.ActiveSlowTime();
        }
        else if((OVRInput.GetDown(OVRInput.Button.One) || Input.GetKeyDown(KeyCode.Tab)) && GameManager.currentFieldState == AffectFieldSkill.SlowTime)
        {
            GameManager.DefaultFieldState();
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            StartForcePull();
        }
    }
}
