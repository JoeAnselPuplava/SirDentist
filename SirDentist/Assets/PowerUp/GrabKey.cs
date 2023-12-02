using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class grabKey : MonoBehaviour
{
    private GameHandler gameHandler;
    private AudioSource grabSound;
    private bool once = true;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioSource>().playOnAwake = false;
        grabSound = GetComponent<AudioSource>();

        if (GameObject.FindWithTag("GameHandler") != null)
        {
            gameHandler = GameObject.FindWithTag("GameHandler").GetComponent<GameHandler>();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && once)
        {
            once = false;
            StartCoroutine(playSound());
        }
    }

    IEnumerator playSound()
    {
        grabSound.Play();
        gameObject.GetComponent<Renderer>().enabled = false;
        gameHandler.setKeyTrue();
        while (grabSound.isPlaying)
            yield return null;
        Destroy(gameObject);
    }
}