using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderHandler : MonoBehaviour
{
    public string targetTag = "YourTargetTag";
    public Transform parentObject;
    private Vector3 newPosition = new Vector3(0, -0.0197f, 0);

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            // Set the other game object as a child of the specified parent object
            other.transform.SetParent(parentObject);

            // Set the local position of the child object along the x, y, and z axes
            other.transform.localPosition = newPosition;
        }
    }
}
