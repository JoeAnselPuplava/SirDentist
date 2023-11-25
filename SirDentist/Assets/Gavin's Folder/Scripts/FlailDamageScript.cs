using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlailDamageScript : MonoBehaviour
{
    public GameObject ball;
    public Rigidbody2D rb;

    public float damagemulti = 0.3f;

    void Start()
    {
        rb = ball.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Enemy"){
            float damage = rb.velocity.magnitude;
            if (damage < 1.5f){
                damage = 0;
            }
            //float damage = Mathf.Sqrt(Mathf.Pow(Mathf.Abs(rb.velocity.x),2))
            //+ Mathf.Sqrt(Mathf.Pow(Mathf.Abs(rb.velocity.y),2));
            //Debug.Log("Damage:" + damage * damagemulti);
            EnemyDamage enemyDamage = other.gameObject.GetComponent<EnemyDamage>();
            enemyDamage.flailDamage(damage * damagemulti);
            //call enemy function
        }
    }

}
