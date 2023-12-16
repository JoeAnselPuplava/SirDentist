using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PickUp : MonoBehaviour{

      public GameHandler gameHandler;
      //public playerVFX playerPowerupVFX;
      public bool isCoinPickUp = true;
      public bool playerCoin = true;
      

      void Start(){
            gameHandler = GameObject.FindWithTag("GameHandler").GetComponent<GameHandler>();
            //playerPowerupVFX = GameObject.FindWithTag("Player").GetComponent<playerVFX>();
      }

      public void OnTriggerEnter2D (Collider2D other){
            if ((playerCoin && other.gameObject.tag == "Player") ||
               (!playerCoin && other.gameObject.tag == "Flail")){

                  GetComponent<Collider2D>().enabled = false;
                  //GetComponent< AudioSource>().Play();
              
                  if (isCoinPickUp == true) {
                        gameHandler.playerGetCoins(1);
                        //playerPowerupVFX.powerup();
                  }
                  Destroy(gameObject);
        }
      }

}