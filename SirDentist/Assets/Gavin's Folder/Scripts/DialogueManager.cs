using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public Queue<string> titles;
    public Queue<string> sentences;
    public TMP_Text titlebox;
    public TMP_Text textbox;

    public GameObject dialogueBox;

    public bool talking = false;
    // Start is called before the first frame update
    void Start()
    {
        titles = new Queue<string>();
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue){
        //Show box and freeze time
        dialogueBox.SetActive(true);
        talking = true;
        sentences.Clear();
        foreach (string sentence in dialogue.sentences){
            sentences.Enqueue(sentence);
        }
        foreach (string title in dialogue.titles){
            titles.Enqueue(title);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence(){
        if(sentences.Count == 0){
            EndDialogue();
            //Start time up again close box
            dialogueBox.SetActive(false);
            return;
        }
        string title = titles.Dequeue();
        titlebox.text = title;
        string sentence = sentences.Dequeue();
        textbox.text = sentence;
    }
    public void EndDialogue(){
        Debug.Log("End of Convo");
        talking = false;
    }
}
