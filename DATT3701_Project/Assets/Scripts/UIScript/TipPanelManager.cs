using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipPanelManager : MonoBehaviour
{
    
    public GameObject TipMenu;
    public GameObject PauseShade;

    public GameObject Tip1;
    public GameObject Tip2;
    public GameObject Tip3;

    public GameObject CloseTipButton;

    private AudioManager audioManager;


    private bool isPanelActive = false;
    void Start(){
        audioManager = FindObjectOfType<AudioManager>();
    }
    void Update(){
        if(Input.GetKeyDown(KeyCode.H) && isPanelActive == false ){
            audioManager.Play("PanelToggle");
            OpenTipMenu();
        }else if(Input.GetKeyDown(KeyCode.H) && isPanelActive == true){
            CloseTipMenu();
        }
    }
    
    public void OpenTipMenu(){
        if(TipMenu != null && isPanelActive == false){
            TipMenu.SetActive(true);
            isPanelActive = true;
            PauseShade.SetActive(true);
        }
    }

    public void CloseTipMenu(){
            audioManager.Play("PanelToggle");
            if(TipMenu != null && isPanelActive == true){
                TipMenu.SetActive(false);
                isPanelActive = false;
                PauseShade.SetActive(false);
            }

            CloseTips();
            CloseTipButton.SetActive(false);
            PauseShade.SetActive(false);

    }

    public void ShowTip1(){
        audioManager.Play("ClickButton");
        bool isTipactive = Tip1.activeSelf;
        
        if(TipMenu != null && isTipactive == false){
                TipMenu.SetActive(false);
                isPanelActive = false;
                Tip1.SetActive(true);
            }
        CloseTipButton.SetActive(true);
        
    }

    public void ShowTip2(){
        audioManager.Play("ClickButton");
         bool isTipactive = Tip1.activeSelf;
        
        if(TipMenu != null && isTipactive == false){
                TipMenu.SetActive(false);
                isPanelActive = false;
                Tip2.SetActive(true);
            }
        CloseTipButton.SetActive(true);
    }

    public void ShowTip3(){
        audioManager.Play("ClickButton");
         bool isTipactive = Tip1.activeSelf;
        
        if(TipMenu != null && isTipactive == false){
                TipMenu.SetActive(false);
                isPanelActive = false;
                Tip3.SetActive(true);
            }
        CloseTipButton.SetActive(true);
    }


    public void CloseTips(){
        Tip1.SetActive(false);
        Tip2.SetActive(false);
        Tip3.SetActive(false);
    }

}
