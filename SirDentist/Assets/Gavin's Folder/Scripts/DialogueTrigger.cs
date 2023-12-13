using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    public void TriggerDialogue(){
        Debug.Log("Dialogue Triggered");
        GameObject.FindWithTag("DialogueManager").GetComponent<DialogueManager>().StartDialogue(dialogue);
    }
}
