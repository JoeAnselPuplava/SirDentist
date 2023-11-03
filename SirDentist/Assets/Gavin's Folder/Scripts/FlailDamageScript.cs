using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlailDamageScript : MonoBehaviour
{
    public GameObject ball;
    public Rigidbody2D rb;

    void Start()
    {
        rb = ball.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy"){
            Vector2 damage = rb.velocity;
            //call enemy function
        }
    }

}
