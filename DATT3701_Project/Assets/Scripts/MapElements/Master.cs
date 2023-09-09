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

    public bool panelActiving = true;
    private bool triggerEnable = true;

    private GameObject mark;
    public GameObject shade;
    private AudioManager audioManager;

    public bool playerNearby = false;
    public bool dialogueCheck = false;

    void Start()
    {
        mark = this.gameObject.transform.GetChild(0).gameObject;
        audioManager = FindObjectOfType<AudioManager>();
        panel = GameObject.FindWithTag("Dialogue");
    }
    // Update is called once per frame
    void Update()
    {
        if (playerNearby)
        {
            if (Input.GetKeyDown(KeyCode.E)&& dialogueCheck == false)
            {
                Debug.Log("Interact");
                audioManager.Play("PanelToggle");
                triggerEnable = false;
                panel.SetActive(true);
                TriggerDialogue();
                dialogueCheck = true;
            }else if(Input.GetKeyDown(KeyCode.E)&& dialogueCheck == true)
            {
                TriggerNextDialogue();
            }
            mark.SetActive(true);
        }else{
            mark.SetActive(false);
        }

        if ((Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.X))&& panelActiving == true)
        {
            audioManager.Play("PanelToggle");
            panelActiving = false;
            TutorialPanel.SetActive(false);
            shade.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.I)&& panelActiving == false)
        {
            audioManager.Play("PanelToggle");
            panelActiving = true;
        }

        if (panelActiving)
        {
            TutorialPanel.SetActive(true);
            shade.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && triggerEnable)
        {
            playerNearby = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            triggerEnable = true;
            playerNearby = false;
        }
    }

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
    public void TriggerNextDialogue()
    {
        FindObjectOfType<DialogueManager>().DisplayNextSentence();
    }

}
