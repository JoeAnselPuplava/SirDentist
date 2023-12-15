using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToothDamage : MonoBehaviour
{

    private GameHandler gameHandler;
    public int attackpower = 10;
    public float health = 50f;
    private Animator animator;
    public AudioClip hurt;
    public AudioClip hit;
    public AudioClip explode;
    private AudioSource AudSource;
    private GameObject player;

    public float stuntime = 0.7f;
    private bool Eimmune = false;

    private bool canhit = true;

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

        if (other.gameObject.tag == "Player" && canhit)
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

        if (other.gameObject.tag == "Player" && canhit)
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
        canhit = false;
        AudioSource.PlayClipAtPoint(hit, transform.position);
        gameHandler.playerGetHit(attackpower);
        yield return new WaitForSeconds(0.8f);
        canhit = true;
    }

    public void flailDamage(float damage)
    {

        if (!Eimmune)
        {
            GetComponent<InjureFlash>().injury();
            AudioSource.PlayClipAtPoint(hurt, transform.position);
            health -= damage;
            checkHealth();
        }
    }

    void checkHealth()
    {
        if (health <= 0)
        {
            GetComponent<InjureFlash>().injury();
            Eimmune = true;
            Debug.Log("dying tooth");
            StartCoroutine(backupdeath());
            AudioSource.PlayClipAtPoint(explode, transform.position);
            animator.SetTrigger("die");//this plays the death animation (in this case eyeball exploding)
            //in animator killMe() is called after death anim finishes playing
        }
    }
    public void meleeDamage(float damage)
    {
       if (!Eimmune)
        {
            AudioSource.PlayClipAtPoint(hurt, transform.position);
            health -= damage;
            checkHealth();
            GetComponent<LauchingMovement>().stuned();
        }
    }

    public void killMe()
    {
        Destroy(gameObject);
        StartCoroutine(Immunity());
    }

    private IEnumerator Immunity()
    {
        Eimmune = true;
        yield return new WaitForSeconds(0.2f);
        Eimmune = false;
    }

    private IEnumerator backupdeath(){
        Debug.Log("test");
        yield return new WaitForSeconds(1.5f);
        if(this.gameObject != null){
            GetComponent<InjureFlash>().injury();
            yield return new WaitForSeconds(0.2f);
            killMe();
        }
    }
}
