using UnityEngine;

public class ElectricArc : MonoBehaviour
{
    public Transform startPoint; // Set the start position in the Inspector
    public Transform endPoint;   // Set the end position in the Inspector
    public float speed = 1f;     // Set the movement speed in the Inspector

    private ParticleSystem particleSystem;

    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        MoveParticleSystem();
    }

    void MoveParticleSystem()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.Lerp(startPoint.position, endPoint.position, Mathf.PingPong(Time.time * step, 1));
    }
}