using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swimmers : MonoBehaviour
{
    public float moveSpeed = 2.0f;//speed that the swimmers move across the screen
    private bool isMovingRight = true;
    public float moveDist=5.0f;// half the distance that the swimmers move


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isMovingRight && transform.position.x >= moveDist)
        {
            isMovingRight = false;
            Flip();
        }
        else if (!isMovingRight && transform.position.x <= -1*moveDist)
        {
            isMovingRight = true;
            Flip();
            
        }

        // Moves the dog
        if (isMovingRight)
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        else
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        
    }

    private void Flip()//flips the swimmers once they reach their furthest point
    {
        Vector3 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;
    }
}
