using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MouseToWeaponScript : MonoBehaviour
{
    GameObject player;
    public float thrust = 400.0f;
    public float maxspeed = 40f;
    float speed;

    public Rigidbody2D ball;
    Vector2 mousePos;
    Vector2 ScreenCenter;
    
    public float maxRadius = 10f;
    public Transform targetPoint;

    public GameObject chainone;
    
    //RigidBody2D weaponrb;s
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        ScreenCenter.x = Screen.width/2;
        ScreenCenter.y = Screen.height/2;
        chainone.GetComponent<HingeJoint2D>().connectedBody = player.GetComponent<Rigidbody2D>();
        targetPoint = GameObject.FindGameObjectWithTag("rotation").transform;
    }

    // Update is called once per frame
    
    void FixedUpdate()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 ballPos = ball.position;
        Vector2 trueballPos = ballPos;
        Vector2 truePos = mousePosition;
        if (targetPoint != null)
        {
            Vector2 targetPosition = targetPoint.position;
            truePos = truePos - targetPosition;
            trueballPos = trueballPos - targetPosition;
            //Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector2 direction = targetPosition - mousePosition;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Ensure the angle is positive (between 0 and 360 degrees)
            if (angle < 0)
            {
                angle += 360f;
            }

            //Debug.Log("Angle: " + angle);



            float distance = Vector2.Distance(targetPosition, mousePosition);
            if (distance > maxRadius)
            {
                float angleRadians = angle * Mathf.Deg2Rad;
                float clampedX = maxRadius * Mathf.Cos(angleRadians);
                float clampedY = maxRadius * Mathf.Sin(angleRadians);
                truePos = new Vector2(-clampedX, -clampedY);
            }
        }
        ball.AddForce((truePos - trueballPos) * thrust);
        //Debug.Log(mousePosition);
        //ball.velocity+= (truePos - trueballPos);

        //rb.AddForce((truePos - trueballPos) * thrust);
        //ball.AddForce((truePos - trueballPos)* thrust);

        //GAVIN NOTE TO SELF: Maybe Make flail increase in spin force when beyond limited barrier?
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