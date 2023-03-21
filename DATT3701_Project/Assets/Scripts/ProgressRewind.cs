using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressRewind : MonoBehaviour
{
    private GameObject playerManager;
    private PlayerEmotionStatus playerEmotion;
    private float emotionStatus;
    private GameObject player;
    private PlayerMovement playerInput;
    private float player_Savedemotion;
    private GameObject[] boxes;
    private GameObject[] slices;
    private bool saved = false;

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
        if(Input.GetKeyDown("c"))
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
        if(Input.GetKeyDown("v") && saved)
        {
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
        }
    }
}
