using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class door_controller : MonoBehaviour
{
    public string newSceneName; // The name of the scene you want to load
    private Animator animator;
    public float playerDist = 2.5f; // dist player must be within to open door
    private bool inRange = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("opening", false);
    }

    // Update is called once per frame
    void Update()
    {
        isPlayerInRange();

        if (Input.GetKeyDown(KeyCode.O) && inRange)
        {
            animator.SetBool("opening", true);
            print("opening door");
        }
    }

    bool isPlayerInRange() // checks to see if player is close enough to the door
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null && Vector3.Distance(transform.position, player.transform.position) <= playerDist)
        {
            inRange = true;
            print("inRange");
            //animator.SetBool("locked", false);
        }
        else
        {
            inRange = false;
        }

        //print(Vector3.Distance(transform.position, player.transform.position));

        return inRange;
    }
}
