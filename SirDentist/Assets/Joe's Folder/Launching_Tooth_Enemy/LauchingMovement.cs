using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauchingMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float dist_to = 7f;

    private GameObject player;
    private GameObject[] ground;
    private Rigidbody2D rb;
    private bool shouldMove = true;
    private bool grounded = true;
    private bool canLaunch = true;
    private Animator animator;
    private Collider2D enemyCollider;
    


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        enemyCollider = GetComponent<Collider2D>();
        player = GameObject.FindWithTag("Player");
        ground = GameObject.FindGameObjectsWithTag("Ground");
    }

    // Update is called once per frame
    void Update()
    {
        //shouldStab();
        float dist = Vector3.Distance(player.transform.position, transform.position);
        touchingGrass();
        
        if (shouldMove && grounded)
        {
            if (dist < dist_to && canLaunch)
            {
                StartCoroutine(launching());
                StartCoroutine(cooldown());
            }
            else
            {
                TowardsPlayer();
            }                        
        }
        else
        {
            standIdle();
        }

    }

    IEnumerator launching()
    {
        float oldSpeed = moveSpeed;
        moveSpeed = 0;
        StartCoroutine(stopMoving());

        //Wind up code

        yield return new WaitForSeconds(1.5f);
        moveSpeed = oldSpeed;
        Vector2 force = new Vector2(player.transform.position.x - transform.position.x,
                                    player.transform.position.y - transform.position.y);
        rb.AddForce(force*200);
    }

    IEnumerator cooldown()
    {
        canLaunch = false;
        yield return new WaitForSeconds(3f);
        canLaunch = true;
    }

    private void shouldStab()
    {
        // calculates distance between players hit 
        //and determines if it should run the stab animation
        float dist_to_player = Vector3.Distance(player.transform.position, transform.position);//computes dist to player
        if (dist_to_player < 3)
        {
            animator.SetBool("hasHit", true);
        }
        else
        {
            animator.SetBool("hasHit", false);
        }


    }

    void TowardsPlayer()
    {
        if (Mathf.Round(player.transform.position.x * 10f) - Mathf.Round(transform.position.x * 10f) < 0)
        {
            StartCoroutine(moveLeft());
            animator.SetBool("left", true);
            animator.SetBool("right", false);

        }
        else if (Mathf.Round(player.transform.position.x * 10f) - Mathf.Round(transform.position.x * 10f) > 0)
        {
            StartCoroutine(moveRight());
            animator.SetBool("right", true);
            animator.SetBool("left", false);
        }

    }

    void standIdle()
    {
        //StandIdle Animation
    }

    IEnumerator moveLeft()
    {
        yield return new WaitForSeconds(0.5f);


        if (rb.velocity == new Vector2(0, 0) || rb.velocity.x > 0)
        {
            Vector2 movement = new Vector2(-1 * moveSpeed, rb.velocity.y);
            rb.velocity = movement;
        }
        else if (rb.velocity.x > -5)
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
        else if (rb.velocity.x < 5)
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
        //Debug.Log("In contact with " + (other.gameObject.tag));
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(stopMoving());
        }
        else if (other.gameObject.tag == "Flail")
        {
            StartCoroutine(stopMoving());
        }


    }

    private void touchingGrass()
    {
        //if (other.gameObject.tag == ("Ground"))

        ground = GameObject.FindGameObjectsWithTag("Ground");
        Collider2D groundCollider;

        foreach (GameObject floors in ground)
        {

            groundCollider = floors.GetComponent<Collider2D>();

            if (groundCollider != null)
            {
                if (enemyCollider.IsTouching(groundCollider))
                {
                    //Debug.Log("Grounded true");
                    grounded = true;
                }
            }
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //Debug.Log("No longer in contact with " + (collision.gameObject.tag));
        if (collision.gameObject.tag == ("Ground"))
        {
            //Debug.Log("Grounded false");
            grounded = false;
        }
    }
}

