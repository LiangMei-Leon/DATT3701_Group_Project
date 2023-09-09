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

    public float horizontalMove = 0f;
    bool jump = false;
    private Vector3 player_Savedlocation;
    private Animator animator;
    public GameObject pauseShade;

    // Start is called before the first frame update
    void Start()
    {
        playerManager = GameObject.FindWithTag("PlayerManager");
        playerEmotion= playerManager.GetComponent<PlayerEmotionStatus>();

        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        emotionStatus = playerEmotion.getEmotionStatus();
        fearStatus = playerEmotion.getFearStatus();
        
        //get keyboard/controller input:left as -1 and right as 1
        //horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        if(!fearStatus && !pauseShade.activeSelf)
        {
            if(emotionStatus > 0){
                horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed * -1;
            }else{
                horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
            }
            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
            if(Input.GetButtonDown("Jump") || Input.GetKeyDown("w"))
            {
                jump = true;
            }
        }else{
            horizontalMove = 0;
            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        }

        
    }

    void FixedUpdate() {
        controller.Move(horizontalMove, jump);
        jump = false;
    }

    public void UpdatePlayerLocation()
    {
        player_Savedlocation = this.transform.position;
    }

    public void RewindPlayerLocation()
    {
        this.transform.position = player_Savedlocation;
    }
}
