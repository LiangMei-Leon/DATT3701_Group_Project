using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContentHolder : MonoBehaviour
{
    
    public GameObject[] TutorialImages;
    public int index;
    public int totalImages = 5;
    
    public GameObject TutorialPanel;
    
    
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        TutorialPanel.gameObject.SetActive(true);
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
        
        
        index += 1;
        CheckIndex();


        for(int i=0; i< TutorialImages.Length; i++){
            TutorialImages[i].gameObject.SetActive(false);
            TutorialImages[index].gameObject.SetActive(true);
        }
    
        Debug.Log(index);
    }

    public void Back(){
       
        index -= 1;
        CheckIndex();


        for(int i=0; i< TutorialImages.Length; i++){
            TutorialImages[i].gameObject.SetActive(false);
            TutorialImages[index].gameObject.SetActive(true);
        }
    
        Debug.Log(index);
    }


    public void Close(){
        TutorialPanel.gameObject.SetActive(false);
    }



}
