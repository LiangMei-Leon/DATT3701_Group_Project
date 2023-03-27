using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContentHolder : MonoBehaviour
{
    
    public GameObject[] TutorialImages;
    public int index = 0;
    public int totalImages = 5;
    
    public GameObject TutorialPanel;
    private AudioManager audioManager;
    
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        //TutorialPanel.gameObject.SetActive(false);
        audioManager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void CheckIndex()
    {
        if(index >= totalImages-1){
            index = totalImages-1;
        }
    
        if(index <= 0){
            index = 0;
        }

        if(index == 0){
            TutorialImages[0].gameObject.SetActive(true);
        }
    
    }

    public void Next(){
        
        audioManager.Play("ClickButton");
        index += 1;
        CheckIndex();


        for(int i=0; i< TutorialImages.Length; i++){
            TutorialImages[i].gameObject.SetActive(false);
            TutorialImages[index].gameObject.SetActive(true);
        }
    
        Debug.Log(index);
    }

    public void Back(){
        audioManager.Play("ClickButton");
        index -= 1;
        CheckIndex();


        for(int i=0; i< TutorialImages.Length; i++){
            TutorialImages[i].gameObject.SetActive(false);
            TutorialImages[index].gameObject.SetActive(true);
        }
    
        Debug.Log(index);
    }


    public void Close(){
        audioManager.Play("PanelToggle");
        index = 0;
        CheckIndex();
       for(int i=0; i< TutorialImages.Length; i++){
            TutorialImages[i].gameObject.SetActive(false);
            TutorialImages[index].gameObject.SetActive(true);
        }
        
        TutorialPanel.gameObject.SetActive(false);
        // change the PanelActiving to false to fix panel close problem
    }



}
