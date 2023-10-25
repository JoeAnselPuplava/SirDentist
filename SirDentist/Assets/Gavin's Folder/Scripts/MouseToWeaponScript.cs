using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseToWeaponScript : MonoBehaviour
{
    public float thrust = 1.0f;
    public float maxspeed = 3f;
    float speed;
    public GameObject weaponForcePoint;
    Rigidbody2D rb;
    Vector2 mousePos;
    //RigidBody2D weaponrb;s
    // Start is called before the first frame update
    void Start()
    {
        rb = weaponForcePoint.GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        mousePos = Input.mousePosition;
        Vector2 ballPos = rb.position;
        //rb.velocity+= new Vector2(4,4);
        rb.AddForce(mousePos * thrust);
        if(rb.velocity.magnitude > maxspeed){
            rb.velocity = Vector2.ClampMagnitude(rb.velocity,maxspeed);
        }
    }
    
}