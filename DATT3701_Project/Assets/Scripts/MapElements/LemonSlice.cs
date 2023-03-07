using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LemonSlice : MonoBehaviour
{
    public float IncreaseAmount = 10f;
    private GameObject playerManager;
    private PlayerEmotionStatus playerEmotion;
    private float emotionStatus;
    private GameObject normalPlayer;
    private bool used = false;
    private SpriteRenderer slice;

    private AudioManager audioManager;
    private GameObject dialogText;

    // Start is called before the first frame update
    void Start()
    {
        playerManager = GameObject.FindWithTag("PlayerManager");
        playerEmotion= playerManager.GetComponent<PlayerEmotionStatus>();
        normalPlayer = GameObject.FindWithTag("Player");
        slice = GetComponent<SpriteRenderer>();
        audioManager = FindObjectOfType<AudioManager>();
        dialogText = normalPlayer.transform.GetChild(1).GetChild(2).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Player") && !used){
            used = true;
            playerEmotion.IncreaseSerenity(IncreaseAmount);
            if(dialogText != null){
                dialogText.SetActive(true);
                Invoke("Cancel2", 1.5f);
            }
            slice.color = new Color(1,1,1,0);
            Destroy(gameObject,1.5f);
        }
    }

    void Cancel2()
    {
        dialogText.SetActive(false);
    }
}
