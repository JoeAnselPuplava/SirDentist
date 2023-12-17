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
        float damage = rb.velocity.magnitude;
        if (damage < 8f){
            damage = 0;
        }
        if(other.gameObject.tag == "Enemy"){
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
        if(other.gameObject.tag == "BossLeg"){
            BossMainScript bossDamage = GameObject.FindGameObjectWithTag("BossObject").GetComponent<BossMainScript>();
            bossDamage.flailDamage(damage * damagemulti);
        }
        if(other.gameObject.tag == "StartingLeg"){
            BossSceneManager manager = GameObject.FindGameObjectWithTag("BossManager").GetComponent<BossSceneManager>();
            manager.attacked = true;
        }
    }

}
