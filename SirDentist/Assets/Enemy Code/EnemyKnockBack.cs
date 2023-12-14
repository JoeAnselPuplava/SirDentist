using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKnockBack : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool playerHit = false;
    private bool enemyHit = false;
    private Rigidbody2D prb;
    private bool isFlail;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        prb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (playerHit)
        {
            knockPlayer();
        }
        else if (enemyHit)
        {
            knockEnemy();
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerHit = true;
        }

        else if (other.gameObject.tag == "Flail")
        {
            enemyHit = true;
            isFlail = true;
        }
        else if (other.gameObject.tag == "Sword")
        {
            enemyHit = true;
            isFlail = true;
        }
    }
    private void knockPlayer()
    {
        
        if (rb.velocity.x > 0)
        {
            prb.velocity = Vector2.right * 10f;
        }

        else
        {
            prb.velocity = Vector2.left * 10f;
        }
        playerHit = false;
    }
    private void knockEnemy()
    {
        
        if (isFlail)
        {
            prb = GameObject.FindGameObjectWithTag("Flail").GetComponent<Rigidbody2D>();
        }
        else
        {
            prb = GameObject.FindGameObjectWithTag("Sword").GetComponent<Rigidbody2D>();
        }
        if (Mathf.Round(prb.gameObject.transform.position.x * 10f) - Mathf.Round(transform.position.x * 10f) < 0)
        {
            //force = new Vector2(500, 500);
            rb.velocity = Vector2.right * 10f;
        }
        else if (Mathf.Round(prb.gameObject.transform.position.x * 10f) - Mathf.Round(transform.position.x * 10f) > 0)
        {
            //force = new Vector2(-500, 500);
            rb.velocity = Vector2.left * 10f;
        }
    }
}
