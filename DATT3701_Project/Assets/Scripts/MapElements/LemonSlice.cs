using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LemonSlice : MonoBehaviour
{
    public float IncreaseAmount = 10f;
    private GameObject playerManager;
    private PlayerEmotionStatus playerEmotion;
    private float emotionStatus;

    // Start is called before the first frame update
    void Start()
    {
        playerManager = GameObject.FindWithTag("PlayerManager");
        playerEmotion= playerManager.GetComponent<PlayerEmotionStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Player")){
            playerEmotion.IncreaseSerenity(IncreaseAmount);
            Destroy(gameObject);
        }
    }
}