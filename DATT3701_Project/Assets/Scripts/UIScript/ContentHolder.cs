using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContentHolder : MonoBehaviour
{
    
    public GameObject[] TutorialImages;
    public int index ;
    public int totalImages = 5;
    
    public GameObject TutorialPanel;
    private AudioManager audioManager;
    private GameObject master;
    private Master masterScript;
    private GameObject pauseShade;
    public int initialIndex;
    
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        index = initialIndex;
        TutorialPanel.gameObject.SetActive(true);
        audioManager = FindObjectOfType<AudioManager>();
        master = GameObject.FindWithTag("Master");
        masterScript = master.GetComponent<Master>();
        pauseShade = GameObject.FindWithTag("PauseShade");
    }


     void Update(){
        if(Input.GetKeyDown(KeyCode.LeftArrow) ){
          Back();
        }else if(Input.GetKeyDown(KeyCode.RightArrow)){
            Next();
        }
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
        
        masterScript.panelActiving = false;
        pauseShade.SetActive(false);
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
