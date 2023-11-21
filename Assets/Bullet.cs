using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;

    void Update()
    {
        MoveBullet();
    }

    void MoveBullet()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
        if (other.CompareTag("Ai"))
        {
            Destroy(gameObject);
        }
        else if (other.CompareTag("BladeSaber"))
        {
            // Reflect the bullet in the opposite direction
            ReflectBullet();
        }
    }

    void ReflectBullet()
    {
        // Reverse the bullet's direction
        speed = -speed;
      
        
    }
}