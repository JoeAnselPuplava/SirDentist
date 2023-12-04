using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tooth_pit : MonoBehaviour
{
    private GameHandler gameHandler;
    public int attackpower = 10;
    public float health = 50f;

    private GameObject player;

  
    private bool Eimmune = false;

    // Start is called before the first frame update
    void Start()
    {

        player = GameObject.FindWithTag("Player");
        if (GameObject.FindWithTag("GameHandler") != null)
        {
            gameHandler = GameObject.FindWithTag("GameHandler").GetComponent<GameHandler>();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.tag == "Player")
        {

            StartCoroutine(waitImmune());


        }

        else if (other.gameObject.tag == "Flail" && !Eimmune)
        {
           // animator.SetTrigger("beenHit");

        }

        else if (other.gameObject.tag == "Sword")
        {
            //animator.SetTrigger("beenHit");
        }

    }


    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(waitImmune());
        }

        else if (other.gameObject.tag == "Flail" && !Eimmune)
        {
           // animator.SetTrigger("beenHit");


        }

        else if (other.gameObject.tag == "Sword")
        {
            //animator.SetTrigger("beenHit");
        }

    }

    IEnumerator waitImmune()
    {
        gameHandler.playerGetHit(attackpower);
        yield return new WaitForSeconds(0.3f);
    }

    public void flailDamage(float damage)
    {

        if (!Eimmune)
        {

            health -= damage;
            checkHealth();
        }
    }

    public void meleeDamage(float damage)
    {
       if (!Eimmune)
        {
            health -= damage;
            checkHealth();
        }
    }

    void checkHealth()
    {
        if (health <= 0)
        {
            Eimmune = true;
        }
    }
    public void killMe()
    {//destroys the enemy game object
        Destroy(gameObject);
        StartCoroutine(Immunity());
    }

    private IEnumerator Immunity()
    {
        Eimmune = true;//tooth pit is always immune
        yield return new WaitForSeconds(0.2f);
        // Eimmune = false;
    }

}
