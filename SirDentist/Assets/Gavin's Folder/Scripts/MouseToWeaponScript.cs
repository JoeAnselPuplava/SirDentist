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
    
    public float maxRadius = 10f;
    public Transform targetPoint;
    
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
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 ballPos = ball.position;

        if (targetPoint != null)
        {
            Vector2 targetPosition = targetPoint.position;
            //Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector2 direction = targetPosition - mousePosition;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Ensure the angle is positive (between 0 and 360 degrees)
            if (angle < 0)
            {
                angle += 360f;
            }

            Debug.Log("Angle: " + angle);

            float distance = Vector2.Distance(targetPosition, mousePosition);
            if (distance > maxRadius)
            {
                float angleRadians = angle * Mathf.Deg2Rad;
                float clampedX = targetPosition.x + maxRadius * Mathf.Cos(angleRadians);
                float clampedY = targetPosition.y + maxRadius * Mathf.Sin(angleRadians);
                mousePosition = new Vector2(-clampedX, -clampedY);
            }
        }

        Debug.Log(mousePosition);
        rb.velocity+= mousePosition - ballPos;
        ball.velocity+= mousePosition - ballPos;
    }

}


    /*

                
    
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
    


        

        
    */