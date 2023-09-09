using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    private bool panelActivating = false;
    public GameObject pauseShade;
    private GameObject tutPanel;
    public GameObject pausePanel;
    public GameObject settingPanel;

    private AudioManager audioManager;
    // Start is called before the first frame update

    public Slider volumeSlider;
    public AudioMixer mixer;
    private float value;
    public string scene = "others";


    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        mixer.GetFloat("volume", out value);
        volumeSlider.value = value;
        tutPanel = GameObject.FindWithTag("TutorialPanel");
    }

    // Update is called once per frame
    void Update()
    {
        if(scene != "startmenu"){
            if(Input.GetKeyDown(KeyCode.P) && !panelActivating){
                audioManager.Play("PanelToggle");
                pauseShade.SetActive(true);
                pausePanel.SetActive(true);
                panelActivating = true;
            }else if(Input.GetKeyDown(KeyCode.P) && panelActivating){
                audioManager.Play("PanelToggle");
                pauseShade.SetActive(false);
                pausePanel.SetActive(false);
                panelActivating = false;
            }
        }
        if(tutPanel != null){
           if(tutPanel.activeSelf){
            if(Input.GetKeyDown(KeyCode.X)){
                audioManager.Play("PanelToggle");
                pauseShade.SetActive(false);
                tutPanel.SetActive(false);
            }
        } 
        }
        
        bool isPauseActive = pauseShade.activeSelf;

        
        // Toggle cursor visibility based on the target object's activation state
        Cursor.visible = isPauseActive;

        // Optionally, you can lock the cursor to the center of the screen when it's hidden
        if (!Cursor.visible)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void Resume()
    {
        audioManager.Play("ClickButton");
        pauseShade.SetActive(false);
        pausePanel.SetActive(false);
        panelActivating = false;
    }

    public void Close()
    {
        audioManager.Play("PanelToggle");
        pauseShade.SetActive(false);
        pausePanel.SetActive(false);
        panelActivating = false;
    }

    public void Back2MainMenu()
    {
        audioManager.Play("ClickButton");
        SceneManager.LoadScene("StartMenu1");
    }

    public void OpenSetting()
    {
        audioManager.Play("ClickButton");
        settingPanel.SetActive(true);
    }


    public void SetVolume(){
        mixer.SetFloat("volume", volumeSlider.value);
        
    }

    public void PlayButtonSound(){
        audioManager.Play("ClickButton");
    }

}
