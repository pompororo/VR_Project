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
        float randomRotationY = Random.Range(-50f, 50f);

        transform.rotation = Quaternion.Euler(90f, randomRotationY, 0f);
        
    }
}