using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PickUp : MonoBehaviour{

      public GameHandler gameHandler;
      //public playerVFX playerPowerupVFX;
      public bool isCoinPickUp = true;
      

      void Start(){
            gameHandler = GameObject.FindWithTag("GameHandler").GetComponent<GameHandler>();
            //playerPowerupVFX = GameObject.FindWithTag("Player").GetComponent<playerVFX>();
      }

      public void OnTriggerEnter2D (Collider2D other){
            if (other.gameObject.tag == "Player" || other.gameObject.tag == "Flail"){
                  GetComponent<Collider2D>().enabled = false;
                  //GetComponent< AudioSource>().Play();
                  StartCoroutine(DestroyThis());

                  if (isCoinPickUp == true) {
                        gameHandler.playerGetCoins(1);
                        //playerPowerupVFX.powerup();
                  }
            }
      }

      IEnumerator DestroyThis(){
            yield return new WaitForSeconds(0.3f);
            Destroy(gameObject);
      }

}