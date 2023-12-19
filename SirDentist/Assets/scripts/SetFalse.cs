using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetFalse : MonoBehaviour
{
    public List<GameObject> enemiesToSpawn; // List of prefabs to instantiate
    private bool once = false;

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player") && !once) // Check if the collider belongs to the player
        {
            InstantiatePrefabs();
        }
    }

    private void InstantiatePrefabs()
    {
        //Debug.Log("Going to Spawn");
        if (enemiesToSpawn.Count > 0)
        {
            foreach (GameObject prefab in enemiesToSpawn)
            {
                prefab.SetActive(false);
                //Debug.Log("Prefab: " + prefab.activeSelf);

            }
        }
        else
        {
            Debug.LogWarning("Prefab list is empty or spawn point not assigned.");
        }
    }
}
