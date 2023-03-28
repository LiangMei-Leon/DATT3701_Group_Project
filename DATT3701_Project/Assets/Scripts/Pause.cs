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
    }

    // Update is called once per frame
    void Update()
    {
        if(scene != "startmenu"){
            if(Input.GetKeyDown(KeyCode.Escape) && !panelActivating){
                audioManager.Play("PanelToggle");
                pauseShade.SetActive(true);
                pausePanel.SetActive(true);
                panelActivating = true;
            }else if(Input.GetKeyDown(KeyCode.Escape) && panelActivating){
                audioManager.Play("PanelToggle");
                pauseShade.SetActive(false);
                pausePanel.SetActive(false);
                panelActivating = false;
            }
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
        SceneManager.LoadScene("StartMenu");
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
