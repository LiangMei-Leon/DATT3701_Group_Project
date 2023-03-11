using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Master : MonoBehaviour
{
   public GameObject TutorialPanel;
   public bool playerIsClose;
   private bool panelActiving = false;
   private bool triggerEnable = true;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I) && panelActiving == true)
        {
            panelActiving = false;
            playerIsClose = false;
            TutorialPanel.SetActive(false);
        }else if(Input.GetKeyDown(KeyCode.I) && panelActiving == false)
        {
            panelActiving = true;
        }

        if(panelActiving || playerIsClose)
        {
            TutorialPanel.SetActive(true);
        }    
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player") && triggerEnable){
            triggerEnable = false;
            panelActiving = true;
            playerIsClose = true;
            //TutorialPanel.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other){
        if(other.CompareTag("Player")){
            triggerEnable = true;
            panelActiving = false;
            playerIsClose = false;
            TutorialPanel.SetActive(false);
        }
    }

}
