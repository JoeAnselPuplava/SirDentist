using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Rigidbody2D rb2D;
    private bool FaceRight = false; // determine which way the player is facing.
    public static float runSpeed = 20f;
    public float startSpeed = 20f;
    public bool isAlive = true;
    private Vector2 velocity;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isAlive)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            velocity = new Vector2(horizontalInput * runSpeed, rb2D.velocity.y);

            // Turning: Reverse if input is moving the Player right and Player faces left
            if ((horizontalInput < 0 && !FaceRight) || (horizontalInput > 0 && FaceRight))
            {
                playerTurn();
            }
        }
    }

    void FixedUpdate()
    {
        // Slow down on hills / stop sliding from velocity
        if (velocity.x == 0)
        {
            velocity.x /= 1.1f;
        }

        rb2D.velocity = velocity;
    }

    private void playerTurn()
    {
        // NOTE: Switch player facing label
        FaceRight = !FaceRight;

        // NOTE: Multiply player's x local scale by -1.
        Vector3 theScale = rb2D.transform.localScale;
        theScale.x *= -1;
        rb2D.transform.localScale = theScale;
    }
}
