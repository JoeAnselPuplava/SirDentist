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
        Debug.Log("HIT!");
        if(other.tag == "Enemy"){
            float damage = Mathf.Sqrt(Mathf.Pow(Mathf.Abs(rb.velocity.x),2))
            + Mathf.Sqrt(Mathf.Pow(Mathf.Abs(rb.velocity.y),2));
            Debug.Log(damage);
            EnemyDamage enemyDamage = other.GetComponent<EnemyDamage>();
            enemyDamage.flailDamage(Mathf.RoundToInt(damage));
            //call enemy function
        }
    }

}
