using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MouseToWeaponScript : MonoBehaviour
{
    public float thrust = 1.0f;
    public float maxspeed = 3f;
    float speed;
    public GameObject weaponForcePoint;
    Rigidbody2D rb;

    public Rigidbody2D ball;
    Vector2 mousePos;
    Vector2 ScreenCenter;
    
    
    //RigidBody2D weaponrb;s
    // Start is called before the first frame update
    void Start()
    {
        ScreenCenter.x = Screen.width/2;
        ScreenCenter.y = Screen.height/2;
        rb = weaponForcePoint.GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log(mousePos - ScreenCenter);
        mousePos = Input.mousePosition;
        Vector2 ballPos = rb.position;
        rb.velocity+= new Vector2((mousePos.x - ScreenCenter.x) * thrust,(mousePos.y - ScreenCenter.y)* thrust);
        ball.velocity+= new Vector2((mousePos.x - ScreenCenter.x) / 150,(mousePos.y - ScreenCenter.y)/ 150);
        //rb.AddForce((mousePos - ScreenCenter) * thrust);
        if(rb.velocity.magnitude > maxspeed){
            rb.velocity = Vector2.ClampMagnitude(rb.velocity,maxspeed);
        }
        if(ball.velocity.magnitude > maxspeed){
            ball.velocity = Vector2.ClampMagnitude(rb.velocity,maxspeed);
        }
    }
    
}