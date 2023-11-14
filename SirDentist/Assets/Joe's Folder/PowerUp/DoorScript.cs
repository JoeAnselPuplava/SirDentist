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
        print("collided");
        if (other.gameObject.tag == "Player" && gameHandler.keyStatus() && once)
        {
            OpenDoorAndWait();
            once = false;
            StartCoroutine(playSound());
        }
    }

    IEnumerator playSound()
    {
        openDoorSound.Play();
        gameHandler.setKeyTrue();
        while (openDoorSound.isPlaying)
            yield return null;
        SceneManager.LoadScene("MainMenu");
        Destroy(gameObject);
    }
     IEnumerator OpenDoorAndWait(){
        animator.SetBool("hasKey", true);
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

     }
}