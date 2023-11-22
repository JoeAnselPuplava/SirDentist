using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerJump : MonoBehaviour {

    public Rigidbody2D rb;
    public float jumpForce = 7f;
    public Transform feet;
    public LayerMask groundLayer;
    public bool isAlive = true;
    private Animator animator;

    public bool in_air;

    public bool canJump = true; // Initially, the player can jump

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update() {
        //Debug.Log(IsGrounded());
        if (IsGrounded() && isAlive)
        {
            jumpTimes = 0; // Reset jump count when touching the ground
            canJump = true; // Player can jump again
            in_air = false;
        }
        else
        {
            in_air = true;
        }

        if (Input.GetButtonDown("Jump") && canJump && isAlive) {
            
            Jump();
            canJump = false; // Disable jumping until the player touches the ground

            //DELETE THIS ONCE IsGrounded() works
            animator.SetBool("is_jumping", false);
            Debug.Log("Made jumping false");

        }
    }

    private int jumpTimes = 0;

    public void Jump() {

        //StartCoroutine(waitForJump());
        animator.SetBool("is_jumping", true);

        jumpTimes += 1;
        rb.velocity = Vector2.up * jumpForce;

        // You can play jump animation and sound here if needed
    }

    IEnumerator waitForJump()
    {
        animator.SetBool("is_jumping", true);

        yield return new WaitForSeconds(0.1f);

        animator.SetBool("is_jumping", false);
    }

    public bool IsGrounded() {        
        Collider2D groundCheck = Physics2D.OverlapCircle(feet.position, .2f, groundLayer);
        if (groundCheck != null) {
            return true;
        }
        return false;
    }
}
