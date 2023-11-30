using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsaberController : MonoBehaviour
{
    public GameObject blade;
    private bool isBladeOn = false;
    public Animator animation;
    void Update()
    {

    }

    public AudioClip openLight;
    public AudioClip closeLight;

    public void ToggleBlade()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger)||OVRInput.Get(OVRInput.Button.SecondaryHandTrigger))
        {
            // Toggle the state
            isBladeOn = !isBladeOn;

            if (isBladeOn)
            {
                Debug.Log("Open Lightsaber");
                this.GetComponent<AudioSource>().clip = openLight;
                this.GetComponent<AudioSource>().Play();
            }
            else
            {
                Debug.Log("Close Lightsaber");
                this.GetComponent<AudioSource>().clip = closeLight;
                this.GetComponent<AudioSource>().Play();
            }
            animation.SetBool("isopen",isBladeOn);
            //blade.SetActive(isBladeOn);
        }
    }
    
  
}

