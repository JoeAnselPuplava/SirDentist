using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSceneManager : MonoBehaviour
{  
    //public BossMainScript bossMain;
    public DialogueTrigger startdialogue; 
    // Start is called before the first frame update
    void Start()
    {
        startdialogue.TriggerDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
