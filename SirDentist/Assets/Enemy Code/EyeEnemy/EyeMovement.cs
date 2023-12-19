using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeMovement : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float jumpForce = 10f;
    private GameObject player;
    private GameObject[] ground;


    private Rigidbody2D rb;
    private bool shouldMove = true;
    private Animator animator;
    private Collider2D enemyCollider;

    public Transform feet;
    public LayerMask groundLayer;

    float stuntime = 1.5f;
    float pastms;
    public GameObject stunanimation;


    // Start is called before the first frame update
    void Start()
    {
        pastms = moveSpeed;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        enemyCollider = GetComponent<Collider2D>();
        player = GameObject.FindWithTag("Player");
        ground = GameObject.FindGameObjectsWithTag("Ground");
        //Hides stun animation (double checking it)
        stunanimation.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        shouldStab();

        if (shouldMove && IsGrounded())
        {
            TowardsPlayer();
        }
    }

    private void shouldStab()
    {// calculates distance between players hit 
        //and determines if it should run the stab animation
        float dist_to_player = Vector3.Distance(player.transform.position, transform.position);//computes dist to player
        if (dist_to_player < 10)
        {
            animator.SetBool("shouldHit", true);
        }
        else
        {
            animator.SetBool("shouldHit", false);
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
        yield return new WaitForSeconds(1f);
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

    public bool IsGrounded()
    {
        Collider2D groundCheck = Physics2D.OverlapCircle(feet.position, .2f, groundLayer);
        if (groundCheck != null)
        {
            return true;
        }
        return false;
    }

    public void stuned(){
        Debug.Log("Stunned");
        StartCoroutine(stun());
    }
    
    private IEnumerator stun(){
        moveSpeed = 0;
        rb.velocity = new Vector2(0, 0);
        stunanimation.SetActive(true);
        yield return new WaitForSeconds(stuntime);
        stunanimation.SetActive(false);
        Debug.Log("unfreeze");
        moveSpeed = pastms;
    }
    
}
