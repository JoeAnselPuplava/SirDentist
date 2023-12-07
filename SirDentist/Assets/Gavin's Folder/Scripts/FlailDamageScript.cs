using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlailDamageScript : MonoBehaviour
{
    public GameObject ball;
    Rigidbody2D rb;

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
            //Check if Eye Enemy
            EyeDamage EyeDamage = other.gameObject.GetComponent<EyeDamage>();
            if(EyeDamage != null){
                EyeDamage.flailDamage(damage * damagemulti);
                Debug.Log("hit");
            }
            //Check if tooth enemy
            else{
                ToothDamage toothDamage = other.gameObject.GetComponent<ToothDamage>();
                if(toothDamage != null){
                    toothDamage.flailDamage(damage * damagemulti);
                    Debug.Log("hit2");
                }
                //check if ranged enemy
                else{
                    EnemyDamage rangedDamage = other.gameObject.GetComponent<EnemyDamage>();
                    if(rangedDamage != null){
                        rangedDamage.flailDamage(damage * damagemulti);
                        Debug.Log("hit3");
                    }
                }
            }
        }
    }

}
