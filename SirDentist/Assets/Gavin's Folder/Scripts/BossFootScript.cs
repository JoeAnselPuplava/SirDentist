using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossFootScript : MonoBehaviour
{
    Rigidbody2D rb;
    public int bossDamage = 25;

    public Vector2 baseSpeeddown = new Vector2(0f,-200f);
    public Vector2 baseSpeedup = new Vector2(0f,80f);

    GameHandler gameHandler;

    public Transform bottomFoot;
    public Transform groundlevel;

    bool pause = false;
    public float pausetime = 3f; 

    public GameObject player;
    // Start is called before the first frame update
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        groundlevel = GameObject.FindGameObjectWithTag("groundlevel").transform;
        gameHandler = GameObject.FindGameObjectWithTag("GameHandler").GetComponent<GameHandler>();
        if (gameHandler == null)
        {
            Debug.LogError("GameHandler not found!");
        }
        rb = gameObject.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("rigidbody not found!");
        }
        rb.velocity = baseSpeeddown;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(groundlevel.position.y > bottomFoot.position.y && !pause){
            pause = true;
            rb.velocity = new Vector2(0f,0f);
            Debug.Log(player.GetComponent<PlayerJump>().canJump);
            if(player.GetComponent<PlayerJump>().IsGrounded()){
                player.GetComponent<Rigidbody2D>().velocity = Vector2.up * (player.GetComponent<PlayerJump>().jumpForce/2);
                //Debug.Log("hello2");
            }
            StartCoroutine(wait());
        }
        if(gameObject.transform.position.y > (groundlevel.position.y + 250f) && pause){
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.tag == "Player" && !pause) 
        { 
            Debug.Log("Stomped");
            gameHandler.playerGetHit(bossDamage);
        }

    }

    void pullUp(){
        rb.velocity = baseSpeedup;
    }

    IEnumerator wait(){
        yield return new WaitForSeconds(pausetime);
        pullUp();
    }
}
