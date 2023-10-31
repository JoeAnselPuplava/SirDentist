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
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        shouldStab(); 
        if (shouldMove)
        {
            TowardsPlayer();
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
        
        //else if (Mathf.Round(player.transform.position.x) - Mathf.Round(transform.position.x) == 0f)
        //{
        //    //StartCoroutine(moveUp());
        //    Vector3 moveUp = new Vector3(0, 0.03f, 0);
        //    transform.position = transform.position + moveUp;
        //}

    }

    IEnumerator moveLeft()
    {
        Vector3 moveLeft = new Vector3(-0.004f, 0, 0);
        
        yield return new WaitForSeconds(0.5f);
        //transform.position = transform.position + moveLeft;

        Vector2 movement = new Vector2(-1 * moveSpeed, rb.velocity.y);
        rb.velocity = movement;
    }

    IEnumerator moveRight()
    {
        //Vector3 moveRight = new Vector3(0.004f, 0, 0);
        yield return new WaitForSeconds(0.5f);
        //transform.position = transform.position + moveRight;

        Vector2 movement = new Vector2(1 * moveSpeed, rb.velocity.y);
        rb.velocity = movement;
    }

    IEnumerator moveUp()
    {
        //Vector3 moveUp = new Vector3(0, 0.03f, 0);
        //Vector2 movement = new Vector2(rb.velocity.x, jumpForce);
        //rb.velocity = movement;
        yield return new WaitForSeconds(0.5f);
        //transform.position = transform.position + moveUp;
      
    }

    IEnumerator stopMoving()
    {
        shouldMove = false;
        yield return new WaitForSeconds(1f);
        shouldMove = true;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            animator.SetBool("hasHit", true);
            Rigidbody2D prb = player.GetComponent<Rigidbody2D>();
            Vector2 movement;
            if (rb.velocity.x > 0)
            {
                movement = new Vector2(20, 20);
            }
            else
            {
                movement = new Vector2(-20, 20);
            }
            prb.velocity = movement;
            Debug.Log(prb.velocity);
            StartCoroutine(stopMoving());
            //Destroy(other.gameObject);
        }
        else if (other.CompareTag("Flail"))
        {
            Rigidbody2D prb = other.GetComponent<Rigidbody2D>();
            rb.velocity = prb.velocity;
            StartCoroutine(stopMoving());
        }
    }
}
