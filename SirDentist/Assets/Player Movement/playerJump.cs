using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerJump : MonoBehaviour {

    public Rigidbody2D rb;
    public float jumpForce = 7f;
    public Transform feet;
    public LayerMask groundLayer;
    public bool isAlive = true;

    private bool canJump = true; // Initially, the player can jump

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        //Debug.Log(canJump);
        if (IsGrounded() && isAlive) {
            jumpTimes = 0; // Reset jump count when touching the ground
            canJump = true; // Player can jump again
        }

        if (Input.GetButtonDown("Jump") && canJump && isAlive) {
            Jump();
            canJump = true; // Disable jumping until the player touches the ground
        }
    }

    private int jumpTimes = 0;

    public void Jump() {
        jumpTimes += 1;
        rb.velocity = Vector2.up * jumpForce;
        // You can play jump animation and sound here if needed
    }

    public bool IsGrounded() {
        Collider2D groundCheck = Physics2D.OverlapCircle(feet.position, 0f, groundLayer);
        if (groundCheck != null) {
            return true;
        }
        return false;
    }
}
