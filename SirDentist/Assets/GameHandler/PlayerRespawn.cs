using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Unity.VisualScripting;

public class PlayerRespawn : MonoBehaviour {

       public GameHandler gameHandler;
       public Transform pSpawn;       // current player spawn point

       public GameObject flailpreFab;
       public GameObject flail;
       public Transform rotationPoint;

       void Start() {
              gameHandler = GameObject.FindWithTag("GameHandler").GetComponent<GameHandler>();
              flail = GameObject.FindGameObjectWithTag("Weapon");
       }

       void Update() {
              if (pSpawn != null){
                     if (GameHandler.playerHealth <= 0 && (GameHandler.Lives > 0)){
                            //comment out lines from GameHandler about EndLose screen
                            Debug.Log("I am going back to the last spawn point");
                            GameHandler.playerHealth = 100;
                            gameHandler.updateStatsDisplay();
                            Vector3 pSpn2 = new Vector3(pSpawn.position.x, pSpawn.position.y, transform.position.z);
                            gameObject.transform.position = pSpn2;
                            //Moves the flail
                            FlailRespawn();
                            
                     }
              }
       }

       public void OnTriggerEnter2D(Collider2D other) {
              if (other.gameObject.tag == "Checkpoint"){
                            pSpawn = other.gameObject.transform;
                            GameObject thisCheckpoint = other.gameObject;
                            StopCoroutine(changeColor(thisCheckpoint));
                            StartCoroutine(changeColor(thisCheckpoint));
              }
       }

       void FlailRespawn(){
              Destroy(flail);
              Vector3 flailpoint = GameObject.FindGameObjectWithTag("rotation").transform.position + new Vector3(1f, 0f, 0f);
              flail = Instantiate(flailpreFab, flailpoint, Quaternion.identity);
              flail.GetComponent<MouseToWeaponScript>().targetPoint = rotationPoint;
       }

       IEnumerator changeColor(GameObject thisCheckpoint){
              Renderer checkRend = thisCheckpoint.GetComponentInChildren<Renderer>();
              checkRend.material.color = new Color(2.4f, 0.9f, 0.9f, 0.5f);
              yield return new WaitForSeconds(0.5f);
              checkRend.material.color = Color.white;
       }
}