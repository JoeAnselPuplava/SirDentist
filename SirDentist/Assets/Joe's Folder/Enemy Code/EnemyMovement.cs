using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public GameObject player;
    private Rigidbody2D rb;
    private bool shouldMove = true;
    private bool grounded = true;
    private Animator animator;

    private GameHandler gameHandler;
    public int damage = 10;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        if (GameObject.FindWithTag ("GameHandler") != null) {
            gameHandler = GameObject.FindWithTag ("GameHandler").GetComponent<GameHandler> ();
        }
    }

    // Update is called once per frame
    void Update()
    {
        shouldStab();

        Debug.Log("What: " + (shouldMove && grounded));
        if (shouldMove && grounded)
        {
            TowardsPlayer();
        }
        else
        {
            standIdle();
        }
            
    }



    private void shouldStab(){// calculates distance between players hit 
        //and determines if it should run the stab animation
        float dist_to_player= Vector3.Distance(player.transform.position,transform.position);//computes dist to player
        if(dist_to_player<3){
            animator.SetBool("hasHit",true);
        }
        else{
            animator.SetBool("hasHit", false);
        }


    }

    void TowardsPlayer()
    {
        if (Mathf.Round(player.transform.position.x*10f) - Mathf.Round(transform.position.x*10f) < 0)
        {
            StartCoroutine(moveLeft());
            animator.SetBool("left", true);
            animator.SetBool("right", false);
           
        }
        else if (Mathf.Round(player.transform.position.x*10f) - Mathf.Round(transform.position.x*10f) > 0)
        {
            StartCoroutine(moveRight());
            animator.SetBool("right", true);
            animator.SetBool("left", false);
        }

    }

    void standIdle()
    {

    }

    IEnumerator moveLeft()
    {
        yield return new WaitForSeconds(0.5f);


        if (rb.velocity == new Vector2(0, 0) || rb.velocity.x > 0)
        {
            Vector2 movement = new Vector2(-1 * moveSpeed, rb.velocity.y);
            rb.velocity = movement;
        }
        else if (rb.velocity.x > 5)
        {
            Vector2 movement = new Vector2(-1 * 0.5f * moveSpeed, 0);
            rb.velocity += movement;
        }
    }

    IEnumerator moveRight()
    {
        yield return new WaitForSeconds(0.5f);


        if (rb.velocity == new Vector2(0, 0) || rb.velocity.x < 0)
        {
            Vector2 movement = new Vector2(1 * moveSpeed, rb.velocity.y);
            rb.velocity = movement;
        }
        else if(rb.velocity.x < 5)
        {
            Vector2 movement = new Vector2(1 * 0.5f * moveSpeed, 0);
            rb.velocity += movement;
        }
        
    }

    IEnumerator stopMoving()
    {
        shouldMove = false;
        rb.velocity = new Vector2(0, 0);
        yield return new WaitForSeconds(2f);
        shouldMove = true;

    }

    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.tag == "Player")
        {
            //animator.SetBool("hasHit", true);
            Rigidbody2D prb = player.GetComponent<Rigidbody2D>();
            Vector2 force = new Vector2(0, 0);
            if (rb.velocity.x > 0)
            {
                force = new Vector2(200, 200);
            }
            else
            {
                force = new Vector2(-200, 200);
            }
            prb.AddForce(force);
            //Debug.Log(prb.velocity);
            StartCoroutine(stopMoving());
        }

        else if (other.gameObject.tag == "Flail")
        {
            StartCoroutine(stopMoving());            
            Rigidbody2D prb = other.gameObject.GetComponent<Rigidbody2D>();

            //Vector2 force = new Vector2(prb.velocity.x * 50, Mathf.Abs(prb.velocity.y) * 20);
            Vector2 force = new Vector2(0,0);
            if (Mathf.Round(other.gameObject.transform.position.x * 10f) - Mathf.Round(transform.position.x * 10f) < 0)
            {
                force = new Vector2(200, 200);

            }
            else if (Mathf.Round(other.gameObject.transform.position.x * 10f) - Mathf.Round(transform.position.x * 10f) > 0)
            {
                force = new Vector2(-200, 200);
            }


            rb.AddForce(force);
        }

        else if (other.gameObject.tag == ("Ground"))
        {
            grounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == ("Ground"))
        {
            grounded = false;
        }
    }
}
