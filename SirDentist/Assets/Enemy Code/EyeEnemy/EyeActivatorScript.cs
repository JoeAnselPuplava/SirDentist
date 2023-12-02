using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeActivatorScript : MonoBehaviour
{
    public GameObject[] enemies;
    public float speed = 10f;
    private bool once = true;
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<EyeMovement>().moveSpeed = 0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("came into contact with " + other.tag);
        if (other.gameObject.tag == "Player")
        {
            once = false;
            foreach (GameObject enemy in enemies)
            {
                enemy.GetComponent<EnemyMovement>().moveSpeed = speed;
            }
        }
    }

}
