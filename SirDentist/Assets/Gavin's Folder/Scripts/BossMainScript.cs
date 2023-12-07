using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossMainScript : MonoBehaviour
{
    public float bossHealth = 1000f;

    float spawningspeed = 7f;
    float feetspeed = 1f;

    public float warningtime = 2f;
    public GameObject warningprefab;
    public GameObject player;

    bool alive = true;

    public GameObject footprefab;

    public Transform groundlevel;
    private bool Eimmune = false;
    void Start()
    {
        groundlevel = GameObject.FindGameObjectWithTag("groundlevel").transform;
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
    public void flailDamage(float damage)
    {
        bossHealth -= damage;
    }
    public void meleeDamage(float damage)
    {
        bossHealth -= damage;
    }
    void died(){
        Debug.Log("Boss has died");
        alive = false;
    }

    void spawnfoot(){
        if(alive){
            Debug.Log("FootSpawn");
            float playerpos = player.transform.position.x;
            Vector3 position = new Vector3(Random.Range(playerpos-40.0f, playerpos+40.0f), transform.position.y, 0);
            GameObject shadow = Instantiate(warningprefab, new Vector3(position.x,groundlevel.position.y-1,groundlevel.position.z), Quaternion.identity);
            StartCoroutine(footpause(position));
            StartCoroutine(shadowkill(shadow));
        }
    }

    IEnumerator footpause(Vector3 position){
        yield return new WaitForSeconds(warningtime);
        Instantiate(footprefab, position, Quaternion.identity);
    }

    IEnumerator shadowkill(GameObject shadow){
        yield return new WaitForSeconds(warningtime + 4f);
        Destroy(shadow);
    }
}
