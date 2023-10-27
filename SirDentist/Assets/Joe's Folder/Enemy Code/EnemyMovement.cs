using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public GameObject player;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        TowardsPlayer();
    }

    void TowardsPlayer()
    {
        if (Mathf.Round(player.transform.position.x*10f) - Mathf.Round(transform.position.x*10f) < 0)
        {
            StartCoroutine(moveLeft());
        }
        else if (Mathf.Round(player.transform.position.x*10f) - Mathf.Round(transform.position.x*10f) > 0)
        {
            StartCoroutine(moveRight());
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
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
            StartCoroutine(moveUp());
            //Destroy(other.gameObject);
        }
    }
}
