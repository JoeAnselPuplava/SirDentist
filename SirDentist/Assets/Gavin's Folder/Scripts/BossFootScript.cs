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
    bool frozen = false;

    //Spawning info
    public int round = 1;
    public GameObject[] enemies;

    public AudioClip crashlanding;

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
        if(groundlevel.position.y > (bottomFoot.position.y + 1f) && !pause){
            //Pause leg down
            pause = true;
            rb.velocity = new Vector2(0f,0f);
            StartCoroutine(wait());
            //Make player Jump
            if(player.GetComponent<PlayerJump>().IsGrounded()){
                player.GetComponent<Rigidbody2D>().velocity = Vector2.up * (player.GetComponent<PlayerJump>().jumpForce/2);
            }
            //Play SFX
            //AudioSource.PlayClipAtPoint(crash, transform.position);
            //Spawn Enemy Stuff
            GameObject[] enemycount = GameObject.FindGameObjectsWithTag("Enemy");
            float random = Random.Range(1, 100);
            if(random < 20f * round && enemycount.Length < round){
                //pick enemy type
                GameObject spawningEnemy;
                if(random < 30f){
                    //spawn eye
                    spawningEnemy = enemies[0];
                }
                else if(random < 40f){
                    //spawn tooth
                    spawningEnemy = enemies[1];
                }
                else{
                    //spawn ranged
                    spawningEnemy = enemies[2];
                }              
                //Choose spawnpos Chose place away from player in map
                //Possible spaces (within 77 points of groundpos, y level 60 up, z 0) so range((groundpos-77),playerrange)((groundpos++77),playerrange)
                //Maybe just simply to be opposite wall?
                
                Vector3 enemyPos;
                if(player.transform.position.x > groundlevel.position.x){
                        Debug.Log("Enemy Spawning left");
                        enemyPos = new Vector3(groundlevel.position.x - 77f,groundlevel.position.y + 60f,0);
                        Instantiate(spawningEnemy,enemyPos,Quaternion.identity);
                }
                else{
                    Debug.Log("Enemy Spawning Right");
                    enemyPos = new Vector3(groundlevel.position.x + 77f,groundlevel.position.y + 60f,0);
                    Instantiate(spawningEnemy,enemyPos,Quaternion.identity);
                }
                
            }
            //Start wait perido
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
    
    public void freeze(){
        if(rb.velocity == new Vector2(0f,0f) && !frozen){
            Debug.Log("Frozen");
            frozen = true;
        }
    }

    void pullUp(){
        rb.velocity = baseSpeedup;
    }

    IEnumerator wait(){
        yield return new WaitForSeconds(pausetime);
        if(!frozen){
            pullUp();
        }
        else{
            yield return new WaitForSeconds(pausetime/2);
            pullUp();
        }
    }
}
