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
        blade.SetActive(true);
    }
    public void BladeOff()
    {
        blade.SetActive(false);
    }
}
