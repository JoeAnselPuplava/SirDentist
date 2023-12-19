using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FlailDamageScript : MonoBehaviour
{
    public GameObject ball;
    Rigidbody2D rb;
    public GameObject damageTextPrefab; // Prefab for the damage indicator text

    public float damagemulti = 0.3f;

    void Start()
    {
        rb = ball.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void ShowDamageIndicator(float damage, Vector3 position)
    {
        // Offset the damage text slightly above the enemy
        Vector3 offset = new Vector3(0f, 6f, 0f);
        // Instantiate the damage indicator text
        GameObject damageText = Instantiate(damageTextPrefab, position + offset, Quaternion.identity);
        // Set the damage value text using TMP_Text, converting damage to integer
        damageText.GetComponent<TMP_Text>().text = "-" + Mathf.RoundToInt(damage).ToString();
        // Destroy the damage indicator after a certain time (adjust as needed)
        Destroy(damageText, .5f);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        float damage = rb.velocity.magnitude;
        if (damage < 8f){
            damage = 0;
        }
        if(other.gameObject.tag == "Enemy" && damage != 0){
            //Check if Eye Enemy
            EyeDamage EyeDamage = other.gameObject.GetComponent<EyeDamage>();
            if(EyeDamage != null){
                if(EyeDamage.Eimmune == false){
                    // Show damage indicator on the enemy
                    ShowDamageIndicator(damage * damagemulti, other.contacts[0].point);
                }
                EyeDamage.flailDamage(damage * damagemulti);
                
                Debug.Log("hit");
            }
            //Check if tooth enemy
            else{
                ToothDamage toothDamage = other.gameObject.GetComponent<ToothDamage>();
                if(toothDamage != null){
                    if(toothDamage.Eimmune == false){
                        // Show damage indicator on the enemy
                        ShowDamageIndicator(damage * damagemulti, other.contacts[0].point);
                    }
                    toothDamage.flailDamage(damage * damagemulti);
                    
                    Debug.Log("hit2");
                }
                //check if ranged enemy
                else{
                    EnemyDamage rangedDamage = other.gameObject.GetComponent<EnemyDamage>();
                    if(rangedDamage != null){
                        if(rangedDamage.Eimmune == false){
                            // Show damage indicator on the enemy
                            ShowDamageIndicator(damage * damagemulti, other.contacts[0].point);
                        }
                        rangedDamage.flailDamage(damage * damagemulti);
                        Debug.Log("hit3");
                    }
                }
            }
        }
        if(other.gameObject.tag == "BossLeg" && damage != 0){
            BossMainScript bossDamage = GameObject.FindGameObjectWithTag("BossObject").GetComponent<BossMainScript>();
            if(bossDamage.Eimmune == false){
                // Show damage indicator on the enemy
                ShowDamageIndicator(damage * damagemulti, other.contacts[0].point);
            }
            bossDamage.flailDamage(damage * damagemulti);
            ShowDamageIndicator(damage * damagemulti, other.contacts[0].point);
        }
        if(other.gameObject.tag == "StartingLeg"){
            BossSceneManager manager = GameObject.FindGameObjectWithTag("BossManager").GetComponent<BossSceneManager>();
            manager.attacked = true;
        }

    }

}
