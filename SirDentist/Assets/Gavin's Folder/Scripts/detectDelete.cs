using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detectDelete : MonoBehaviour
{
    public GameObject target;

    bool detected = false;

    public DialogueTrigger trigger;

    // Update is called once per frame
    void Update()
    {
        if(!detected && target.GetComponent<grabKey>().once == false){
            detected = true;
            trigger.TriggerDialogue();
        }
    }
}
