using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restart : MonoBehaviour
{
    public GameObject startgame;
    private void OnTriggerEnter(Collider other)
    {
        // Check if the entered collider is the Lightsaber
        if (other.CompareTag("BladeSaber"))
        {
            FindObjectOfType<GameManager>().gameObject.SetActive(false);
            startgame.GetComponent<GameManager>().Restart();
            startgame.SetActive(true);
            FindObjectOfType<Player>().MoveToRestart();
        }
    }
}
