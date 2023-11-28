using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    public CapsuleCollider MainCollider;
    public GameObject cloonRig;
    public Animator cloonAni;
    
    void Start()
    {
        GetRagDollBits();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Lightsaber")
        {
            RagdollModeOn();
        }
    }

    private Collider[] ragDollColliders;
    private Rigidbody[] limbsRigidbodies;
    
    void GetRagDollBits()
    {
        ragDollColliders = cloonRig.GetComponentsInChildren<Collider>();
        limbsRigidbodies = cloonRig.GetComponentsInChildren<Rigidbody>();
    }

    void RagdollModeOn()
    {
        cloonAni.enabled = false;
        
        foreach (Collider col in ragDollColliders)
        {
            col.enabled = true;
        }
        
        foreach (Rigidbody rigid in limbsRigidbodies)
        {
            rigid.isKinematic = false;
        }
        
        MainCollider.enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
    }

    void RagdollModeOff()
    {
        foreach (Collider col in ragDollColliders)
        {
            col.enabled = false;
        }
        
        foreach (Rigidbody rigid in limbsRigidbodies)
        {
            rigid.isKinematic = true;
        }

        cloonAni.enabled = true;
        MainCollider.enabled = true;
        GetComponent<Rigidbody>().isKinematic = false;
    }
}
