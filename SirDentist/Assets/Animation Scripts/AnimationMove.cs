using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationMove : MonoBehaviour
{
    //private bool FaceRight = false; // determine which way the player is facing.
    //public static float runSpeed = 20f;
    //public float startSpeed = 20f;
    public bool isAlive = true;
    //private Vector2 velocity;
    public Animator animator;
    public GameObject swordCollider;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isAlive)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            if (Mathf.Abs(horizontalInput) > 0.1f)
            {
                animator.SetBool("walk", true);
            }
            else
            {
                animator.SetBool("walk", false);
            }
            // Turning: Reverse if input is moving the Player right and Player faces left
            //if ((horizontalInput < 0 && !FaceRight) || (horizontalInput > 0 && FaceRight))
            //{
            //    playerTurn();
            //}
            //if (Input.GetKeyDown(KeyCode.Mouse0) && swordCollider.GetComponent<PlayerMelee>().canSwing)
            //{
            //    Debug.Log("Swing animation");
            //    animator.SetTrigger("Swing");
            //}
        }
    }

    //private void playerTurn()
    //{
    //    // NOTE: Switch player facing label
    //    FaceRight = !FaceRight;

    //    // NOTE: Multiply player's x local scale by -1.
    //    Vector3 theScale = rb2D.transform.localScale;
    //    theScale.x *= -1;
    //    rb2D.transform.localScale = theScale;
    //}
}

