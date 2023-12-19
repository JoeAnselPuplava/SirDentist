using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BossMainScript : MonoBehaviour
{
    public int round = 1;

    public float bossHealth;

    float spawningspeed;

    public float warningtime = 2f;
    public GameObject warningprefab;
    public GameObject player;
    public bool fighting = false;

    public GameObject footprefab;

    public Transform groundlevel;
    public Transform leftWall;
    public Transform rightWall;
    public bool Eimmune = false;

    public Text healthbar;

    float timePassed = 0f;
    public int difficulty = 1;

    void Start()
    {
        groundlevel = GameObject.FindGameObjectWithTag("groundlevel").transform;
        player = GameObject.FindGameObjectWithTag("Player").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(bossHealth <= 0 && fighting){
            died();
            bossHealth = 0;
            healthbar.text = "Boss Health: " + 0;
        }
        else{
            healthbar.text = "Boss Health: " + Mathf.Round(bossHealth);
        }

        if(bossHealth < (500f * difficulty * 0.8f) && round == 1){
            Debug.Log("Round 2");
            round = 2;
            spawningspeed = spawningspeed * 0.8f;
            warningtime = warningtime * 0.75f;
        }
        else if(bossHealth < (500f * difficulty * 0.4f) && round == 2){
            Debug.Log("Round 3");
            round = 3;
            spawningspeed = spawningspeed * 0.85f;
            warningtime = warningtime * 0.8f;
        }
        //Repeating clock for spawner
        timePassed += Time.deltaTime;
        if(timePassed > spawningspeed)
        {
            spawnfoot();
            timePassed = 0f;
        } 

    }

    public void startbattle(){
        bossHealth = 500f * difficulty;
        timePassed = -2f;
        spawningspeed = 9f - difficulty;
        warningtime = 3f - (difficulty/2);

        Debug.Log(difficulty);
    }
    public void flailDamage(float damage)
    {
        if(!Eimmune){
            damageflash();
            bossHealth -= damage;
            StartCoroutine(Immunity());
        }
    }
    public void meleeDamage(float damage)
    {
        if(!Eimmune){
            damageflash();
            bossHealth -= damage/2;
            StartCoroutine(Immunity());
        }
    }

    void damageflash(){
        GameObject[] legs = GameObject.FindGameObjectsWithTag("BossLeg");
        foreach(GameObject leg in legs){
            leg.GetComponent<InjureFlash>().injury();
        }
        
    }

    void died(){
        Debug.Log("Boss has died");
        fighting = false;
        GameObject[] enemycount = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject enemy in enemycount){
            Destroy(enemy);
        }
    }

    void spawnfoot(){
        if(fighting){
            Debug.Log("FootSpawn");
            float playerpos = player.transform.position.x;
            Vector3 position = new Vector3(Random.Range(playerpos-40.0f, playerpos+40.0f), transform.position.y, 0);
            if(position.x < leftWall.position.x){
                position.x = leftWall.position.x;
            }
            else if(position.x > rightWall.position.x){
                position.x = rightWall.position.x;
            }
            GameObject shadow = Instantiate(warningprefab, new Vector3(position.x+1f,groundlevel.position.y-1f,position.z-2.1f), Quaternion.identity);   
            StartCoroutine(footpause(position, shadow));
        }
    }
    private IEnumerator Immunity()
    {
        Eimmune = true;
        yield return new WaitForSeconds(0.3f);
        Eimmune = false;
    }

    IEnumerator footpause(Vector3 position, GameObject shadow){
        yield return new WaitForSeconds(warningtime);
        GameObject foot = Instantiate(footprefab, new Vector3(position.x,position.y,position.z-3f), Quaternion.identity);
        if((int)Random.Range(0f, 2f) == 1){
            foot.transform.localScale = new Vector3(-1,1,1);
        }
        foot.GetComponent<BossFootScript>().pausetime = (foot.GetComponent<BossFootScript>().pausetime - (difficulty * 0.5f)) / round;
        foot.GetComponent<BossFootScript>().round = round;
        foot.GetComponent<BossFootScript>().difficulty = difficulty;
        foot.GetComponent<BossFootScript>().shadow = shadow;
    }

}
