using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpFlail : MonoBehaviour
{
    public GameObject objectToSetTrue; // Reference to the GameObject you want to set to true
    private GameObject player;
    private Transform placeToBe;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the triggering object has a tag "Player"
        if (other.gameObject.CompareTag("Player"))
        {
            // Set the boolean value to true for the specified object
            if (objectToSetTrue != null)
            {
                objectToSetTrue.transform.position = new Vector3(player.transform.position.x + 2.4f,
                                                                 player.transform.position.y+ 3.12f,0);
                // Change the boolean value for the specified object
                // Here, I'm setting the GameObject active, but modify this line as needed
                objectToSetTrue.SetActive(true);
                gameObject.SetActive(false);
            }
            else
            {
                Debug.LogWarning("No object specified to set true.");
            }
        }
    }
}
