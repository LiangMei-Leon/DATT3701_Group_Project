using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PollutedGround : MonoBehaviour
{
    public float IncreaseAmount = 2.5f;
    private GameObject playerManager;
    private PlayerEmotionStatus playerEmotion;
    private float emotionStatus;

    public float cooldown = 1f;
    private float timer = 0f;
    private bool isAbleToHit = false;

    // Start is called before the first frame update
    void Start()
    {
        playerManager = GameObject.FindWithTag("PlayerManager");
        playerEmotion = playerManager.GetComponent<PlayerEmotionStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0){
            isAbleToHit = true;
        }else{
            isAbleToHit = false;
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Player")){
            if(isAbleToHit && !playerEmotion.getFearStatus()){
                playerEmotion.IncreaseFear(IncreaseAmount);
                timer = cooldown;
                //Debug.Log("hit");
            }
        }
    }
}
