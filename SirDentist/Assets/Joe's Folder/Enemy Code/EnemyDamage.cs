using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{

    private GameHandler gameHandler;
    public float damage = 10f;
    public float health = 50f;
    private Animator animator;

    private bool immune = false;

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindWithTag("GameHandler") != null)
        {
            gameHandler = GameObject.FindWithTag("GameHandler").GetComponent<GameHandler>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.tag == "Player")
        {
            //animator.SetBool("hasHit", true);
            gameHandler.playerGetHit(10);
        }

        else if (other.gameObject.tag == "Flail")
        {

        }

        else if (other.gameObject.tag == "Sword")
        {

        }
    }

    public void flailDamage(float damage)
    {
        if(!immune){
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
        immune = true;
        yield return new WaitForSeconds(0.2f);
        immune = false;
    }
}
