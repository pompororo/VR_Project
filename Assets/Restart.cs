using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restart : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Check if the entered collider is the Lightsaber
        if (other.CompareTag("BladeSaber"))
        {
            FindObjectOfType<GameManager>().gameObject.SetActive(false);
            FindObjectOfType<StartGameObject>().gameObject.SetActive(true);
            FindObjectOfType<Player>().MoveToRestart();
        }
    }
}
