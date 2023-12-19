using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSceneManager : MonoBehaviour
{  
    BossMainScript bossMain;
    public DialogueTrigger[] dialogues;

    GameObject player;

    int dialoguestage = 0;
    public GameObject shadow;
    public GameObject startingLeg;

    public GameObject bossHealthbar;
    public GameObject choicemenu;

    public bool attacked = false;
    public bool fighting = false; 
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start Scene");
        shadow.SetActive(false);
        bossHealthbar.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player");
        bossMain = GameObject.FindGameObjectWithTag("BossObject").GetComponent<BossMainScript>();
        //Dialogue just fairies and Dentist
    }

    // Update is called once per frame
    void Update()
    {
        if(!FindObjectOfType<DialogueManager>().talking && !fighting && dialoguestage == 0){
            dialogues[0].TriggerDialogue();
            dialoguestage++; 
            Debug.Log("Dia Stage 1");
        }
        if(!FindObjectOfType<DialogueManager>().talking && !fighting && dialoguestage ==1){
            //Boss shows up and main intro convo
            startingLeg.GetComponent<StartingFoot>().down();
            dialogues[1].TriggerDialogue();
            dialoguestage++;
            Debug.Log("Dia Stage 2");
        }
        if(!FindObjectOfType<DialogueManager>().talking && !fighting && dialoguestage ==2){
            shadow.SetActive(true);
            dialoguestage = -3;
            Debug.Log("Dia Stage 3");
        }
        if(attacked && dialoguestage ==-3){
            //startingLeg up
            shadow.SetActive(false);
            startingLeg.GetComponent<StartingFoot>().pullUp();
            //Bosses response, prompts player response after
            dialogues[2].TriggerDialogue();
            dialoguestage = -1;
            Debug.Log("Dia Stage 4");
        }
        if(!FindObjectOfType<DialogueManager>().talking && dialoguestage == -1 && attacked){
            choicemenu.SetActive(true);
            dialoguestage = -2;
            Debug.Log("Dia Stage 5");
        }

        //Next needs to give prompt for choicing battle diff
        if(!FindObjectOfType<DialogueManager>().talking && !fighting && dialoguestage ==10){
            Debug.Log("Start Battle!");
            bossHealthbar.SetActive(true);
            bossMain.fighting = true;
            fighting = true;
        }
        if(fighting && !bossMain.fighting && dialoguestage ==10){
            Debug.Log("Boss Defeated");
            fighting = false;
            dialogues[7].TriggerDialogue();
            dialoguestage = 100;
        }
    }

    public void stageselect(int i){
        dialogues[i].TriggerDialogue();
        choicemenu.SetActive(false);
        dialoguestage = 10;
        bossMain.difficulty = i - 2;
        bossMain.startbattle();
    }
}
