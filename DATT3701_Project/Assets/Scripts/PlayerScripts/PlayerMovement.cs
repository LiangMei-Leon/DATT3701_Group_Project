using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private GameObject playerManager;
    private PlayerEmotionStatus playerEmotion;
    private float emotionStatus;
    private bool fearStatus;
    public CharacterController2D controller;

    public float runSpeed = 5f;

    float horizontalMove = 0f;
    bool jump = false;

    // Start is called before the first frame update
    void Start()
    {
        playerManager = GameObject.FindWithTag("PlayerManager");
        playerEmotion= playerManager.GetComponent<PlayerEmotionStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        emotionStatus = playerEmotion.getEmotionStatus();
        fearStatus = playerEmotion.getFearStatus();
        //get keyboard/controller input:left as -1 and right as 1
        //horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        if(!fearStatus)
        {
            if(emotionStatus > 0){
                horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed * -1;
            }else{
                horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
            }
            if(Input.GetButtonDown("Jump"))
            {
                jump = true;
            }
        }
    }

    void FixedUpdate() {
        controller.Move(horizontalMove, jump);
        jump = false;
    }
}
