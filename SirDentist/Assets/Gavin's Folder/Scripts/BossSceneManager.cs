using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSceneManager : MonoBehaviour
{  
    BossMainScript bossMain;
    public DialogueTrigger startdialogue;
    public DialogueTrigger enddialogue;

    public bool fighting = false; 
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start Scene");
        bossMain = GameObject.FindGameObjectWithTag("BossObject").GetComponent<BossMainScript>();
        startdialogue.TriggerDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if(!FindObjectOfType<DialogueManager>().talking && !fighting && bossMain.bossHealth > 1){
            Debug.Log("Start Battle!");
            fighting = true;
            bossMain.fighting = fighting;
        }
        if(fighting && !bossMain.fighting){
            Debug.Log("Boss Defeated");
            fighting = false;
            enddialogue.TriggerDialogue();
        }
    }
}
