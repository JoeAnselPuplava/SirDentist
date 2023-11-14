using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DoorScript : MonoBehaviour
{
    private GameHandler gameHandler;
    private AudioSource openDoorSound;
    private Animator animator;
    private bool once = true;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioSource>().playOnAwake = false;
        openDoorSound = GetComponent<AudioSource>();

        if (GameObject.FindWithTag("GameHandler") != null)
        {
            gameHandler = GameObject.FindWithTag("GameHandler").GetComponent<GameHandler>();
        }
        animator = GetComponent<Animator>();
        animator.SetBool("hasKey", false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        gameHandler.setKeyTrue();
        if (other.gameObject.tag == "Player"&& gameHandler.keyStatus() && once)
        {
            
            once = false;
            openDoorSound.Play();
            animator.SetBool("hasKey", true);//starts opening animation
            WaitForDoor();//waits while opening animation plays
            StartCoroutine(switchScenes());
        }
    }

    IEnumerator switchScenes()
    {
        gameHandler.setKeyTrue();
        while (openDoorSound.isPlaying)
            yield return null;
        SceneManager.LoadScene("MainMenu");
        Destroy(gameObject);
    }
     IEnumerator WaitForDoor(){
        //openDoorSound.Play();
        animator.SetBool("hasKey", true);
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

     }
}