using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    private bool panelActivating = false;
    public GameObject pauseShade;
    public GameObject pausePanel;

    private AudioManager audioManager;
    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
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
    }
}
