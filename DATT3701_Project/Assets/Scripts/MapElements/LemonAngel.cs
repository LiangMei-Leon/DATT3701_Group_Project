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
    public float playDuration = 0.5f;

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

    public IEnumerator PlayAndStopParticleSystem()
    {
        Debug.Log("Starting Particle System");
        // Play the particle system
        VFX.Play();

        // Wait for the specified duration
        yield return new WaitForSeconds(playDuration);

        // Stop the particle system
        VFX.Stop();
    }
}
