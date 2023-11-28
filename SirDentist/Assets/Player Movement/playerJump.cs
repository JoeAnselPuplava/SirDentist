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
        if (IsGrounded() && isAlive)
        {
            jumpTimes = 0; // Reset jump count when touching the ground
            canJump = true; // Player can jump again

        }

        if (Input.GetButtonDown("Jump") && canJump && isAlive) {
            
            Jump();

        }
    }

    private int jumpTimes = 0;

    public void Jump() {

        canJump = false; // Disable jumping until the player touches the ground

        rb.velocity = Vector2.up * jumpForce;

        // You can play jump animation and sound here if needed
    }

    IEnumerator createJump()
    {
        //Debug.Log("Running");

        yield return new WaitForSeconds(0.1f);

        
    }

    public bool IsGrounded() {        
        Collider2D groundCheck = Physics2D.OverlapCircle(feet.position, .2f, groundLayer);
        if (groundCheck != null) {
            return true;
        }
        canJump = false;
        return false;
    }
}
