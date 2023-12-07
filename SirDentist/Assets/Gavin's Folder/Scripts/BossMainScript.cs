using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMainScript : MonoBehaviour
{
    public float bossHealth = 1000f;

    float spawningspeed = 7f;
    float feetspeed = 1f;

    public GameObject player;

    bool alive = true;

    public GameObject footprefab;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").gameObject;
        InvokeRepeating("spawnfoot", 5f, spawningspeed);
    }

    // Update is called once per frame
    void Update()
    {
        if(bossHealth < 0){
            died();
        }
    }

    void FixedUpdate(){

    }

    void died(){
        Debug.Log("Boss has died");
        alive = false;
    }

    void spawnfoot(){
        if(alive){
            Debug.Log("FootSpawn");
            float playerpos = player.transform.position.x;
            var position = new Vector3(Random.Range(playerpos-10.0f, playerpos+10.0f), transform.position.y, 0);
            Instantiate(footprefab, position, Quaternion.identity);
        }
    }
}
