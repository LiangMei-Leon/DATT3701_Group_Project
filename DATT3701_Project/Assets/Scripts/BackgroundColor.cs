using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundColor : MonoBehaviour
{
    private SpriteRenderer backgroundIMG;
    private GameObject playerManager;
    private PlayerEmotionStatus playerEmotion;
    private float emotionStatus;
    private float mappingValue = 0f;
    public float rageMaxValue = 100f;
    private float colorChange1;

    // Start is called before the first frame update
    void Start()
    {
        backgroundIMG = GetComponent<SpriteRenderer>();
        playerManager = GameObject.FindWithTag("PlayerManager");
        playerEmotion = playerManager.GetComponent<PlayerEmotionStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        emotionStatus = playerEmotion.getEmotionStatus();
        mappingValue = emotionStatus / rageMaxValue;
        if(mappingValue <= 0)
        {
            backgroundIMG.color = new Color(1, 1, 1, 1);
        }else{
            colorChange1 = 255f - Mathf.Lerp(90f, 255f, mappingValue);
            backgroundIMG.color = new Color(1, colorChange1/255f, colorChange1/255f, 1);
        }
    }
}
