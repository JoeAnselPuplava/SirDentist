using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tooth_enemy_damage : MonoBehaviour
{
    private GameHandler gameHandler;
    public float damage = 10f;
    public float health = 50f;
    private Animator animator;
    public AudioClip hurt;
    public AudioClip hit;
    private AudioSource AudSource;

    private bool Eimmune = false;
    private float damage_level=0f;
    void Start()
    {
        damage_level=health/4;
        print(damage_level);
         animator = GetComponent<Animator>();
        AudSource = GetComponent<AudioSource>();
        if (GameObject.FindWithTag("GameHandler") != null)
        {
            gameHandler = GameObject.FindWithTag("GameHandler").GetComponent<GameHandler>();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.tag == "Player")
        {
            
            StartCoroutine(waitImmune());


        }

        else if (other.gameObject.tag == "Flail" && !Eimmune)
        {
            AudioSource.PlayClipAtPoint(hurt, transform.position);
            flailDamage(damage);
            changeArt();

        }

        else if (other.gameObject.tag == "Sword")
        {
            animator.SetTrigger("beenHit");
        }

    }

    IEnumerator waitImmune()
    {
        AudioSource.PlayClipAtPoint(hit, transform.position);
        gameHandler.playerGetHit(10);
        yield return new WaitForSeconds(0.3f);
    }

    private void changeArt(){//this changes the tooth's art based on the level of damage it has taken
    //more cracked tooth art= more damage

    if(health<= damage_level*3){
        animator.SetTrigger("state2");
    }
    if(health<= damage_level*2){
        animator.SetTrigger("state3");
    }
    if(health<= damage_level){
        animator.SetTrigger("state4");
    }


    }

    public void flailDamage(float damage)
    {
        if(!Eimmune){
            health -= damage;
            checkHealth();
        }
    }

    void checkHealth(){//kills enemy if the health is below zero
        if (health <= 0)
        {
            killMe();
        }
    }
    public void killMe(){//destroys the enemy game object
        Destroy(gameObject);
        StartCoroutine(Immunity());
    }

    private IEnumerator Immunity(){
        Eimmune = true;
        yield return new WaitForSeconds(0.2f);
        Eimmune = false;
    }
}
