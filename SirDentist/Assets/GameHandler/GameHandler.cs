using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour {

      private GameObject player;
      public static int playerHealth = 100;
      public int StartPlayerHealth = 100;
      public GameObject healthText;

    //   public static int gotTokens = 0;
    //   public GameObject tokensText;

      public bool isDefending = false;


      private string sceneName;
      public static string lastLevelDied;  //allows replaying the Level where you died
    private bool hasKey = false;

      public bool immune = false;

      void Start(){
            player = GameObject.FindWithTag("Player");
            sceneName = SceneManager.GetActiveScene().name;
            if (sceneName=="MainMenu"){ //uncomment these two lines when the MainMenu exists
                  playerHealth = StartPlayerHealth;
            }
            updateStatsDisplay();
      }

    //   public void playerGetTokens(int newTokens){
    //         gotTokens += newTokens;
    //         updateStatsDisplay();
    //   }

      public void playerGetHit(int damage){
        if (!immune)
        {
            playerHealth -= damage;
            if (playerHealth >=0){
                updateStatsDisplay();
            }
            if (damage > 0){
                //player.GetComponent<PlayerHurt>().playerHit();       //play GetHit animation
            }
            StartCoroutine(Immunity());
        }

        if (playerHealth > StartPlayerHealth){
                  playerHealth = StartPlayerHealth;
                  updateStatsDisplay();
            }

        if (playerHealth <= 0){
                playerHealth = 0;
                updateStatsDisplay();
                playerDies();
        }
      }

    public void playerGetHeal(int heal)
    {
        playerHealth += heal;
        updateStatsDisplay();
    }

    public int returnPlayerHealth()
    {
        return playerHealth;
    }

    public void setKeyTrue()
    {
        hasKey = true;
    }

    public bool keyStatus()
    {
        return hasKey;
    }

    public void updateStatsDisplay(){
            Text healthTextTemp = healthText.GetComponent<Text>();
            healthTextTemp.text = "HEALTH: " + playerHealth;

            // Text tokensTextTemp = tokensText.GetComponent<Text>();
            // tokensTextTemp.text = "GOLD: " + gotTokens;
      }

      public void playerDies(){
            //player.GetComponent<PlayerHurt>().playerDead();       //play Death animation
            lastLevelDied = sceneName;       //allows replaying the Level where you died
            StartCoroutine(DeathPause());
      }

      IEnumerator DeathPause(){
            //player.GetComponent<PlayerMove>().isAlive = false;
            //player.GetComponent<PlayerJump>().isAlive = false;
            yield return new WaitForSeconds(1.0f);
            SceneManager.LoadScene("EndLose");
      }

      public void StartGame() {
            //Change by Joe to build gavin's test Level
            SceneManager.LoadScene("Mouth_Level");
             // Reset time scale to 1 when starting the game
            Time.timeScale = 1f;
            // Reset the paused state if it was paused before
            GameHandler_PauseMenu.GameisPaused = false;
    }

      // Return to MainMenu
      public void RestartGame() {
            Time.timeScale = 1f;
            GameHandler_PauseMenu.GameisPaused = false;
            SceneManager.LoadScene("MainMenu");
             // Reset all static variables here, for new games:
            playerHealth = StartPlayerHealth;
      }

      // Replay the Level where you died
      public void ReplayLastLevel() {
            Time.timeScale = 1f;
             GameHandler_PauseMenu.GameisPaused = false;
            SceneManager.LoadScene(lastLevelDied);
             // Reset all static variables here, for new games:
            playerHealth = StartPlayerHealth;
      }

      public void QuitGame() {
                #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
                #else
                Application.Quit();
                #endif
      }

      public void Credits() {
            SceneManager.LoadScene("Credits");
      }

    private IEnumerator Immunity()
    {
        immune = true;
        yield return new WaitForSeconds(0.3f);
        immune = false;
    }
}