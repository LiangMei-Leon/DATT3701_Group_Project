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
    private GameObject player;
    private PlayerMovement playerInput;
    private float player_Savedemotion;
    private GameObject[] boxes;
    private GameObject[] slices;
    private bool saved = false;
    private bool panelActivating = false;
    public GameObject pauseShade;
    public GameObject reloadPanel;
    public GameObject warning;

    // Start is called before the first frame update
    void Start()
    {
        playerManager = GameObject.FindWithTag("PlayerManager");
        playerEmotion= playerManager.GetComponent<PlayerEmotionStatus>();
        player = GameObject.FindWithTag("Player");
        playerInput = player.GetComponent<PlayerMovement>();
        if (boxes == null)
            boxes = GameObject.FindGameObjectsWithTag("Boxes");
        if (slices == null)
            slices = GameObject.FindGameObjectsWithTag("LemonSlices");
    }

    // Update is called once per frame
    void Update()
    {
        emotionStatus = playerEmotion.getEmotionStatus();
        if(Input.GetKeyDown("r") && !panelActivating && !playerEmotion.getFearStatus()){
            pauseShade.SetActive(true);
            reloadPanel.SetActive(true);
            panelActivating = true;
        }else if(Input.GetKeyDown("r") && panelActivating && !playerEmotion.getFearStatus()){
            pauseShade.SetActive(false);
            warning.SetActive(false);
            reloadPanel.SetActive(false);
            panelActivating = false;
        }
        if(Input.GetKeyDown("c") && !playerEmotion.getFearStatus())
        {
            saved = true;
            player_Savedemotion = playerEmotion.getEmotionStatus();
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
    }

    public void Rewind()
    {
        if(saved)
        {
            pauseShade.SetActive(false);
            warning.SetActive(false);
            reloadPanel.SetActive(false);
            panelActivating = false;
            playerEmotion.IncreaseFear(30);
            if(player_Savedemotion <= emotionStatus){
                playerEmotion.IncreaseSerenity(emotionStatus - player_Savedemotion);
            }else{
                playerEmotion.IncreaseRage(player_Savedemotion - emotionStatus);
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
        }else{
            warning.SetActive(true);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Back()
    {
        SceneManager.LoadScene("StartMenu");
    }
}
