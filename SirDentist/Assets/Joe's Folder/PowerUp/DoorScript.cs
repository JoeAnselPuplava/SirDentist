using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DoorScript : MonoBehaviour
{
    private GameHandler gameHandler;
    public AudioSource openDoorSound;
    private bool once = true;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioSource>().playOnAwake = false;

        if (GameObject.FindWithTag("GameHandler") != null)
        {
            gameHandler = GameObject.FindWithTag("GameHandler").GetComponent<GameHandler>();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && gameHandler.keyStatus() && once)
        {
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
}