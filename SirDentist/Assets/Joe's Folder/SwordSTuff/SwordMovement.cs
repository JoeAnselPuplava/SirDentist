using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordMovement : MonoBehaviour
{
    public float thrust = 400.0f;
    public float maxspeed = 3f;
    float speed;
    Rigidbody2D rb;

    public Transform ball;
    Vector2 mousePos;
    Vector2 ScreenCenter;

    public float maxRadius = 10f;
    public Transform targetPoint;

    //RigidBody2D weaponrb;s
    // Start is called before the first frame update
    void Start()
    {
        ScreenCenter.x = Screen.width / 2;
        ScreenCenter.y = Screen.height / 2;
        rb = GetComponent<Rigidbody2D>();

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

        //Debug.Log(mousePosition);
        rb.velocity += (truePos - trueballPos);

        //rb.AddForce((truePos - trueballPos) * thrust);
        //ball.AddForce((truePos - trueballPos)* thrust);

    }

}
