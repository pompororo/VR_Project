using UnityEngine;

public class StartGameObject : MonoBehaviour
{
    public GameObject gameManager;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the entered collider is the Lightsaber
        if (other.CompareTag("BladeSaber"))
        {
            Debug.Log("Start");
            gameManager.SetActive(true);
            Destroy(gameObject);
        }
    }
}
