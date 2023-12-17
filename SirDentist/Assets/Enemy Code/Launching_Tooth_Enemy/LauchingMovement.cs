using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauchingMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float dist_to = 7f;

    private int crackState=1;

    private GameObject player;
    private GameObject[] ground;
    private Rigidbody2D rb;
    private bool shouldMove = true;
    private bool canLaunch = true;
    private Animator animator;
    private Collider2D enemyCollider;
    public AudioClip chargeUpAudioClip;
    private AudioSource AudSource;

    public Transform feet;
    public LayerMask groundLayer;

    float stuntime = 1.5f;
    float pastms;
    public GameObject stunanimation;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        enemyCollider = GetComponent<Collider2D>();
        player = GameObject.FindWithTag("Player");
        ground = GameObject.FindGameObjectsWithTag("Ground");
        AudSource = GetComponent<AudioSource>();
        //Hide stun animation
        stunanimation.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //shouldStab();
        float dist = Vector3.Distance(player.transform.position, transform.position);
        
        if (shouldMove && IsGrounded())
        {
            
            if (dist < dist_to && canLaunch)
            {
                StartCoroutine(launching());      
            }
            else
            {
                TowardsPlayer();
            }                        
        }

    }
    private void windUp(){
        
        GetComponent<AudioSource>().clip = chargeUpAudioClip;
        animator.SetBool("charging",true);
        GetComponent<AudioSource>().Play();

    }

    IEnumerator launching()
    {
        float oldSpeed = moveSpeed;
        moveSpeed = 0;

        StartCoroutine(stopMoving());
        windUp();
        yield return new WaitForSeconds(1.5f);
        animator.SetBool("charging", false);
        Debug.Log(canLaunch);
        if (canLaunch)
        {
            moveSpeed = oldSpeed;

            Vector2 force = new Vector2(player.transform.position.x - transform.position.x,
                                        player.transform.position.y - transform.position.y);
            rb.AddForce(force * 200);
            StartCoroutine(cooldown());
        }
        
    }

    IEnumerator cooldown()
    {
        
        canLaunch = false;
        yield return new WaitForSeconds(3f);
        canLaunch = true;
    }

    void TowardsPlayer()
    {
        animator.SetBool("notShootin", false);
        animator.SetBool("charging", false);
        if (Mathf.Round(player.transform.position.x * 10f) - Mathf.Round(transform.position.x * 10f) < 0)
        {
            StartCoroutine(moveLeft());
        }
        else if (Mathf.Round(player.transform.position.x * 10f) - Mathf.Round(transform.position.x * 10f) > 0)
        {
            StartCoroutine(moveRight());
        }

    }

    IEnumerator moveLeft()
    {
        animator.SetBool("notShootin",false);
        animator.SetBool("right", false);
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
        animator.SetBool("notShootin",false);
        animator.SetBool("right", true);
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
        yield return new WaitForSeconds(1.5f);
        shouldMove = true;


    }

    private void OnCollisionEnter2D(Collision2D other)
    {

        //Debug.Log("In contact with " + (other.gameObject.tag));
        if (other.gameObject.tag == "Player")
        {
            animator.SetBool("notShootin",true);
            animator.SetBool("charging",false);
            StartCoroutine(stopMoving());
        }
        else if (other.gameObject.tag == "Flail")
        {
            animator.SetBool("notShootin",true);
            animator.SetBool("charging",false);
            StartCoroutine(stopMoving());
        }
        else if (other.gameObject.tag == "Ground") {
            animator.SetBool("notShootin", true);
            animator.SetBool("charging", false);
        }
        else if (other.gameObject.tag == "Sword")
        {
            stuned();
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

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == ("Player")||collision.gameObject.tag == ("Flail"))
        {
            animator.SetBool("notShootin", true);
            //Debug.Log("got hit");
        }
    }


    public void stuned(){
        //Debug.Log("Stunned");
        StartCoroutine(stun());
    }
    
    private IEnumerator stun(){
        moveSpeed = 0;
        canLaunch = false;
        rb.velocity = new Vector2(0, 0);
        stunanimation.SetActive(true);
        animator.SetBool("notShootin", true);
        yield return new WaitForSeconds(stuntime);
        Debug.Log("unfreeze");
        canLaunch = true;
        stunanimation.SetActive(false);
        moveSpeed = pastms;
    }
}

