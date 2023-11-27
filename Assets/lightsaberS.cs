using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsaberController : MonoBehaviour
{
    public GameObject blade;
    private bool isBladeOn = false;

    void Update()
    {
        
    }

    public void ToggleBlade()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger)||OVRInput.Get(OVRInput.Button.SecondaryHandTrigger))
        {
            // Toggle the state
            isBladeOn = !isBladeOn;

            if (isBladeOn)
            {
                Debug.Log("Open Lightsaber");
            }
            else
            {
                Debug.Log("Close Lightsaber");
            }
            
            blade.SetActive(isBladeOn);
        }
    }
}

