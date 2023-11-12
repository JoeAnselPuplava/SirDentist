using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public Queue<string> sentences;

    public TMP_Text title;
    public TMP_Text textbox;

    public GameObject dialogueBox;
    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue){
        dialogueBox.SetActive(true);
        title.text = dialogue.title;
        sentences.Clear();
        foreach (string sentence in dialogue.sentences){
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence(){
        if(sentences.Count == 0){
            EndDialogue();
            dialogueBox.SetActive(false);
            return;
        }
        string sentence = sentences.Dequeue();
        textbox.text = sentence;
    }
    public void EndDialogue(){
        Debug.Log("End of Convo");
    }
}
