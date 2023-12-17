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

    public bool attacked = false;
    public bool fighting = false; 
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start Scene");
        shadow.SetActive(false);
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
        }
        if(!FindObjectOfType<DialogueManager>().talking && !fighting && dialoguestage ==1){
            //Boss shows up and main intro convo
            startingLeg.GetComponent<StartingFoot>().down();
            dialogues[1].TriggerDialogue();
            dialoguestage++;
        }
        if(!FindObjectOfType<DialogueManager>().talking && !fighting && dialoguestage ==2){
            shadow.SetActive(true);
        }
        if(attacked && dialoguestage ==2){
            //startingLeg up
            shadow.SetActive(false);
            startingLeg.GetComponent<StartingFoot>().pullUp();
            //Bosses response, prompts player response after
            dialogues[2].TriggerDialogue();
            dialoguestage++;
        }
        //Next needs to give prompt for choicing battle diff
        if(!FindObjectOfType<DialogueManager>().talking && !fighting && dialoguestage ==3){
            Debug.Log("Start Battle!");
            fighting = true;
            bossMain.fighting = fighting;
        }
        if(fighting && !bossMain.fighting && dialoguestage ==3){
            Debug.Log("Boss Defeated");
            fighting = false;
            dialogues[3].TriggerDialogue();
        }
    }
}
