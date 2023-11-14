using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class HealPowerUp : MonoBehaviour
{
    private GameHandler gameHandler;
    public int heal = 10;
    private AudioSource healSound;
    private bool once = true;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioSource>().playOnAwake = false;
        healSound = GetComponent<AudioSource>();

        if (GameObject.FindWithTag("GameHandler") != null)
        {
            gameHandler = GameObject.FindWithTag("GameHandler").GetComponent<GameHandler>();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("On trigger works!");
        if (other.gameObject.tag == "Player" && gameHandler.returnPlayerHealth() < 100 && once)
        {
            once = false;
            StartCoroutine(playSound());
        }
    }

    IEnumerator playSound()
    {
        healSound.Play();
        gameObject.GetComponent<Renderer>().enabled = false;
        gameHandler.setKeyTrue();
        while (healSound.isPlaying)
            yield return null;
        Destroy(gameObject);
    }
}