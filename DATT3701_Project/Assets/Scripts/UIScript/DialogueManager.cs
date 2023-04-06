using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    public Animator animator;

    public GameObject shade;
    private GameObject panel;
    
    
    
    private Queue<string> sentences;
    private AudioManager audioManager;
    
    
    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        audioManager = FindObjectOfType<AudioManager>();
        panel = GameObject.FindWithTag("Dialogue");
    }

    public void StartDialogue(Dialogue dialogue){
        
        panel.SetActive(true);
        animator.SetBool("isOpen", true);
        
        Debug.Log ("Starting conversation with" + dialogue.name);

        nameText.text =dialogue.name;

        sentences.Clear();

        foreach(string sentence in dialogue.sentences){
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
        shade.SetActive(true);
        

        
    }

    public void DisplayNextSentence(){
        audioManager.Play("ClickButton");
        if (sentences.Count == 0){
            EndDialogue();
            return;
        }
    
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));

    }

    IEnumerator TypeSentence (string sentence){
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray()){
            dialogueText.text += letter;
            yield return null;
        }
    }

    void EndDialogue(){
        
        Debug.Log("End of Concersation");
        animator.SetBool("isOpen", false);
        panel.SetActive(false);
        shade.SetActive(false);
    }


    


}
