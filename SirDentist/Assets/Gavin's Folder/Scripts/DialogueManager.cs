using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public Queue<string> titles;
    public Queue<string> lines;
    public TMP_Text titlebox;
    public TMP_Text textbox;

    public GameObject dialogueBox;

    public bool talking = false;

    //public CameraFollow2DLERP camerascript;
    //float oldcameradist;
    // Start is called before the first frame update
    void Start()
    {
        titles = new Queue<string>();
        lines = new Queue<string>();
        //camerascript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow2DLERP>();
    }

    public void StartDialogue(Dialogue dialogue){
        //Show box and freeze time
        dialogueBox.SetActive(true);
        talking = true;
        lines.Clear();
        //Shift Camera down
        //oldcameradist = camerascript.up;
        //camerascript.up = 0;
        
        foreach (string sentence in dialogue.sentences){
            lines.Enqueue(sentence);
        }
        foreach (string title in dialogue.titles){
            titles.Enqueue(title);
        }
        //DisplayNextSentence();
    }

    public void DisplayNextSentence(){
        if(lines.Count == 0){
            EndDialogue();
            //Start time up again close box
            dialogueBox.SetActive(false);
            return;
        }
        string title = titles.Dequeue();
        titlebox.text = title;
        string sentence = lines.Dequeue();
        textbox.text = sentence;
    }
    public void EndDialogue(){
        Debug.Log("End of Convo");
        talking = false;
        //camerascript.up = oldcameradist;
    }
}

/*

public string[] title;
    public string[] lines;
    public TMP_Text titlebox;
    public TMP_Text textbox;

    public GameObject dialogueBox;

    public bool talking = false;

    int curr;
    int count;

    //public CameraFollow2DLERP camerascript;
    //float oldcameradist;
    // Start is called before the first frame update
    void Start()
    {
        //camerascript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow2DLERP>();
    }

    public void StartDialogue(Dialogue dialogue){
        //Show box and freeze time
        dialogueBox.SetActive(true);
        talking = true;
        //Shift Camera down
        //oldcameradist = camerascript.up;
        //camerascript.up = 0;
        //lines.Enqueue("Test");
        count = dialogue.sentences.Length;
        curr = 1;

        for (int i = 0; i < dialogue.sentences.Length;i++){
            lines[i] = dialogue.sentences[i];
            Debug.Log(i);
        }
        for (int i = 0; i < dialogue.titles.Length;i++){
            title[i] = dialogue.titles[i];
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence(){
        if(count == curr){
            EndDialogue();
            //Start time up again close box
            dialogueBox.SetActive(false);
            return;
        }
        string titl = title[curr];
        titlebox.text = titl;
        string sentence = lines[curr];
        textbox.text = sentence;
        curr++;
    }
    public void EndDialogue(){
        Debug.Log("End of Convo");
        talking = false;
        //camerascript.up = oldcameradist;
    }
}
/*
    public void DisplayNextSentence(){
        if(lines.Length == 0){
            EndDialogue();
            //Start time up again close box
            dialogueBox.SetActive(false);
            return;
        }
        string title = title.Dequeue();
        titlebox.text = title;
        string sentence = lines.Dequeue();
        textbox.text = sentence;
    }
*/
