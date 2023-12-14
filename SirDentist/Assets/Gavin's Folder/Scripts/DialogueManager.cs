using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public string[] titles;
    public string[] lines;
    public TMP_Text titlebox;
    public TMP_Text textbox;

    public GameObject dialogueBox;

    public bool talking = false;

    int currentSentenceIndex;

    void Start()
    {
        // Initialize other variables or components if needed
    }

    public void StartDialogue(Dialogue dialogue)
    {
        dialogueBox.SetActive(true);
        talking = true;

        lines = new string[dialogue.sentences.Length];
        titles = new string[dialogue.titles.Length];

        for (int i = 0; i < dialogue.sentences.Length; i++)
        {
            lines[i] = dialogue.sentences[i];
        }

        for (int i = 0; i < dialogue.titles.Length; i++)
        {
            titles[i] = dialogue.titles[i];
        }

        currentSentenceIndex = 0;
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (currentSentenceIndex == lines.Length)
        {
            EndDialogue();
            dialogueBox.SetActive(false);
            return;
        }

        string title = titles[currentSentenceIndex];
        titlebox.text = title;

        string sentence = lines[currentSentenceIndex];
        textbox.text = sentence;

        currentSentenceIndex++;
    }

    public void EndDialogue()
    {
        Debug.Log("End of Convo");
        talking = false;
        // Perform any other necessary actions upon ending dialogue
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
