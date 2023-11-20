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

    public bool canJump = true; // Initially, the player can jump

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update() {
        //Debug.Log(canJump);
        if (IsGrounded() && isAlive) {
            jumpTimes = 0; // Reset jump count when touching the ground
            canJump = true; // Player can jump again
        }

        if (Input.GetButtonDown("Jump") && canJump && isAlive) {
            animator.SetBool("is_jumping",true);
            Jump();
            canJump = false; // Disable jumping until the player touches the ground


            //DELETE THIS ONCE IsGrounded() works
            animator.SetBool("is_jumping",false);
        }
    }

    private int jumpTimes = 0;

    public void Jump() {
        jumpTimes += 1;
        rb.velocity = Vector2.up * jumpForce;
        // You can play jump animation and sound here if needed
    }

    public bool IsGrounded() {        
        Collider2D groundCheck = Physics2D.OverlapCircle(feet.position, .2f, groundLayer);
        if (groundCheck != null) {
            animator.SetBool("is_jumping",false);
            print("ON GROUND");
            return true;
        }
        return false;
    }
}
