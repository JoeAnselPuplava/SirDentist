using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMelee : MonoBehaviour
{
    public float swingCooldown = 1.0f;
    public Collider2D swordCollider;

    private bool canSwing = true;

    void Start()
    {
        // Ensure the collider is initially disabled
        swordCollider.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && canSwing)
        {
            SwingSword();
            StartCoroutine(SwingCooldown());
        }
    }

    void SwingSword()
    {
        // Enable the collider when swinging the sword
        swordCollider.enabled = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the sword collider has entered a collider with the "Enemy" tag
        if (other.CompareTag("Enemy"))
        {
            // Perform actions when the sword collides with an enemy
            Debug.Log("Sword hit an enemy!");
            EnemyDamage enemyDamage = other.gameObject.GetComponent<EnemyDamage>();
            enemyDamage.meleeDamage(10);
        }
    }

    IEnumerator SwingCooldown()
    {
        canSwing = false;

        // Wait for the specified cooldown duration
        yield return new WaitForSeconds(swingCooldown);

        // Disable the collider after the cooldown
        swordCollider.enabled = false;

        canSwing = true;
    }
}

/*
    bool attack = false;

    public Collider2D attackbox;
    // Start is called before the first frame update
    void Start()
    {
        attackbox = gameObject.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z)){
            StartCoroutine(swing());
        }
    }

    void OnCollisionStay2D(Collision2D other) 
    { 
        Debug.Log("Whatttt");
        if (other.gameObject.tag == "Enemy") 
        { 
            Debug.Log("Hit");
            EnemyDamage enemyDamage = other.gameObject.GetComponent<EnemyDamage>();
            enemyDamage.meleeDamage(10);
        } 
    }

    public IEnumerator swing(){
        attack = true;
        yield return new WaitForSeconds(0.2f);
        attack = false;
    }
*/