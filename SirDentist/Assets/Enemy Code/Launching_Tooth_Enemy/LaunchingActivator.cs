using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchingActivator : MonoBehaviour
{
    public GameObject[] enemies;
    private bool once = true;
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<LauchingMovement>().moveSpeed = 0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            once = false;
            foreach (GameObject enemy in enemies)
            {
                enemy.GetComponent<LauchingMovement>().moveSpeed = 5f;
            }
        }
    }

}
