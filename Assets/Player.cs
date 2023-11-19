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

    private void StartForcePull()
    {
        /*
         *
         * Raycast ต้องยิงมาจากมือ
         * Force จะต้องดึง(กด)แค่ครั้งเดียวดึงมาที่มือของเรา (จำของแล้วดึงเรื่อยๆ? who know อย่าหาทำใน start ครั้งเดียว)
         * Hightlight lightsaber ด้วย
         * 
         */
        
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
        if ((OVRInput.GetDown(OVRInput.Button.One) || Input.GetKeyDown(KeyCode.Tab)) && GameManager.currentFieldState != AffectFieldSkill.SlowTime)
        {
            GameManager.ActiveSlowTime();
        }
        else if((OVRInput.GetDown(OVRInput.Button.One) || Input.GetKeyDown(KeyCode.Tab)) && GameManager.currentFieldState == AffectFieldSkill.SlowTime)
        {
            GameManager.DefaultFieldState();
        }
        
        //อย่าหาทำพอเรียกซ้ำแล้วกลายเป็นช้าเลย
        if (Input.GetMouseButtonDown(0))
        {
            StartForcePull();
        }
    }
}
