using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Master : MonoBehaviour
{
   public GameObject TutorialPanel;
   public GameObject DialoguePanel;

   public Dialogue dialogue;
   private GameObject panel;
   
   
   public bool playerIsClose = false;
   public bool panelActiving = false;
   private bool triggerEnable = true;
   
   private GameObject mark;
   public GameObject shade;
   private AudioManager audioManager;
   
   void Start()
   {
        mark = this.gameObject.transform.GetChild(0).gameObject;
        audioManager = FindObjectOfType<AudioManager>();
        panel = GameObject.FindWithTag("Dialogue");
   }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I) && panelActiving == true)
        {
            audioManager.Play("PanelToggle");
            panelActiving = false;
            playerIsClose = false;
            TutorialPanel.SetActive(false);
            shade.SetActive(false);
        }else if(Input.GetKeyDown(KeyCode.I) && panelActiving == false)
        {
            audioManager.Play("PanelToggle");
            panelActiving = true;
        }

        if(playerIsClose)
        {
            mark.SetActive(false);
        }

        if(panelActiving)
        {
            TutorialPanel.SetActive(true);
            shade.SetActive(true);
        }    
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player") && triggerEnable){
            audioManager.Play("PanelToggle");
            triggerEnable = false;
            playerIsClose = true;
            panel.SetActive(true);
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
