using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightsaberS : MonoBehaviour
{
    public GameObject blade;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void BladeOn()
    {
        if (OVRInput.Get(OVRInput.Button.PrimaryHandTrigger))
        {
            Debug.Log("Open Light Saber");
            blade.SetActive(true);
        }

    }
        
    public void BladeOff()
    {
        if (OVRInput.Get(OVRInput.Button.PrimaryHandTrigger))
        {
            Debug.Log("Close Light Saber");
            blade.SetActive(false);
        }
    }
}
