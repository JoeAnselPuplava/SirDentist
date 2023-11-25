using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{

    private GameHandler gameHandler;
    public float damage = 10f;
    public float health = 50f;
    private Animator animator;
    public AudioClip hurt;
    public AudioClip hit;
    private AudioSource AudSource;

    private bool Eimmune = false;

    // Start is called before the first frame update
    void Start()
    {
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
            animator.SetTrigger("beenHit");
            //print("OWWW");
            AudioSource.PlayClipAtPoint(hurt, transform.position);

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

    public void flailDamage(float damage)
    {
        if(!Eimmune){
            health -= damage;
            checkHealth();
        }
    }

    void checkHealth()
    {
        if (health <= 0)
        {
            //Play death animation
            Destroy(gameObject);
            StartCoroutine(Immunity());
        }
    }

    private IEnumerator Immunity(){
        Eimmune = true;
        yield return new WaitForSeconds(0.2f);
        Eimmune = false;
    }

}
