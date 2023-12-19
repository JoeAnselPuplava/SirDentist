using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneStart : MonoBehaviour
{
    // Start is called before the first frame update

    public DialogueTrigger trigger;
    void Start()
    {
        trigger.TriggerDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        //hello
    }
}
