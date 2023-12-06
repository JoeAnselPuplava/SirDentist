using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class AnimationJump : MonoBehaviour
{

    public float jumpForce = 7f;
    public Transform feet;
    public LayerMask groundLayer;
    public bool isAlive = true;
    private Animator animator;

    public bool in_air;

    public bool canJump = true; // Initially, the player can jump

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //Debug.Log(IsGrounded());
        if (IsGrounded() && isAlive)
        {
            canJump = true; // Player can jump again
            in_air = false;
        }
        else
        {
            in_air = true;
        }

        if (Input.GetButtonDown("Jump") && canJump && isAlive)
        {
            Jump();
            canJump = false;


        }
    }

    public void Jump()
    {
        //Debug.Log("Set to true");
        StartCoroutine(waitForJump());
    }

    IEnumerator waitForJump()
    {
        animator.SetBool("is_jumping", true);

        yield return new WaitForSeconds(0.7f);

        animator.SetBool("is_jumping", false);
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
}
