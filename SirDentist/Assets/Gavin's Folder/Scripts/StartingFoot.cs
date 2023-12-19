using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StartingFoot : MonoBehaviour
{
    Rigidbody2D rb;
    public int bossDamage = 25;

    public Vector2 baseSpeeddown = new Vector2(0f,-200f);
    public Vector2 baseSpeedup = new Vector2(0f,80f);

    BossSceneManager bossSceneManager;

    public Transform bottomFoot;
    public Transform groundlevel;

    bool pause = false;
    public float pausetime = 3f; 

    public GameObject player;

    public AudioClip crashlanding;

    // Start is called before the first frame update
    void Start()
    {
        groundlevel = GameObject.FindGameObjectWithTag("groundlevel").transform;
        bossSceneManager = GameObject.FindGameObjectWithTag("BossManager").GetComponent<BossSceneManager>();
        if (bossSceneManager == null)
        {
            Debug.LogError("bossSceneManager not found!");
        }
        rb = gameObject.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("rigidbody not found!");
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(groundlevel.position.y > (bottomFoot.position.y + 1f) && !pause){
            //Pause leg down
            pause = true;
            rb.velocity = new Vector2(0f,0f);
            //Play SFX
            //AudioSource.PlayClipAtPoint(crash, transform.position);
            //Spawn Enemy Stuff
            //Start wait perido
        }
        if(gameObject.transform.position.y > (groundlevel.position.y + 250f) && pause){
            Destroy(gameObject);
        }
    }
    
    public void down(){
        rb.velocity = baseSpeeddown;
    }
    public void pullUp(){
        GetComponent<InjureFlash>().injury();
        StartCoroutine(pausing());
    }

    IEnumerator pausing(){
        yield return new WaitForSeconds(0.5f);
        bossSceneManager.attacked = true;
        yield return new WaitForSeconds(0.5f);
        rb.velocity = baseSpeedup;
    }
}
