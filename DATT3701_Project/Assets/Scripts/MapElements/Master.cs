using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Master : MonoBehaviour
{
   public GameObject DialoguePanel;
   public bool playerIsClose;

   public Dialogue dialogue;


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I) &&  playerIsClose ){
           
                //TutorialPanel.SetActive(true);
                TriggerDialogue();
            }    
        
        
    }




    public void TriggerDialogue(){
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
            playerIsClose = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other){
        if(other.CompareTag("Player")){
            playerIsClose = false;
        }
    }




}
