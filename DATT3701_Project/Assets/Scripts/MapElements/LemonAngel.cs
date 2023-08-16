using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LemonAngel : MonoBehaviour
{
    private GameObject playerManager;
    private PlayerEmotionStatus playerEmotion;
    private float emotionStatus;

    private ParticleSystem VFX;
    private bool vfxplaying = false;

    // Start is called before the first frame update
    void Start()
    {
        playerManager = GameObject.FindWithTag("PlayerManager");
        playerEmotion= playerManager.GetComponent<PlayerEmotionStatus>();
        VFX = this.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
       if(playerEmotion.getFearStatus() && !vfxplaying){
            VFX.Play();
            vfxplaying = true;
       }else if(!playerEmotion.getFearStatus()){
            VFX.Stop();
            vfxplaying = false;
       }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("GhostPlayer")){
            playerEmotion.ReturnNormal();
            this.gameObject.SetActive(false);
        }
    }
}
