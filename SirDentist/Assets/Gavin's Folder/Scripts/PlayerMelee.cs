using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMelee : MonoBehaviour
{
    public float swingCooldown = 0.01f;
    public Animator animator;
    public Collider2D swordCollider;
    public bool canSwing = true;

    void Start()
    {
        // Ensure the collider is initially disabled
        swordCollider.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && canSwing)
        {
            SwingSword();
        }

    }

    void SwingSword()
    {
        
        // Enable the collider when swinging the sword
        Debug.Log("Sword Enabled");
        //swordCollider.enabled = true;
        //animator.SetBool("Swing", true);
        animator.SetTrigger("Swing");
        StartCoroutine(SwingCooldown());
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the sword collider has entered a collider with the "Enemy" tag
        Debug.Log("Sword status: " + swordCollider.enabled);
        if (other.gameObject.tag == "Enemy")
        {
            // Perform actions when the sword collides with an enemy
            Debug.Log("Sword hit an enemy!");

            //Check if Eye Enemy
            EyeDamage EyeDamage = other.gameObject.GetComponent<EyeDamage>();
            if(EyeDamage != null){
                EyeDamage.meleeDamage(10);
                Debug.Log("hit");
            }
            else{
                ToothDamage toothDamage = other.gameObject.GetComponent<ToothDamage>();
                if(toothDamage != null){
                    toothDamage.meleeDamage(10);
                    Debug.Log("hit2");
                }
                else{
                    EnemyDamage rangedDamage = other.gameObject.GetComponent<EnemyDamage>();
                    if(rangedDamage != null){
                        rangedDamage.meleeDamage(10);
                        Debug.Log("hit3");
                    }
                }
            }
        }
        if(other.gameObject.tag == "BossLeg"){
            BossMainScript bossDamage = GameObject.FindGameObjectWithTag("BossObject").GetComponent<BossMainScript>();
            bossDamage.meleeDamage(10);
            other.GetComponent<BossFootScript>().freeze();
        }
        if(other.gameObject.tag == "StartingLeg"){
            BossSceneManager manager = GameObject.FindGameObjectWithTag("BossManager").GetComponent<BossSceneManager>();
            manager.attacked = true;
        }
        if(other.gameObject.tag == "BreakWall")
        {
            other.GetComponent<BreakableWall>().hitByWeapon();
        }
    }

    IEnumerator SwingCooldown()
    {   
        canSwing = false;
        //How long the collider will be out
        swordCollider.enabled = true;
        for (int i = 0; i < 50; i++) {
            yield return null;
        }
        swordCollider.enabled = false;

        //Cooldown for sword use
        for (int i = 0; i < 10; i++)
        {
            yield return null;
        }

        canSwing = true;

        //swordCollider.enabled = false;
        //yield return new WaitForSeconds(swingCooldown);


        // Disable the collider after the cooldown
        //animator.SetBool("Swing", false);
        //Debug.Log("Sword Disabled");

        //Debug.Log("Sword status: " + swordCollider.enabled);

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