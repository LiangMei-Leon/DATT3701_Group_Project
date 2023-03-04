using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private GameObject playerManager;
    private PlayerEmotionStatus playerEmotion;
    private float emotionStatus;
    private bool fearStatus;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        playerManager = GameObject.FindWithTag("PlayerManager");
        playerEmotion= playerManager.GetComponent<PlayerEmotionStatus>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        emotionStatus = playerEmotion.getEmotionStatus();
        fearStatus = playerEmotion.getFearStatus();
        if(fearStatus)
        {
            animator.Play("GhostCamera");
        }else{
            animator.Play("NormalCamera");
        }
    }
}
