using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneFormationManager : MonoBehaviour
{
    public Transform formationCenter; // Set this in the inspector to the desired formation center

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            SetFormationState();
        }
    }

    void SetFormationState()
    {
        // Set all AI units to the formations state
        CloneBehavior[] cloneBehaviors = FindObjectsOfType<CloneBehavior>();

        foreach (CloneBehavior cloneBehavior in cloneBehaviors)
        {
            cloneBehavior.SetFomation();
            cloneBehavior.SetFormationCenter(formationCenter);
        }
    }
}