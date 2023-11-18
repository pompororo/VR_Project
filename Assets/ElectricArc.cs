using UnityEngine;

public class ElectricArc : MonoBehaviour
{
    public GameObject startObject;
    public GameObject endObject;

    private void Start()
    {
        if (startObject != null)
        {
            // Set the initial position of the electric arc to the position of the start object
            transform.position = startObject.transform.position;
        }

        // Set the end position of the electric arc
        if (endObject != null)
        {
            ParticleSystem.ShapeModule shapeModule = GetComponent<ParticleSystem>().shape;
            shapeModule.position = endObject.transform.position;
        }
    }
}