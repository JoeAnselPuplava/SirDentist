using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpFlail : MonoBehaviour
{
    //public GameObject objectToSetTrue; // Reference to the GameObject you want to set to true
    public GameObject flailPrefab;
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
            //if (objectToSetTrue != null)
            if (flailPrefab != null)
            {
                //objectToSetTrue.transform.position = new Vector3(player.transform.position.x + 2.4f,
                //                                                 player.transform.position.y+ 3.12f,0);
                Vector3 putFlail = new Vector3(player.transform.position.x + 2.4f,
                                                                 player.transform.position.y + 3.12f, 0);
                Instantiate(flailPrefab, putFlail, Quaternion.identity);
                // Change the boolean value for the specified object
                // Here, I'm setting the GameObject active, but modify this line as needed
                //objectToSetTrue.SetActive(true);
                flailPrefab.GetComponent<chainLinker>().start2();
                gameObject.SetActive(false);
            }
            else
            {
                Debug.LogWarning("No object specified to set true.");
            }
        }
    }
}
