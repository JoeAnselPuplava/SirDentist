using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKnockBack : MonoBehaviour
{
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Rigidbody2D prb = other.gameObject.GetComponent<Rigidbody2D>();
        if (other.gameObject.tag == "Player")
        {            
            Vector2 force = new Vector2(0, 0);
            if (rb.velocity.x > 0)
            {
                force = new Vector2(200, 200);
            }

            else
            {
                force = new Vector2(-200, 200);
            }

            prb.AddForce(force);
        }

        else if (other.gameObject.tag == "Flail")
        {
            //Vector2 force = new Vector2(prb.velocity.x * 50, Mathf.Abs(prb.velocity.y) * 20);

            Vector2 force = new Vector2(0, 0);

            if (Mathf.Round(other.gameObject.transform.position.x * 10f) - Mathf.Round(transform.position.x * 10f) < 0)
            {
                force = new Vector2(500, 500);
            }
            else if (Mathf.Round(other.gameObject.transform.position.x * 10f) - Mathf.Round(transform.position.x * 10f) > 0)
            {
                force = new Vector2(-500, 500);
            }

            rb.AddForce(force);
        }
    }
}
