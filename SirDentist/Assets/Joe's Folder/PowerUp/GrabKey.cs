using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class grabKey : MonoBehaviour
{
    private GameHandler gameHandler;
    public AudioSource grabSound;
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
        if (other.gameObject.tag == "Player" && once)
        {
            once = false;
            StartCoroutine(playSound());
        }
    }

    IEnumerator playSound()
    {
        grabSound.Play();
        gameHandler.setKeyTrue();
        while (grabSound.isPlaying)
            yield return null;
        Destroy(gameObject);
    }
}