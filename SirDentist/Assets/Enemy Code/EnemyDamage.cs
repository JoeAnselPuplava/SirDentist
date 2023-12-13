using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{

    private GameHandler gameHandler;
    public int attackpower = 10;
    public float health = 100f;
    private Animator animator;
    public AudioClip hurt;
    public AudioClip hit;
    public AudioClip explode;
    private AudioSource AudSource;
    private GameObject player;

    public float stuntime = 0.7f;
    private bool Eimmune = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        AudSource = GetComponent<AudioSource>();
        player = GameObject.FindWithTag("Player");
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
            animator.SetTrigger("beenHit");
            //print("OWWW");
            //AudioSource.PlayClipAtPoint(hurt, transform.position);

        }

        else if (other.gameObject.tag == "Sword")
        {
            animator.SetTrigger("beenHit");
        }

    }


    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(waitImmune());
        }

        else if (other.gameObject.tag == "Flail" && !Eimmune)
        {
            animator.SetTrigger("beenHit");
            //print("OWWW");
            //AudioSource.PlayClipAtPoint(hurt, transform.position);

        }

        else if (other.gameObject.tag == "Sword")
        {
            animator.SetTrigger("beenHit");
        }

    }

    IEnumerator waitImmune()
    {
        //AudioSource.PlayClipAtPoint(hit, transform.position);
        AudioSource.PlayClipAtPoint(hit, transform.position);
        gameHandler.playerGetHit(attackpower);
        yield return new WaitForSeconds(0.3f);
    }

    public void flailDamage(float damage)
    {
        Debug.Log("here! " + Eimmune);
        if (!Eimmune)
        {
            Debug.Log("Hit!");
            GetComponent<InjureFlash>().injury();
            AudioSource.PlayClipAtPoint(hurt, transform.position);
            health -= damage;
            Debug.Log(health);
            checkHealth();
        }
    }

    public void meleeDamage(float damage)
    {
        Debug.Log("here! " + Eimmune);
       if (!Eimmune)
        {
            Debug.Log("Hit!");
            GetComponent<InjureFlash>().injury();
            AudioSource.PlayClipAtPoint(hurt, transform.position);
            health -= damage;
            Debug.Log(health);
            checkHealth();
            GetComponent<EnemyMovement>().stuned();
        }
    }

    void checkHealth()
    {
        if (health <= 0)
        {
            Debug.Log("Died");
            Eimmune = true;
            AudioSource.PlayClipAtPoint(explode, transform.position);
            animator.SetTrigger("die");//this plays the death animation (in this case eyeball exploding)
            //in animator killMe() is called after death anim finishes playing
            print("dying");
            StartCoroutine(backupdeath());
        }
    }
    public void killMe()
    {//destroys the enemy game object
        Destroy(gameObject);
        StartCoroutine(Immunity());
    }

    private IEnumerator Immunity()
    {
        Debug.Log("Immunity");
        Eimmune = true;
        yield return new WaitForSeconds(0.2f);
        Eimmune = false;
    }

    private IEnumerator backupdeath(){
        yield return new WaitForSeconds(1f);
        if(this.gameObject != null){
            GetComponent<InjureFlash>().injury();
            killMe();
        }
    }

}
