using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProgressRewind : MonoBehaviour
{
    private GameObject playerManager;
    private PlayerEmotionStatus playerEmotion;
    private float emotionStatus;

    private bool isGhost;
    private float fearStatus;
    private bool respawnable = false;
    private bool respawnUsed = false;
    public GameObject Lemonangel;
    private LemonAngel angelScript;

    private GameObject player;
    private PlayerMovement playerInput;
    private float player_Savedemotion;
    private GameObject[] boxes;
    private GameObject[] slices;
    private bool saved = false;
    private bool panelActivating = false;
    public GameObject pauseShade;
    public GameObject reloadPanel;
    public GameObject restartPanel;
    public GameObject warning;
    public GameObject warning2;
    public GameObject text;
    public bool unlocked = false;

    private AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        playerManager = GameObject.FindWithTag("PlayerManager");
        playerEmotion = playerManager.GetComponent<PlayerEmotionStatus>();
        player = GameObject.FindWithTag("Player");
        playerInput = player.GetComponent<PlayerMovement>();
        audioManager = FindObjectOfType<AudioManager>();
        angelScript = Lemonangel.GetComponent<LemonAngel>();

        if (boxes == null)
            boxes = GameObject.FindGameObjectsWithTag("Boxes");
        if (slices == null)
            slices = GameObject.FindGameObjectsWithTag("LemonSlices");
    }

    // Update is called once per frame
    void Update()
    {
        emotionStatus = playerEmotion.getEmotionStatus();
        if (Input.GetKeyDown("r") && !panelActivating && !playerEmotion.getFearStatus())
        {
            audioManager.Play("PanelToggle");
            pauseShade.SetActive(true);
            reloadPanel.SetActive(true);
            panelActivating = true;
        }
        else if (Input.GetKeyDown("r") && panelActivating && !playerEmotion.getFearStatus())
        {
            audioManager.Play("PanelToggle");
            pauseShade.SetActive(false);
            warning.SetActive(false);
            reloadPanel.SetActive(false);
            panelActivating = false;
        }
        // if (Input.GetKeyDown("c") && !playerEmotion.getFearStatus() && unlocked)
        // {
        //     saved = true;
        //     audioManager.Play("Save");
        //     if (text != null)
        //     {
        //         text.SetActive(true);
        //         Invoke("Cancel", 1f);
        //     }
        //     player_Savedemotion = playerEmotion.getEmotionStatus();
        //     fearStatus = playerEmotion.fearStatus;
        //     respawnable = playerEmotion.respawnable;
        //     respawnUsed = playerEmotion.respawnUsed;

        //     playerInput.UpdatePlayerLocation();
        //     foreach (GameObject box in boxes)
        //     {
        //         BoxFunctions boxf = box.GetComponent<BoxFunctions>();
        //         boxf.UpdateBoxStatus();
        //     }
        //     foreach (GameObject slice in slices)
        //     {
        //         LemonSlice slicef = slice.GetComponent<LemonSlice>();
        //         slicef.UpdateSliceStatus();
        //     }
        // }
    }
    public void save()
    {
        saved = true;
        audioManager.Play("Save");
        if (text != null)
        {
            text.SetActive(true);
            Invoke("Cancel", 1f);
        }
        player_Savedemotion = playerEmotion.getEmotionStatus();
        // fearStatus = playerEmotion.fearStatus;
        // respawnable = playerEmotion.respawnable;
        // respawnUsed = playerEmotion.respawnUsed;
        respawnable = true;
        fearStatus = 0f;
        respawnUsed = false;
        playerEmotion.respawnable = true;
        playerEmotion.respawnUsed = false;
        playerEmotion.fearStatus = 0f;
        Lemonangel.SetActive(true);
        StartCoroutine(angelScript.PlayAndStopParticleSystem());
        playerEmotion.fearCountDown = 10f;
        playerInput.UpdatePlayerLocation();
        foreach (GameObject box in boxes)
        {
            BoxFunctions boxf = box.GetComponent<BoxFunctions>();
            boxf.UpdateBoxStatus();
        }
        foreach (GameObject slice in slices)
        {
            LemonSlice slicef = slice.GetComponent<LemonSlice>();
            slicef.UpdateSliceStatus();
        }
    }
    public void Rewind()
    {
        if (saved)
        {
            audioManager.Play("ClickButton");
            pauseShade.SetActive(false);
            warning.SetActive(false);
            reloadPanel.SetActive(false);
            restartPanel.SetActive(false);
            panelActivating = false;
            //playerEmotion.IncreaseFear(30);
            if (player_Savedemotion <= emotionStatus)
            {
                playerEmotion.IncreaseSerenity(emotionStatus - player_Savedemotion);
            }
            else
            {
                playerEmotion.IncreaseRage(player_Savedemotion - emotionStatus);
            }
            if (fearStatus <= playerEmotion.fearStatus)
            {
                playerEmotion.IncreaseFear(fearStatus - playerEmotion.fearStatus);
            }
            else
            {
                playerEmotion.IncreaseFear(fearStatus - playerEmotion.fearStatus);
            }
            playerEmotion.respawnable = respawnable;
            if(playerEmotion.getFearStatus())
            {
                playerEmotion.ReturnNormal();
            }
            if (!respawnUsed)
            {
                playerEmotion.respawnUsed = false;
                playerEmotion.fearCountDown = 10f;
                Lemonangel.SetActive(true);
                //warningText.SetActive(false);
            }
            playerInput.RewindPlayerLocation();
            foreach (GameObject box in boxes)
            {
                BoxFunctions boxf = box.GetComponent<BoxFunctions>();
                boxf.RewindBox();
            }
            foreach (GameObject slice in slices)
            {
                LemonSlice slicef = slice.GetComponent<LemonSlice>();
                slicef.RewindSlice();
            }
        }
        else
        {
            audioManager.Play("ClickButton");
            warning.SetActive(true);
            warning2.SetActive(true);
        }
    }

    public void Restart()
    {
        audioManager.Play("ClickButton");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Back()
    {
        audioManager.Play("ClickButton");
        SceneManager.LoadScene("StartMenu1");
    }

    public void Close()
    {
        audioManager.Play("PanelToggle");
        pauseShade.SetActive(false);
        warning.SetActive(false);
        reloadPanel.SetActive(false);
        panelActivating = false;
    }

    void Cancel()
    {
        text.SetActive(false);
    }
}
