using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Master : MonoBehaviour
{
   public GameObject TutorialPanel;
   public GameObject DialoguePanel;

   public Dialogue dialogue;
   
   
   public bool playerIsClose = false;
   private bool panelActiving = false;
   private bool triggerEnable = true;
   
   private GameObject mark;
   
   void Start()
   {
        mark = this.gameObject.transform.GetChild(0).gameObject;
   }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I) && panelActiving == true)
        {
            panelActiving = false;
            playerIsClose = false;
            TutorialPanel.SetActive(false);
            Time.timeScale = 1;
        }else if(Input.GetKeyDown(KeyCode.I) && panelActiving == false)
        {
            panelActiving = true;
        }

        if(playerIsClose)
        {
            mark.SetActive(false);
        }

        if(panelActiving)
        {
            TutorialPanel.SetActive(true);
            Time.timeScale = 0;
        }    
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player") && triggerEnable){
            triggerEnable = false;
            playerIsClose = true;
            TriggerDialogue();
        }
    }

    private void OnTriggerExit2D(Collider2D other){
        if(other.CompareTag("Player")){
            triggerEnable = true;
        }
    }

    public void TriggerDialogue(){
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

}
