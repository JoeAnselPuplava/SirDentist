using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{

    private GameHandler gameHandler;
    public int damage = 10;
    public int health = 10;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.tag == "Player")
        {
            animator.SetBool("hasHit", true);
        }

        else if (other.gameObject.tag == "Flail")
        {

        }

        else if (other.gameObject.tag == "Sword")
        {

        }
    }

    void flailDamage(int damage)
    {
        health -= damage;
        checkHealth();
    }

    void checkHealth()
    {
        if (health <= 0)
        {
            //Play death animation
            Destroy(gameObject);
        }
    }
}
